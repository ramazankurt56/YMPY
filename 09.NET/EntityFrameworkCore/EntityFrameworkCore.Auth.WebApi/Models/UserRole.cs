using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore.Auth.WebApi.Models
{
    public sealed class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public AppUser? AppUser { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Role? Role { get; set; }
    }
}
