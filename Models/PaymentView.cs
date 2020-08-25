using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Qbla.Models
{
    public class PaymentView
    {
        public int Id { get; set; }
        public string CaseId { get; set; }
        public string CustomerId { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SS { get; set; }
        public string Alien { get; set; }        
        public string BirthCountry { get; set; }
        [Required]
        public string MigrationStatus { get; set; }
        public string ServiceId { get; set; }
        public string Title { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public DateTime PayDate { get; set; }
        public string Description { get; set; }
    }
}
