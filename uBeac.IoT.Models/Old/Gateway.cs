using System;
using System.Collections.Generic;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Gateway : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? LastRequestDate { get; set; }
        public long RequestCount { get; set; }
        public GatewaySecurity Security { get; set; }
    }

    public class GatewaySecurity
    {
        public GatewaySecurityHttp Http { get; set; }
        public GatewaySecurityMqtt Mqtt { get; set; }
        public GatewaySecurityIpRestriction IpRestriction { get; set; }
    }

    public class GatewaySecurityHttp
    {
        public bool Ssl { get; set; }
        public bool Enabled { get; set; }
        public GatewaySecurityHttpHeaders Headers { get; set; }
    }

    public class GatewaySecurityHttpHeaders : Dictionary<string, string>
    {
    }

    public class GatewaySecurityMqtt
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Tls { get; set; }
        public bool Enabled { get; set; }
    }

    public class GatewaySecurityIpRestriction
    {
        public List<string> AllowedIps { get; set; }
        public List<string> DeniedIps { get; set; }
    }

}