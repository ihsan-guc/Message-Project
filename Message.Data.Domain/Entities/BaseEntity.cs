using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Data.Domain.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
    public class BaseGuidEntity : BaseEntity<Guid>
    {

    }
    public class TrackableEntity : BaseGuidEntity
    {
        public Guid CreaterId { get; set; }
        public Guid? LastUpdaterId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
