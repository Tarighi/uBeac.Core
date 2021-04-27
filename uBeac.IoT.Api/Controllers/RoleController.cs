using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Identity;
using uBeac.Core.Web;
using uBeac.IoT.Api.DTO;

namespace uBeac.IoT.Api.Controllers
{
    public class RoleController : BaseController
    {

        private readonly IRoleService<Role> _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService<Role> roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [Get]
        public async Task<IListResultSet<RoleResponse>> GetAll(CancellationToken cancellationToken = default)
        {
            var roles = (await _roleService.GetAll(cancellationToken)).ToList();
            return _mapper.Map<List<RoleResponse>>(roles).ToListResultSet();
        }

        [Post]
        public async Task<IResultSet<Guid>> Add([Body] RoleAddRequest model, CancellationToken cancellationToken = default)
        {
            var role = _mapper.Map<Role>(model);

            await _roleService.Insert(role, cancellationToken);

            return role.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Update([Body] RoleUpdateRequest model, CancellationToken cancellationToken = default)
        {
            var role = _mapper.Map<Role>(model);

            var updateResult = await _roleService.Update(role, cancellationToken);

            return updateResult.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Remove([Body] RoleRemoveRequest model, CancellationToken cancellationToken = default)
        {
            var deleteResult = await _roleService.Delete(model.Id, cancellationToken);
            return deleteResult.ToResultSet();
        }

    }
}
