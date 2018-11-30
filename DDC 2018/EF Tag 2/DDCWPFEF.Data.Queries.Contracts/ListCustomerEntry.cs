using System;
using System.Collections;

namespace DDCWPFEF.Data.Queries
{
    public class ListCustomerEntry
    {
        public int Id { get; private set; }
        public string CompanyName { get; private set; }
        public string City { get; private set; }

        public ListCustomerEntry(int id, string companyName, string city)
        {
            Id = id;
            CompanyName = companyName;
            City = city;
        }
    }
}
