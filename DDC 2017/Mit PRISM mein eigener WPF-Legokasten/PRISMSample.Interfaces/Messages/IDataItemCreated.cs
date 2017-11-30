using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRISMSample.Interfaces.Messages
{
    public interface IDataItemCreated
    {
        string EntityType { get; }
        int PrimaryKey { get; }
    }
}
