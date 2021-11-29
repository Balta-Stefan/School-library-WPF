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
    public class AddUserViewModel
    {
        private UserDAO userDao;
        private ObservableCollection<UserViewModel> users;
        public ObservableCollection<User.UserTypes> UserTypes { get; } = new ObservableCollection<User.UserTypes>();
        public User.UserTypes? SelectedType { get; set; } = null;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICommand AddUserCommand { get; }


        public AddUserViewModel(UserDAO userDao, ObservableCollection<UserViewModel> users)
        {
            this.userDao = userDao;
            this.users = users;

            AddUserCommand = new AddUserCommand(this);

            UserTypes.Add(User.UserTypes.MEMBER);
            UserTypes.Add(User.UserTypes.ACCOUNTANT);
        }

        public void addUser()
        {
            if (FirstName.Equals(string.Empty) ||
                LastName.Equals(string.Empty) ||
                Username.Equals(string.Empty) ||
                Password.Equals(string.Empty) ||
                SelectedType == null)
            {
                MessageBox.Show("You must fill all the fields and choose a user type.", "", MessageBoxButton.OK);
                return;
            }

            User? userToAdd = new User(-1, FirstName, LastName, Username, Password, SelectedType.Value);
            userToAdd = userDao.addUser(userToAdd);

            if (userToAdd == null)
            {
                MessageBox.Show("User not added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                users.Add(new UserViewModel(userToAdd, userDao));
                MessageBox.Show("User added", "User added", MessageBoxButton.OK);
            }
        }
    }
}
