using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afforestation.Domain.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Phone { get; set; }                               
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public ICollection<Posts> Posts { get; set; }
    }
}
                                          