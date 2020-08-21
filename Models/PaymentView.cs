using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qbla.Models
{
    public class PaymentView
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }        
        public string Lastname { get; set; }        
        public string Address { get; set; }        
        public string City { get; set; }       
        public string Zipcode { get; set; }        
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SS { get; set; }
        public string Alien { get; set; }        
        public string BirthCountry { get; set; }        
        public string MigrationStatus { get; set; }
        public string ServiceId { get; set; }
        public string Title { get; set; }
        public float Amount { get; set; }
        public DateTime PayDate { get; set; }
        public string Description { get; set; }
    }
}
