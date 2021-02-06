using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afforestation.Domain.Dtos
{
    public class UserRegister
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Phone { get; set; }
                                                
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }         
    }                                                
}
