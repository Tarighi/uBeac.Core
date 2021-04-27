using System;

namespace uBeac.Core
{
    public class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public class Entity : Entity<Guid>, IEntity
    {
    }
}
