using System.Collections.Generic;
using PRISMSample.Interfaces;

namespace PRISMSample.Data
{
    internal class OrderProvider : IOrderProvider
    {
        public IEnumerable<IOrder> Get()
        {
            return new IOrder[]
            {
                new Order(),
                new Order(),
                new Order(),
                new Order(),
            };
        }
    }

    internal class Order : IOrder
    {
    }

    internal class ArticleProvider : IArticleProvider
    {
        public IEnumerable<IArticle> Get()
        {
            return new[]
            {
                new Article(),
                new Article(),
                new Article(),
                new Article(),
                new Article(),
                new Article(),
            };
        }
    }
}