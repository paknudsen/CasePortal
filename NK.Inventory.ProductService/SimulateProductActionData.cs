using NK.Data.Repositories.Entities;
using NK.DataRepositories;


namespace NK.Inventory.ProductService
{
    public interface ISimulateProductActionData
    {
        public Product GetProduct(int productId);
        public void UpdateProduct(Product product);
    }
    public class SimulateProductActionData : ISimulateProductActionData
    {
        public Product GetProduct(int productId)
        {
            using (var repo = new ProductRepository())
            {
                return repo.Products.FirstOrDefault(q => q.ProductId == productId);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var repo = new ProductRepository())
            {
                repo.Products.Update(product);
                repo.Commit();

            }
        }
    }
}
