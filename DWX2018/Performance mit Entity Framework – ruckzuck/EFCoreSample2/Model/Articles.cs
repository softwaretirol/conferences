using System;
using System.Collections.Generic;

namespace EFCoreSample2.Model
{
    public partial class Articles
    {
        public Articles()
        {
            OrderArticles = new HashSet<OrderArticles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SizeDescription { get; set; }


        public ICollection<OrderArticles> OrderArticles { get; set; }
    }
}
