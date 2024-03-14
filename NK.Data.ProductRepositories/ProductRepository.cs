using Newtonsoft.Json;
using NK.Data.Repositories.Entities;
using NK.Data.Repository.Core;
using System.Runtime.CompilerServices;

namespace NK.DataRepositories
{
    public interface IProductRepository : IRepositoryFactory
    {
        IRepository<Product> Products { get; set; }
    }

    public class ProductRepository : Repository, IProductRepository
    {
        public IRepository<Product> Products { get; set; }
    }

}
