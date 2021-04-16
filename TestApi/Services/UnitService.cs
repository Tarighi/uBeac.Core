using TestApi.Models;
using TestApi.Repositories;
using uBeac.Core.Services;

namespace TestApi.Services
{
    public interface IUnitService : IEntityService<Unit>
    {
    }

    public class UnitService : EntityService<Unit>, IUnitService
    {
        public UnitService(IUnitRepository repository) : base(repository)
        {
        }
    }
}
