using System;
using System.Collections;
using System.Resources;
using System.Windows;
using System.Windows.Markup;


namespace DDCDarkSecretsXaml
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Juhu();
            var asm = typeof(MainWindow).Assembly;
            var resourceNames = asm.GetManifestResourceNames();
            foreach (var name in resourceNames)
            {
                using var stream = asm.GetManifestResourceStream(name);
                using var reader = new ResourceReader(stream);

                foreach (DictionaryEntry entry in reader)
                {

                }
            }
        }
    }
}