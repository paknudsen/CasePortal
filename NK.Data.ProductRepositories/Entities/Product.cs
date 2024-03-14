using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace NK.Data.Repositories.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageLargeUrl { get; set; }
        public string ManufacturerLogo { get; set; }
        public string ShortDescription { get; set; }
        public double FinalSalesPrice { get; set; }
        public string AverageRatingFormatted { get; set; }
    }
}
