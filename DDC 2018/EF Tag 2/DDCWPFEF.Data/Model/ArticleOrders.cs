using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDCWPFEF.Data.Model
{
    public partial class ArticleOrders
    {
        public int Article_Id { get; set; }
        public int Order_Id { get; set; }

        [ForeignKey("Article_Id")]
        [InverseProperty("ArticleOrders")]
        public Articles Article_ { get; set; }
        [ForeignKey("Order_Id")]
        [InverseProperty("ArticleOrders")]
        public Orders Order_ { get; set; }
    }
}
