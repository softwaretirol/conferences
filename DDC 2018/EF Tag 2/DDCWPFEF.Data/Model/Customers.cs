using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDCWPFEF.Data.Model
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }

        [InverseProperty("Customer_")]
        public ICollection<Orders> Orders { get; set; }
    }
}
