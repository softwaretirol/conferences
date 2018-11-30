using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDCWPFEF.Data.Model
{
    public partial class Orders
    {
        public Orders()
        {
            ArticleOrders = new HashSet<ArticleOrders>();
        }

        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }
        public int Customer_Id { get; set; }

        [ForeignKey("Customer_Id")]
        [InverseProperty("Orders")]
        public Customers Customer_ { get; set; }
        [InverseProperty("Order_")]
        public ICollection<ArticleOrders> ArticleOrders { get; set; }
    }
}
