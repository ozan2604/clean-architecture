using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class EntityDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public Guid CreateUserId { get; set; }
        public string CreateUserName { get; set; } = default!;
        public DateTimeOffset? UpdateAt { get; set; }
        public Guid? UpdateUserId { get; set; }
        public string? UpdateUserName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
    }
}
