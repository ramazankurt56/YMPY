using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore.Relational.WebApi.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set;}
    }
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("Role")]
        public Role Role { get; set; }


    }
}
