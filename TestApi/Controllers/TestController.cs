//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using uBeac.Core.Identity;
//using uBeac.Core.Web;

//namespace TestApi.Controllers
//{
//    public class TestController : BaseController
//    {
//        private readonly RoleManager<Role> _roleManager;
//        private readonly UserManager<User> _userManager;

//        public TestController(RoleManager<Role> roleManager, UserManager<User> userManager)
//        {
//            _roleManager = roleManager;
//            _userManager = userManager;
//        }

//        [Get]
//        public async Task<IResultSet> TestRoles(CancellationToken cancellationToken = default)
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                var role = new Role { Name = "Role_" + i.ToString() };
//                await _roleManager.CreateAsync(role);
//                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("a", "b"));
//                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("c", "d"));
//            }

//            var x = await _roleManager.FindByNameAsync("role_1");
//            var y = await _roleManager.GetClaimsAsync(x);
//            var z = await _roleManager.FindByIdAsync(x.Id.ToString());
//            var k = _roleManager.Roles.ToList();

//            return "OK".ToResultSet();
//        }

//        [Get]
//        public async Task<IResultSet> TestUsers(CancellationToken cancellationToken = default)
//        {
//            var result = new List<IdentityResult>();
//            for (int i = 0; i < 2; i++)
//            {
//                var user = new User
//                {
//                    Email = "aaa_" + i.ToString() + "@xxx.com",
//                    UserName = "aaa_" + i.ToString(),
//                };
//                var idResult = await _userManager.CreateAsync(user, "password_" + i.ToString());
//                result.Add(idResult);

//                if (idResult.Succeeded)
//                {
//                }
//            }


//            return result.ToResultSet();
//        }

//    }
//}
