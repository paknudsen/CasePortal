using Newtonsoft.Json;
using NK.Data.Repositories.Entities;
using NK.Data.Repository.Core;
using NK.DataRepositories;

namespace NK.Data.Repositories.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static void FillProductRepositoryMock(this IRepository<Product> repo)
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Specify the filename
            string fileName = "products.json";

            // Combine the directory path and the filename to get the full file path
            string filePath = Path.Combine(executableDirectory, fileName);
            string jsonText = File.ReadAllText(filePath);

            // Deserialize JSON into objects
            var products = JsonConvert.DeserializeObject<MockProduct[]>(jsonText);

            var entityProducts = products.Select(t => new Product
            {
                ProductId = t.Source.ProductId,
                FinalSalesPrice = t.Source.FinalSalesPrice,
                Name = t.Source.Name,
                AverageRatingFormatted = t.Source.AverageRatingFormatted,
                ImageLargeUrl = t.Source.ImageLargeUrl,
                ManufacturerLogo = t.Source.ManufacturerLogo,
                ShortDescription = t.Source.ShortDescription,
                NumberOfRatings = t.Source.NumberOfRatings
            });

            repo.Add(entityProducts);
           
        }
    }

    public class MockProduct
    {
        [JsonProperty("_source")]
        public ProductSource Source { get; set; }
    }

    public class ProductSource
    {
        [JsonProperty("imageLargeUrl")]
        public string ImageLargeUrl { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("manufacturerLogo")]
        public string ManufacturerLogo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("finalSalesPrice")]
        public double FinalSalesPrice { get; set; }

        [JsonProperty("averageRatingFormatted")]
        public string AverageRatingFormatted { get; set; }

        [JsonProperty("numberOfRatings")]
        public int NumberOfRatings { get; set; }
    }
}
