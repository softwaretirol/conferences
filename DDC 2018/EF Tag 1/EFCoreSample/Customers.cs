using System;
using System.Collections.Generic;

namespace EFCoreSample
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
