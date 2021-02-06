using Afforestation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afforestation.Domain.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string TreeType { get; set; }
        public int TreeCount { get; set; }
        public Category Category { get; set; }

        public double Long { get; set; }
        public double Lat { get; set; }
        public DateTime TimeStamps { get; set; } 

        public int UserId { get; set; }
        public Users User { get; set; }

        public ICollection<Attachments> Attachments { get; set; }

    }
}                                               
                                                                               