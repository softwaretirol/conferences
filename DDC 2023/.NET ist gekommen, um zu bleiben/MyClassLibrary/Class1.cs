using System;
using System.Windows.Forms;

namespace MyClassLibrary;

public class MyDemoClass
{
    public MyDemoClass()
    {
        MessageBox.Show("Hallo");
        Console.WriteLine("Hello World :-)");


        var juhu = () =>
        {

        };

#if NETCOREAPP
        Range r = new Range();
#else
        string s = "Juhu";
#endif
    }
}
