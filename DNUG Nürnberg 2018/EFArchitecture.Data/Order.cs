using System;
using System.Collections.Generic;

namespace EFArchitecture.App
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}