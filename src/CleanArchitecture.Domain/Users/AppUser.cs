using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Users
{
    public sealed class AppUser : IdentityUser<Guid>    
    {
        public AppUser()
        {
            Id = Guid.CreateVersion7();
        }
        public string FirstName { get; set; } = default!;
        public string  LastName { get; set; } = default!;
        public string FullName => $"{FirstName} {LastName}";

        #region Audit Log
        public DateTimeOffset CreateAt { get; set; }
        public Guid CreateUserId { get; set; } = default!;
        public DateTimeOffset? UpdateAt { get; set; }
        public Guid? UpdateUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
        public Guid? DeleteUserId { get; set; }

        #endregion
    }
}
