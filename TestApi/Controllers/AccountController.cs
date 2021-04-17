using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using TestApi.DTO;
using uBeac.Core.Identity;
using uBeac.Core.Web;

namespace TestApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public AccountController(UserManager<User> userManager, IMapper mapper, IJwtTokenProvider jwtTokenProvider, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtTokenProvider = jwtTokenProvider;
        }

        [Get]
        [Auth]
        public async Task<string> Test()
        {
            return await Task.FromResult("iuyiuyiuyiuyiuiu");
        }

        [Post]
        public async Task<IResultSet<RegisterResponse>> Register([FromBody] RegisterRequest model)
        {

            var user = _mapper.Map<User>(model);

            var idResult = await _userManager.CreateAsync(user, model.Password);

            // creating result set with created user informarion
            var resultSet = _mapper.Map<RegisterResponse>(user).ToResultSet();

            // setting create user IdentityResult errors to the ResultSet
            resultSet.Errors.AddRange(_mapper.Map<List<Error>>(idResult.Errors));

            return resultSet;
        }

        [Post]
        public async Task<IResultSet<LoginResponse>> Authenticate([Body] LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            LoginResponse response = null;

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var result = new ResultSet<LoginResponse>(response)
                {
                    Code = StatusCodes.Status401Unauthorized
                };
                result.Errors.Add(new Error { Code = "AUTH_FAILED", Description = "Invalid username or password!" });
                return result;
            }

            var token = _jwtTokenProvider.GenerateToken<Guid, User>(user);

            response = _mapper.Map<LoginResponse>(user);
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Expiration = token.ValidTo;

            return response.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> ResetPassword([Body] ResetPasswordRequest model) 
        {
            return true.ToResultSet();
        }

        [Post]
        [Auth]
        public async Task<IResultSet<bool>> ChangePassword([Body] ChangePasswordRequest model)
        {
            return true.ToResultSet();
        }

        [Post]
        [Auth]
        public async Task<IResultSet<bool>> Claims()
        {
            return true.ToResultSet();
        }

    }

}
