using uBeac.Core.Services;
using uBeac.IoT.Models;
using uBeac.IoT.Repositories;

namespace uBeac.IoT.Services
{
    public interface IUnitService : IBaseEntityService<Unit>
    {
    }

    public class UnitService : BaseEntityService<Unit>, IUnitService
    {
        public UnitService(IUnitRepository repository) : base(repository)
        {
        }
    }
}
