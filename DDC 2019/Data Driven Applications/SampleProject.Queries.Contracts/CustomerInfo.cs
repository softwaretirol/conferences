using System;

namespace SampleProject.Queries.Contracts
{
    public class CustomerInfo
    {
        public string CompanyName { get; set; }
        public int OrderCount { get; set; }
        public DateTime? LastOrder { get; set; }
    }
}