using System;

namespace uBeac.Core
{
    public class BaseEntity<TKey> : Entity<TKey>, IBaseEntity<TKey>
       where TKey : IEquatable<TKey>
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public TKey CreateBy { get; set; }
        public TKey UpdateBy { get; set; }
    }

    public class BaseEntity : BaseEntity<Guid>, IBaseEntity
    {
    }
}
