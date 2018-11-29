namespace EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customers
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string Street { get; set; }

        public int? PostalCode { get; set; }

        public string City { get; set; }

        public string FirstName { get; set; }
    }
}
