using Microsoft.AspNetCore.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public interface IUserService<TKey, TUser, TRole>
       where TKey : IEquatable<TKey>
       where TUser : User<TKey>
       where TRole : Role<TKey>
    {
        public Task Create(TUser user, string password, CancellationToken cancellationToken = default);
        public Task<AuthResult<TKey, TUser>> Authenticate(string username, string password, CancellationToken cancellationToken = default);
    }

    public interface IUserService<TUser, TRole> : IUserService<Guid, TUser, TRole>
        where TUser : User
        where TRole : Role
    {
    }

    public class UserService<TKey, TUser, TRole> : IUserService<TKey, TUser, TRole>
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
        where TRole : Role<TKey>
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider)
        {
            _userManager = userManager;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public virtual async Task<AuthResult<TKey, TUser>> Authenticate(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null || !await _userManager.CheckPasswordAsync(user, password))
                throw new Exception("User doesn't exist or username/password is not valid!");

            var token = _jwtTokenProvider.GenerateToken<TKey, TUser>(user);

            return new AuthResult<TKey, TUser>
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                 Expiration = token.ValidTo,
                 User = user
            };

        }

        public virtual async Task Create(TUser user, string password, CancellationToken cancellationToken = default)
        {
            var idResult = await _userManager.CreateAsync(user, password);

            if (!idResult.Succeeded)
            {
                var message = string.Empty;
                throw new Exception(string.Join("\r\n", idResult.Errors.Select(x => x.Code + "," + x.Description)));
            }
        }
    }

    public class UserService<TUser, TRole> : UserService<Guid, TUser, TRole>, IUserService<TUser, TRole>
        where TUser : User
        where TRole : Role
    {
        public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider) : base(userManager, jwtTokenProvider)
        {
        }
    }
}
