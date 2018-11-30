using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDCWPFEF.Data.Model
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

        [InverseProperty("Article_")]
        public ICollection<ArticleOrders> ArticleOrders { get; set; }
    }
}
