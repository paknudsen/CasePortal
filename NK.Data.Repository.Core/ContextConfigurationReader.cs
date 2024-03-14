using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NK.Data.Repository.Core
{
    [XmlRoot("dataSourceConfigurations")]
    public class ContextConfiguration
    {

        private static ContextConfiguration _instance;
        public const string ConfigFileName = "ContextConfigurationSection.config";
        public const string UnitConfigFileName = "MockUpContextConfigurationSection.config";

        [XmlElement("add")]
        public DataSourceConfiguration[] DataSourceConfigurations;

        public static ContextConfiguration Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                string fileName = null;


                var dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                fileName = Path.Combine(dir, $@"Config\{ConfigFileName}");

                if (fileName == null)
                    throw new ApplicationException($"{ConfigFileName} missing in '{dir}' (or any parent directory)");


                // now; find out where our xml file is...
                // will be linked to the project and copyed to the output.

                var serializer = new XmlValidatingSerializer(typeof(ContextConfiguration));

                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    return _instance = (ContextConfiguration)serializer.Deserialize(stream);
            }
        }


        public static DataSourceConfiguration GetConfigForContext(Type getType)
        {
            if (getType.FullName != null && (!getType.FullName.EndsWith("Context") && !getType.FullName.EndsWith("Repository")))
                throw new NotSupportedException("Can only find configuration item for Contexts");

            // -We can override the ContectConfiguration.config by define own dataSourceConfigurations section in Web.config or app.config file
            // We try to find the config section in the ordinary config file ..
            var config = ConfigurationManager.GetSection("dataSourceConfigurations");
            if (config is ContextConfiguration configuration)
            {
                return
                    configuration.DataSourceConfigurations.FirstOrDefault(
                                                                          context => String.Equals(context.ContextName, getType.Name));
            }

            return
                Instance.DataSourceConfigurations.FirstOrDefault(
                                                                 context => String.Equals(context.ContextName, getType.Name));
        }
    }

    public class DataSourceConfiguration
    {
        [XmlAttribute("contextName")]
        public string ContextName;

        [XmlAttribute("contextConnector")]
        public string ContextConnector;

        [XmlAttribute("dataSourceType")]
        public string DataSourceType;

        private DataSource _dataSource;

        public DataSource DataSourceEnum
        {
            get
            {
                if (!Enum.TryParse(DataSourceType, out _dataSource))
                    throw new InvalidEnumArgumentException("No enum with the value: " + DataSourceType);

                return _dataSource;
            }
        }
    }

    public enum AccessType
    {
        /// <summary>
        /// Perfect, keep using this one... It is faster than the original SaveChanges anyway :) 
        /// </summary>
        Normal,

        /// <summary>
        /// Don't use it. SoftDelete, AuditFields and other automatic functionality will not work when using this feature!
        /// If you are inserting an insane amount of rows (around 100k rows is quite insane) it's ok :)
        /// And yes.. it's very fast...
        /// </summary>
        Bulk
    }

    public enum QueryOption
    {
        /// <summary>
        /// https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries
        /// </summary>
        SingleQuery,
        SplitQuery
    }

    public enum DataSource
    {
        EntityFramework,
        DistributedCache,
        MockUp,
        InMemoryFullLoadCache,
        InMemoryQueryLoadCache,
    }

    public class XmlValidatingSerializer<T> : XmlValidatingSerializer
    {
        public XmlValidatingSerializer() : base(typeof(T))
        {
        }

        public new T Deserialize(TextReader reader)
        {
            return (T)base.Deserialize(reader);
        }
    }

    /// <summary>
    /// Summary description for XmlValidatingSerializer.
    /// </summary>
    public class XmlValidatingSerializer : XmlSerializer
    {
        public XmlValidatingSerializer(Type type)
            : base(type)
        {
            base.UnknownNode += XmlValidatingSerializer_UnknownNode;
        }

        private static void XmlValidatingSerializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            throw new InvalidOperationException(
                $"Unknown Node. LineNumber: {e.LineNumber}, LinePosition: {e.LinePosition}, " +
                $"LocalName: {e.LocalName}, Name: {e.Name}, NamespaceURI: {e.NamespaceURI}, " +
                $"NodeType: {e.NodeType}, ObjectBeingDeserialized: {e.ObjectBeingDeserialized}, Text: {e.Text}");
        }
    }
}
