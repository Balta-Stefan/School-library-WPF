using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace School_library.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void activateDark(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream(@"Themes\DarkTheme.xaml", FileMode.Open))
            {
                ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                Resources.MergedDictionaries.Clear();
                Resources.MergedDictionaries.Add(dic);

                App tmp = (App)DataContext;
                tmp.changeTheme(dic);
            }
        }
        private void activateLight(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream(@"Themes\LightTheme.xaml", FileMode.Open))
            {
                ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                Resources.MergedDictionaries.Clear();
                Resources.MergedDictionaries.Add(dic);

                App tmp = (App)DataContext;
                tmp.changeTheme(dic);
            }
        }
    }
}
