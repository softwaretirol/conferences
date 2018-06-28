using System;
using System.Collections.Generic;

namespace EFCoreSample2.Model
{
    public partial class OrderArticles
    {
        public int OrderId { get; set; }
        public int ArticleId { get; set; }

        public Articles Article { get; set; }
        public Orders Order { get; set; }
    }
}
