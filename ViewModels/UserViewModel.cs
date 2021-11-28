using School_library.DAO;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private User user;
        private UserDAO userDao;

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                user.firstName = value;
                if(userDao.updateUser(user) == true)
                {
                    firstName = value;
                    OnPropertyChange("FirstName");
                }
                else
                {
                    user.firstName = firstName;
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                user.lastName = value;
                if (userDao.updateUser(user) == true)
                {
                    lastName = value;
                    OnPropertyChange("LastName");
                }
                else
                {
                    user.lastName = lastName;
                }
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                user.username = value;
                if (userDao.updateUser(user) == true)
                {
                    username = value;
                    OnPropertyChange("Username");
                }
                else
                {
                    user.username = username;
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                user.password = value;
                if (userDao.updateUser(user) == true)
                {
                    password = value;
                    OnPropertyChange("Password");
                }
                else
                {
                    user.password = password;
                }
            }
        }

        private User.UserTypes userType;
        public User.UserTypes UserType
        {
            get { return userType; }
            set
            {
                user.userType = value;
                if (userDao.updateUser(user) == true)
                {
                    userType = value;
                    OnPropertyChange("UserType");
                }
                else
                {
                    user.userType = userType;
                }
            }
        }

        private bool active;
        public bool Active
        {
            get { return active; }
            set
            {
                user.active = value;
                if (userDao.updateUser(user) == true)
                {
                    active = value;
                    OnPropertyChange("Active");
                }
                else
                {
                    user.active = active;
                }
            }
        }

        public UserViewModel(User user, UserDAO userDao)
        {
            this.user = user;
            this.userDao = userDao;

            firstName = user.firstName;
            lastName = user.lastName;
            username = user.username;
            password = user.password;
            userType = user.userType;
            active = user.active;
        }
    }
}
