using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using uBeac.Core.Common;

namespace uBeac.Core.Web
{
    public class ApplicationContext<TUserKey> : IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
        }

        public TUserKey UserId { get; }
        public IPAddress UserIp { get; }

    }
    public class ApplicationContext : ApplicationContext<Guid>, IApplicationContext
    {
        public ApplicationContext(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }
    }
}
