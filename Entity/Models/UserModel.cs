using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MatysProjekt.Entity.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public string LastLogonAttempt { get; set; }

    }
}
