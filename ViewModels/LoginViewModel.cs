using School_library.Commands;
using School_library.DAO;
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

        private UserDAO userDao;

        #region Properties
        public ObservableCollection<LanguageAndFlag> languages { get; set; } = new ObservableCollection<LanguageAndFlag>();
        public User? user { get; private set; } = null;
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

        public LoginViewModel(UserDAO userDao, ObservableCollection<LanguageAndFlag> languages)
        {
            this.userDao = userDao;
            this.languages = languages;
            selectedLanguage = languages[0];
        }


        public void login(string password)
        {
            User? userToFind = userDao.getUser(username);

            if(userToFind == null || userToFind.userType.Equals(User.UserTypes.MEMBER))
            {
                MessageBox.Show(School_library.Resources.UsernameDoesntExist, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(password.Equals(userToFind.password) == false)
            {
                MessageBox.Show(School_library.Resources.IncorrectPassword, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.user = userToFind;
        }
    }
}
