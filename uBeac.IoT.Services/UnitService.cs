using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Services;
using uBeac.IoT.Models;

namespace uBeac.IoT.Services
{
    public interface IUnitService : IBaseEntityService<Unit>
    {
    }

    public class UnitService : BaseEntityService<Unit>, IUnitService
    {
        public UnitService(IBaseEntityRepository<Unit> repository) : base(repository)
        {
        }
    }
}
