using System.Collections.Generic;

namespace ShareSample
{
    public interface IShareProvider
    {
        IEnumerable<IShare> Provide();
    }
}