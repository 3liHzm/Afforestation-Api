using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afforestation.Domain.Dtos
{
   public class UserLogin
    {
        
        public string UserName { get; set; }
        
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }       
    }
}
