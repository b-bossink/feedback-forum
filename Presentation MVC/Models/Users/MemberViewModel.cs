
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Presentation_MVC.Models.Posting;

namespace Presentation_MVC.Models.Users
{
    public class MemberViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        public string Emailaddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
