using System;
using System.ComponentModel.DataAnnotations;

namespace Qbla.Models
{
    public class Payments
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string ServiceId { get; set; }
        [Required]
        public float Amount { get; set; }
        public string Beneficiary { get; set; }
        public DateTime PayDate { get; set; }
        public string Description { get; set; }
    }
}
