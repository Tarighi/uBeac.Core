using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestApi
{
    public interface IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey UserId { get; }
        public IPAddress UserIp { get; }
    }

    public interface IApplicationContext : IApplicationContext<Guid> 
    {
    }

    public class ApplicationContext<TUserKey> : IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            var y = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var tttt = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            var x = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public TUserKey UserId { get; }
        public IPAddress UserIp { get; }

    }
    public class ApplicationContext : ApplicationContext<Guid>, IApplicationContext 
    {
        public ApplicationContext(IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
        {

        }
    }

}
