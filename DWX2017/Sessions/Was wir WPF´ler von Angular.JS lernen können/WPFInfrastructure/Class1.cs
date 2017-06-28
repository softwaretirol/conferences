using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFInfrastructure
{
    public class Button
    {

    }
    public class PropertyBinding : Binding
    {
        public PropertyBinding() : this(string.Empty)
        {
        }

        public PropertyBinding(string path) : base(path)
        {
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        }
    }


    public class OneTimeBinding : Binding
    {
        public OneTimeBinding() : this(string.Empty)
        {
        }

        public OneTimeBinding(string path) : base(path)
        {
            Mode = BindingMode.OneTime;
        }
    }
}
