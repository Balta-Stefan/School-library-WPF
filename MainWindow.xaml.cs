using School_library.ViewModels;
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

namespace School_library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void switchToDarkTheme(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream(@"Themes\Dark theme\DarkTheme.xaml", FileMode.Open))
            {
                ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(dic);
            }
        }

        private void switchToLightTheme(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream(@"Themes\Light theme\LightTheme.xaml", FileMode.Open))
            {
                ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(dic);
            }
        }
    }
}
