namespace HelloWorldEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Persons
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
