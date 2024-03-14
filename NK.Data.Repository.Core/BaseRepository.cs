using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NK.Data.Repository.Core
{
    public abstract class BaseRepository<T, TP> : IRepository<T>
    {
        public abstract void Add(T entity);
        public abstract void Add(IEnumerable<T> entities);
        public abstract void Update(T entity);
        public abstract void Update(IEnumerable<T> entities);
        public abstract void Delete(T entity);
        public abstract void Delete(IEnumerable<T> entities);
        public abstract void Delete(Expression<Func<T, bool>> filter = null);
        public abstract void PrepareCommit();

        public abstract IEnumerable<T> GetCache(object key,
            int? expirationTimeInSeconds,
            Expression<Func<T, bool>> cacheFilter = null,
            string cacheIncludeProperties = null,
            Expression<Func<T, bool>> returnFilter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> returnOrderBy = null,
            int? skip = null,
            int? take = null);

        public abstract void ClearCache(object key);
        public abstract IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null);



        public abstract IEnumerable<TResult> GetReadOnlySubSet<TResult>(Expression<Func<T, bool>> filter,
                                                               Expression<Func<T, TResult>> selector,
                                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                               int? skip = null,
                                                               int? take = null);

        public abstract IQueryable<T> Queryable { get; }
        public abstract T SingleOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        public abstract T FirstOrDefault(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);
        public abstract TResult Max<TResult>(Expression<Func<T, TResult>> filter);

        public abstract bool Exists(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        public abstract int Count(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        public virtual IList<T> Execute(TP input)
        {
            throw new NotImplementedException();
        }

        public virtual T FirstOrDefault(TP input)
        {
            throw new NotImplementedException();
        }

    }

    public class BaseRepositoryFactory
    {

        public static List<T> FindInstances<T>()
        {
            List<Type> implementors = FindImplementors<T>();

            if (implementors.Count == 0)
            {
                Console.WriteLine("Found 0 candidates for {0}!", typeof(T).Name);
            }

            List<T> instances = new List<T>();

            foreach (Type implementor in implementors)
            {
                try
                {
                    instances.Add((T)Activator.CreateInstance(implementor));
                }
                catch (Exception error)
                {
                    Console.WriteLine("Unable to create instance of {0} {1}", implementor.Name, error);
                }
            }

            return instances;
        }

        public static T FindInstance<T>()
        {
            List<Type> implementors = FindImplementors<T>();

            if (implementors.Count != 1)
            {
                Console.WriteLine("Found {0} candidates for {1}!", implementors.Count, typeof(T).Name);
            }

            return (T)Activator.CreateInstance(implementors.First());
        }

        protected static T CreateInstanceAndCastTo<T>(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        protected static List<Type> FindImplementors<T>()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> implementors = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                if (!assembly.FullName.StartsWith("NK."))
                {
                    continue;
                }

                if (assembly.FullName.StartsWith("NK.Test"))
                {
                    continue;
                }
                try
                {

                    foreach (var type in assembly.GetTypes())
                    {
                        try
                        {
                            if (!type.IsAbstract && !type.IsInterface && typeof(T).IsAssignableFrom(type))
                            {
                                implementors.Add(type);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error finding implementation of " + type);
                        }
                    }
                }
                catch (ReflectionTypeLoadException fs)
                {
                    throw new Exception("Extremely annoying TypeLoader Exception occured trying to do Assembly.GetTypes() - here's the faulting assembly and its relevant assembly-references. Thank me later for saving you hours of debugging! " + assembly.FullName + " " + string.Join("-", fs.LoaderExceptions.ToList().Select(e => "" + e.Message + e.StackTrace).ToArray()), fs);
                }
            }


            return implementors;
        }
    }
}
