using System.Collections.Generic;

namespace DAL
{
    public interface IArticleRepository
    {
        List<Article> Get();
    }
}