using School_library.Commands;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace School_library.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly mydbContext dbContext;
        private UserViewModel user;
        private App app;

        public ICommand ChangeThemeCommand { get; }
        public SettingsViewModel(mydbContext dbContext, UserViewModel user, App app)
        {
            this.dbContext = dbContext;
            this.user = user;
            this.app = app;

            ChangeThemeCommand = new ChangeThemeCommand(this);
        }

        public void changeTheme(string theme)
        {
            string path = @"Themes\DarkTheme.xaml";
            if (theme.Equals("light"))
                path = @"Themes\LightTheme.xaml";

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                app.changeTheme(dic);
            }
            user.Theme = theme;
            
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception) { }
        }
    }
}
