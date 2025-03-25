using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }

       
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreateAt { get; set; }
        public Guid CreateUserId { get; set; } = default!;
        public DateTimeOffset? UpdateAt { get; set; }
        public Guid? UpdateUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
        public Guid? DeleteUserId { get; set; }
    }
}
