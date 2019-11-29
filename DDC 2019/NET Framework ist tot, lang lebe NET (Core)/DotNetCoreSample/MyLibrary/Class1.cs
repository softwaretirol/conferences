using System;

namespace MyLibrary
{
#if NETCOREAPP
    public class Class1
    {
        public Class1()
        {
        }
    }
#else
    public class Class1
    {
        public Class1()
        {
        }
    }
#endif
}
