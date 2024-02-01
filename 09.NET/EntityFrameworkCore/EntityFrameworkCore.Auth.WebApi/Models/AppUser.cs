using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore.Auth.WebApi.Models
{
    [Index("Email",IsUnique =true)]
    public sealed class AppUser
    {
        [Key]
        [Column(Order =1)]
        public Guid Guid { get; set; }
        [Column(Order = 2, TypeName = "varchar(50)")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; } = string.Empty;
        [Column(Order =3,TypeName="varchar(50)")]
        [Required(ErrorMessage ="Lastname is Required")]
        public string LastName { get; set; }=string.Empty;
        //[Column(Order =5)]
        //[Required]
        //public byte[] PasswordSalt { get; set; }=new byte[64];
        //[Column(Order =6)]
        //[Required]
        //public byte[] PasswordHash { get; set; }=new byte[128];
        [Column(Order = 4, TypeName = "varchar(300)")]
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
