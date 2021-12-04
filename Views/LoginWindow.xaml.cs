using School_library.Models;
using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace School_library.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var pass = Password;
            
            LoginViewModel viewModel = (LoginViewModel)DataContext;
            viewModel.login(pass.Password);

            if (viewModel.user != null)
                Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginButtonClick(null, null);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox languagesCombobox = (ComboBox)sender;
            LanguageAndFlag newLanguage = (LanguageAndFlag)languagesCombobox.SelectedItem;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(newLanguage.language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(newLanguage.language);

            UsernameTextBlock.Text = School_library.Resources.UsernameText;
            PasswordTextBlock.Text = School_library.Resources.PasswordText;
            LoginButton.Content = School_library.Resources.LoginText;
        }
    }
}
