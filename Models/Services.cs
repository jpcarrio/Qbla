using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Qbla.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        [Required]
        public string Service { get; set; }
        public string Form { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
