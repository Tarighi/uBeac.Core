using TestApi.Models;
using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Repositories.MongoDB;

namespace TestApi.Repositories
{
    public interface IUnitRepository : IBaseEntityRepository<Unit>
    {
    }

    public class UnitRepository : MongoBaseEntityRepository<Unit>, IUnitRepository
    {
        public UnitRepository(MainDBContext mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
