using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Identity;
using uBeac.Core.Web;

namespace TestApi.Controllers
{
    public class TestController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;

        public TestController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [Get]
        public async Task<IResultSet> StartTest(CancellationToken cancellationToken = default)
        {
            for (int i = 0; i < 10; i++)
            {
                var role = new Role { Name = "Role_" + i.ToString() };
                await _roleManager.CreateAsync(role);
                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("a", "b"));
                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("c", "d"));
            }

            var x = await _roleManager.FindByNameAsync("role_1");
            var y = await _roleManager.GetClaimsAsync(x);
            var z = await _roleManager.FindByIdAsync(x.Id.ToString());
            var k = _roleManager.Roles.ToList();

            return "OK".ToResultSet();
        }

    }
}
