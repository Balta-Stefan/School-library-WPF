using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class AccountantViewModel : ViewModelBase
    {
        private Accountant accountant;

        public int UserId
        {
            get { return accountant.User.UserId; }
            set
            {
                accountant.UserId = value;
                OnPropertyChange("UserId");
            }
        }
        public string FirstName 
        {
            get { return accountant.User.FirstName; }
            set
            {
                accountant.User.FirstName = value;
                OnPropertyChange("FirstName");
            }
        }
        public string LastName
        {
            get { return accountant.User.LastName; }
            set
            {
                accountant.User.LastName = value;
                OnPropertyChange("LastName");
            }
        }
        public string Username
        {
            get { return accountant.User.Username; }
            set
            {
                accountant.User.Username = value;
                OnPropertyChange("Username");
            }
        }
        public string Password
        {
            get { return accountant.User.Password; }
            set
            {
                accountant.User.Password = value;
                OnPropertyChange("Password");
            }
        }
        public string UserType
        {
            get { return accountant.User.UserType; }
            set
            {
                accountant.User.UserType = value;
                OnPropertyChange("UserType");
            }
        }
        public bool Active
        {
            get 
            {
                if (accountant.User.Active == 0)
                    return false;
                return true;
            }
            set
            {
                if (value == false)
                    accountant.User.Active = 0;
                else
                    accountant.User.Active = 1;

                OnPropertyChange("Active");
            }
        }
        public string Localization
        {
            get { return accountant.User.Localization; }
            set
            {
                accountant.User.Localization = value;
                OnPropertyChange("Localization");
            }
        }
        public string Theme
        {
            get { return accountant.User.Theme; }
            set
            {
                accountant.User.Theme = value;
                OnPropertyChange("Theme");
            }
        }

        public AccountantViewModel(Accountant accountant)
        {
            this.accountant = accountant;
        }

        public override string ToString()
        {
            return accountant.ToString();
        }
    }
}
