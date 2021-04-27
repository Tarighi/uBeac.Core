using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Web;
using uBeac.IoT.Api.DTO;
using uBeac.IoT.Models;
using uBeac.IoT.Services;

namespace uBeac.IoT.Api.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ITeamService _service;
        private readonly IMapper _mapper;

        public TeamController(IMapper mapper, ITeamService teamService)
        {
            _mapper = mapper;
            _service = teamService;
        }

        [Get]
        public async Task<IListResultSet<TeamResponse>> GetAll(CancellationToken cancellationToken = default)
        {
            var teams = await _service.GetAll(cancellationToken);
            var teamsVm = _mapper.Map<List<TeamResponse>>(teams.ToList());
            return teamsVm.ToListResultSet();
        }

        [GetById]
        public async Task<IResultSet<TeamResponse>> GetById([Required] Guid id, CancellationToken cancellationToken = default)
        {
            var team = await _service.GetById(id, cancellationToken);
            return _mapper.Map<TeamResponse>(team).ToResultSet();
        }

        [Post]
        public async Task<IResultSet<Guid>> Add([Body] TeamAddRequest model, CancellationToken cancellationToken = default)
        {
            var team = _mapper.Map<Team>(model);
            await _service.Insert(team, cancellationToken);
            return team.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Update([Body] TeamUpdateRequest model, CancellationToken cancellationToken = default)
        {
            var team = _mapper.Map<Team>(model);
            var isUpdated = (await _service.Replace(team, cancellationToken)) != null;
            return isUpdated.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Remove(Guid id, CancellationToken cancellationToken = default)
        {
            var isDeleted = await _service.Delete(id, cancellationToken);
            return isDeleted.ToResultSet();
        }

        [Get("namespace")]
        public async Task<IResultSet<bool>> Exists(string _namespace, CancellationToken cancellationToken = default)
        {
            var exists = await _service.Exists(_namespace, cancellationToken);
            return exists.ToResultSet();
        }
    }
}
