using School_library.Commands;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace School_library.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string username = string.Empty;
        public LanguageAndFlag selectedLanguage { get; private set; }

        private readonly mydbContext dbContext;

        #region Properties
        public ObservableCollection<LanguageAndFlag> languages { get; set; } = new ObservableCollection<LanguageAndFlag>();
        public UserViewModel? user { get; private set; } = null;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChange("Username");
            }
        }
        public LanguageAndFlag SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                OnPropertyChange("SelectedLanguage");
            }
        }
        #endregion

        public LoginViewModel(mydbContext dbContext, ObservableCollection<LanguageAndFlag> languages)
        {
            this.dbContext = dbContext;
            this.languages = languages;
            selectedLanguage = languages[0];
        }


        public void login(string password)
        {
            User? userToFind = dbContext.Users.Where(u => u.Username.Equals(username) && u.UserType.Equals("MEMBER") == false).FirstOrDefault();

            if(userToFind == null || userToFind.UserType.Equals(AccountTypesEnum.MEMBER))
            {
                MessageBox.Show(School_library.Resources.UsernameDoesntExist, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(password.Equals(userToFind.Password) == false)
            {
                MessageBox.Show(School_library.Resources.IncorrectPassword, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.user = new UserViewModel(userToFind, dbContext);
        }
    }
}
