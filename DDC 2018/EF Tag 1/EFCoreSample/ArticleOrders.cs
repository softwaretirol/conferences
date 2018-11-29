using System;
using System.Collections.Generic;

namespace EFCoreSample
{
    public partial class ArticleOrders
    {
        public int ArticleId { get; set; }
        public int OrderId { get; set; }

        public Articles Article { get; set; }
        public Orders Order { get; set; }
    }
}
