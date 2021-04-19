using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestApi.DTO;
using TestApi.Models;
using uBeac.Core.Identity;
using uBeac.Core.Web;

namespace TestApi.Controllers
{
    public class UserRoleController : BaseController
    {
        private readonly IUserRoleService<AppUser, AppRole> _userRoleService;
        private readonly IMapper _mapper;

        public UserRoleController(IUserRoleService<AppUser, AppRole> userRoleService, IMapper mapper)
        {
            _mapper = mapper;
            _userRoleService = userRoleService;
        }

        [Post]
        public async Task<IResultSet<bool>> AddRoles([Body] UserRoleRequest model, CancellationToken cancellationToken = default)
        {
            var response = await _userRoleService.AddRoles(model.UserId, model.RoleIds, cancellationToken);
            return response.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> RemoveRoles([Body] UserRoleRequest model, CancellationToken cancellationToken = default)
        {
            var response = await _userRoleService.RemoveRoles(model.UserId, model.RoleIds, cancellationToken);
            return response.ToResultSet();
        }

        [GetById]
        public async Task<IListResultSet<RoleResponse>> GetRolesForUser(Guid id, CancellationToken cancellationToken = default)
        {
            var roles = await _userRoleService.GetRolesForUser(id, cancellationToken);
            return _mapper.Map<List<RoleResponse>>(roles).ToListResultSet();
        }

        [GetById]
        public async Task<IListResultSet<UserResponse>> GetUsersInRole(Guid id, CancellationToken cancellationToken = default)
        {
            var users = await _userRoleService.GetUsersInRole(id, cancellationToken);
            return _mapper.Map<List<UserResponse>>(users).ToListResultSet();
        }
    }

}
