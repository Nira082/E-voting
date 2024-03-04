using System.ComponentModel.DataAnnotations;

namespace evoting.Models
{
    public class Register
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}



