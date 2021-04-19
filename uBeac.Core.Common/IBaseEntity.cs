using System;

namespace uBeac.Core
{
    public interface IBaseEntity<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
    }

    public interface IBaseEntity : IBaseEntity<Guid>
    {
    }
}
