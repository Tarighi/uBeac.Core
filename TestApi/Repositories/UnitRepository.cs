using TestApi.Models;
using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Repositories.MongoDB;

namespace TestApi.Repositories
{
    public interface IUnitRepository : IEntityRepository<Unit>
    {
    }

    public class UnitRepository : MongoEntityRepository<Unit>, IUnitRepository
    {
        public UnitRepository(MainDBContext mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
