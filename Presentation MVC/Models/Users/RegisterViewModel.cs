
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Presentation_MVC.Models.Posting;

namespace Presentation_MVC.Models.Users
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required] [EmailAddress]
        public string Emailaddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirmation { get; set; }
    }
}
