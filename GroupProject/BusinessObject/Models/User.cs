using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
        }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
