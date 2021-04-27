using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Services;
using uBeac.IoT.Models;

namespace uBeac.IoT.Services
{
    public interface IProductService : IBaseEntityService<Product>
    {
    }

    public class ProductService : BaseEntityService<Product>, IProductService
    {
        public ProductService(IBaseEntityRepository<Product> repository) : base(repository)
        {
        }
    }
}
