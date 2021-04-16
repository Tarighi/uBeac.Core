using TestApi.Models;
using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Services;

namespace TestApi.Services
{
    public interface IUnitService : IEntityService<Unit>
    {
    }

    public class UnitService : EntityService<Unit>, IUnitService
    {
        public UnitService(IEntityRepository<Unit> repository) : base(repository)
        {
        }
    }
}
