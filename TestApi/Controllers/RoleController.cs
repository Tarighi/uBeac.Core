using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApi.DTO;
using TestApi.Models;
using uBeac.Core.Identity;
using uBeac.Core.Web;

namespace TestApi.Controllers
{
    public class RoleController : BaseController
    {

        private readonly IRoleService<AppRole> _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService<AppRole> roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [Get]
        public async Task<IListResultSet<AppRole>> GetAll(CancellationToken cancellationToken = default)
        {
            return (await _roleService.GetAll(cancellationToken)).ToList().ToListResultSet();
        }

        [Post]
        public async Task<IResultSet<Guid>> Add([Body] RoleAddRequest model, CancellationToken cancellationToken = default)
        {
            var role = _mapper.Map<AppRole>(model);

            await _roleService.Insert(role, cancellationToken);

            return role.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Update([Body] RoleUpdateRequest model, CancellationToken cancellationToken = default)
        {
            var role = _mapper.Map<AppRole>(model);

            var updateResult = await _roleService.Update(role, cancellationToken);

            return updateResult.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Remove([Body][Required] Guid id, CancellationToken cancellationToken = default)
        {
            var deleteResult = await _roleService.Delete(id, cancellationToken);
            return deleteResult.ToResultSet();
        }

    }
}
