using Microsoft.AspNetCore.Identity;

namespace MatysProjekt.Entity
{
    public class UserModel
    {
        private string password;
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public string LastLogonAttempt { get; set; }

        public string Password
        {
            get
            {
                string encrPass = password.Replace("a", "h");
                return encrPass;
            }
            set 
            { 
                password = value;
            }
        }
       
    }
}
