using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Repositories.Abstractions;
using uBeac.Core.Services;
using uBeac.IoT.Models;

namespace uBeac.IoT.Services
{
    public interface ITeamService : IBaseEntityService<Team>
    {
        Task<bool> Exists(string _namespace, CancellationToken cancellationToken = default);
    }

    public class TeamService : BaseEntityService<Team>, ITeamService
    {
        public TeamService(IBaseEntityRepository<Team> repository) : base(repository)
        {
        }

        public async Task<bool> Exists(string _namespace, CancellationToken cancellationToken = default)
        {
            // todo: implement in repository layer
            var teams = await Repository.Find(x => x.Namespace.ToLower() == _namespace.ToLower(), cancellationToken);
            return teams.ToList().Count == 1;
        }
    }
}
