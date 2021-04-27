using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Services;
using uBeac.IoT.Models;

namespace uBeac.IoT.Services
{
    public interface IManufacturerService : IBaseEntityService<Manufacturer>
    {
    }

    public class ManufacturerService : BaseEntityService<Manufacturer>, IManufacturerService
    {
        public ManufacturerService(IBaseEntityRepository<Manufacturer> repository) : base(repository)
        {
        }
    }
}
