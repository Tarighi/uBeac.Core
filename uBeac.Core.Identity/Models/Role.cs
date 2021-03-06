using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace uBeac.Core.Identity
{
    public class Role<TKey>: IdentityRole<TKey>, IEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public List<IdentityRoleClaim<TKey>> Claims { get; set; }

        public Role()
        {
            Claims = new List<IdentityRoleClaim<TKey>>();
        }

        public Role(string name) : this()
        {
            Name = name;
            NormalizedName = name.ToUpperInvariant();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Role : Role<Guid>, IEntity
    {
    }
}
