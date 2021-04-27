using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Repositories.MongoDB;
using uBeac.IoT.Models;

namespace uBeac.IoT.Repositories
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
