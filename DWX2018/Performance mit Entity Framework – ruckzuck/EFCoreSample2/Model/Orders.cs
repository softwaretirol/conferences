using System;
using System.Collections.Generic;

namespace EFCoreSample2.Model
{
    public partial class Orders
    {
        public Orders()
        {
            OrderArticles = new HashSet<OrderArticles>();
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }

        public Customers Customer { get; set; }
        public ICollection<OrderArticles> OrderArticles { get; set; }
    }
}
