using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace aspnetcoreauth.ViewModel
{
    public class LoginInfo
    {
        [Required]
        [Display(Name="User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name="Password")]
        public string Pwd { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
