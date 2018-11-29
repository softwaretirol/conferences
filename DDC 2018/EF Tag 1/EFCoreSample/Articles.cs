using System;
using System.Collections.Generic;

namespace EFCoreSample
{
    public partial class Articles
    {
        public Articles()
        {
            ArticleOrders = new HashSet<ArticleOrders>();
        }

        public int Id { get; set; }
        public string ArticleName { get; set; }
        public decimal UnitPrice { get; set; }

        public ICollection<ArticleOrders> ArticleOrders { get; set; }
    }
}
