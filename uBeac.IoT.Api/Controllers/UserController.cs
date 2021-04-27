using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Common;
using uBeac.Core.Identity;
using uBeac.Core.Web;
using uBeac.IoT.Api.DTO;

namespace uBeac.IoT.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService<User, Role> _userService;
        private readonly IMapper _mapper;
        private readonly IApplicationContext _applicationContext;

        public UserController(IUserService<User, Role> userService, IMapper mapper, IApplicationContext applicationContext)
        {
            _userService = userService;
            _mapper = mapper;
            _applicationContext = applicationContext;
        }

        [Post]
        public async Task<IResultSet<Guid>> Register([Body] RegisterRequest model)
        {
            var user = _mapper.Map<User>(model);
            await _userService.Create(user, model.Password);
            return user.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<LoginResponse>> Authenticate([Body] LoginRequest model, CancellationToken cancellationToken = default)
        {
            var authResult = await _userService.Authenticate(model.Username, model.Password, cancellationToken);
            var response = _mapper.Map<LoginResponse>(authResult);
            return response.ToResultSet();
        }

        [GetById]
        public async Task<IResultSet<UserResponse>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetById(id, cancellationToken);
            var res = _mapper.Map<UserResponse>(user);
            return res.ToResultSet();
        }

        [Get]
        [Auth]
        public async Task<IResultSet<UserResponse>> Get(CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetById(_applicationContext.UserId, cancellationToken);
            var res = _mapper.Map<UserResponse>(user);
            return res.ToResultSet();
        }

    }

}
