using System.ComponentModel.DataAnnotations;

namespace NGK_Assignment_3.Areas.Database.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}