using System;
using System.Collections.Generic;

namespace EFCoreSample
{
    public partial class Orders
    {
        public Orders()
        {
            ArticleOrders = new HashSet<ArticleOrders>();
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }

        public Customers Customer { get; set; }
        public ICollection<ArticleOrders> ArticleOrders { get; set; }
    }
}
