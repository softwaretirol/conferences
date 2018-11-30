using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDCWPFEF.Data.Queries.Contracts
{
    public interface ICustomerQueries
    {
        Task<EditCustomerEntry> Get(int id);
        Task<IEnumerable<ListCustomerEntry>> GetAll();

        Task SaveCustomer(EditCustomer customer);
        Task SaveCustomer(EditCustomerEntry originalCustomer, EditCustomer customer);
    }

    public class EditCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }
    }

    public interface IEditCustomer
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string CompanyName { get; set; }
        string Street { get; set; }
        int? PostalCode { get; set; }
        string City { get; set; }
    }

    public class EditCustomerEntry : IEditCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }


        public int OrderCount { get; set; }
        public DateTime? LastOrder { get; set; }
    }
}
