using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Services;
using uBeac.IoT.Models;

namespace uBeac.IoT.Services
{
    public interface IFirmwareService : IBaseEntityService<Firmware> 
    {
    }

    public class FirmwareService : BaseEntityService<Firmware>, IFirmwareService
    {
        public FirmwareService(IBaseEntityRepository<Firmware> repository) : base(repository)
        {
        }
    }
}
