using System.Collections.Generic;

namespace EFArchitecture.App
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}