using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afforestation.Domain.Models
{
    public class Attachments
    {
        
        public int Id { get; set; }
        public string ImgUrl { get; set; }

        public int PostId { get; set; }
        public Posts Post { get; set; }
    }
}
