using TestApi.Models;
using TestApi.Repositories;
using uBeac.Core.Services;

namespace TestApi.Services
{
    public interface IUnitService : IEntityService<Unit>
    {
    }

    public class UnitService : BaseEntityService<Unit>, IUnitService
    {
        public UnitService(IUnitRepository repository) : base(repository)
        {
        }
    }
}
