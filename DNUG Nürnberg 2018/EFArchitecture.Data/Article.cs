using System.Collections.Generic;

namespace EFArchitecture.App
{
    public class Article
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}