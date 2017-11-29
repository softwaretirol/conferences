using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSample.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
    public class Article
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string ArticleName { get; set; }
        public decimal UnitPrice { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public double Weigth { get; set; }

        [Timestamp]
        public byte[] ROWVERSION { get; set; }

        public virtual Category Category { get; set; }
    }
}
