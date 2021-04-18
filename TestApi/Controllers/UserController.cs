using System;
using System.Threading;
using System.Threading.Tasks;
using TestApi.DTO;
using TestApi.Models;
using uBeac.Core.Identity;
using uBeac.Core.Web;

namespace TestApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService<AppUser, AppRole> _userService;
        public UserController(IUserService<AppUser, AppRole> userService)
        {
            _userService = userService;
        }

        [Post]
        public async Task<IResultSet<Guid>> Register([Body] RegisterRequest model)
        {
            var user = new AppUser 
            {
                UserName=model.Username,
                Email=model.Email                 
            };

            await _userService.Create(user, model.Password);

            return user.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<LoginResponse>> Authenticate([Body] LoginRequest model, CancellationToken cancellationToken = default)
        {
            var authResult = await _userService.Authenticate(model.Username, model.Password, cancellationToken);
            
            var response = new LoginResponse
            {
                Email = authResult.User.Email,
                Expiration = authResult.Expiration,
                Id = authResult.User.Id,
                Token = authResult.Token,
                Username = authResult.User.UserName
            };
            
            return response.ToResultSet();
        }
    }

}
