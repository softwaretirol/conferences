using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRISMSample.Interfaces
{
    public interface IArticleProvider
    {
        IEnumerable<IArticle> Get();
    }

    public interface IOrderProvider
    {
        IEnumerable<IOrder> Get();
    }

    public interface IOrder
    {
    }
}
