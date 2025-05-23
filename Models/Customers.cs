using System.ComponentModel.DataAnnotations;

namespace Qbla.Models
{
    public class Customers
    {
        public int Id { get; set; }
        public string CaseId { get; set; }
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
        [Required]
        public string BirthCountry { get; set; }
        [Required]
        public string MigrationStatus { get; set; }
    }
}
