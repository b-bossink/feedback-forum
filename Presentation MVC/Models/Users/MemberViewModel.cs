
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Presentation_MVC.Models.Posting;

namespace Presentation_MVC.Models.Users
{
    public class MemberViewModel
    {
        public int ID;
        [Required]
        public string Username;
        public string Emailaddress;
        [Required]
        public string Password;
    }
}
