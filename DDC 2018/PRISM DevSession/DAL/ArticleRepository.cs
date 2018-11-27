using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ArticleRepository : IArticleRepository
    {
        public List<Article> Get()
        {
            return new List<Article>()
            {
                new Article(),
                new Article(),
                new Article(),
            };
        }
    }
}
