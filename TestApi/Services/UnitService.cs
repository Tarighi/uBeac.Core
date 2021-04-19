using System.Threading;
using System.Threading.Tasks;
using TestApi.Models;
using TestApi.Repositories;
using uBeac.Core;
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

        public async override Task<Unit> Replace(Unit unit, CancellationToken cancellationToken = default)
        {
            var unitDb = await Repository.GetById(unit.Id, cancellationToken);
            unitDb.ThrowIfNull();

            if (unit.ParentId.HasValue)
            {
                var parentUnitDb = await Repository.GetById(unit.ParentId.Value, cancellationToken);
                parentUnitDb.ThrowIfNull();
                unit.ParentId = unit.ParentId.Value;
            }

            return await base.Replace(unit, cancellationToken); 
        }

    }
}
