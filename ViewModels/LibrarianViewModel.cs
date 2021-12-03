using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class LibrarianViewModel : ViewModelBase
    {
        private Librarian librarian;

        public User User
        {
            get { return librarian.User; }
        }
        public int UserId
        {
            get { return librarian.User.UserId; }
            set
            {
                librarian.UserId = value;
                OnPropertyChange("UserId");
            }
        }
        public string FirstName
        {
            get { return librarian.User.FirstName; }
            set
            {
                librarian.User.FirstName = value;
                OnPropertyChange("FirstName");
            }
        }
        public string LastName
        {
            get { return librarian.User.LastName; }
            set
            {
                librarian.User.LastName = value;
                OnPropertyChange("LastName");
            }
        }
        public string Username
        {
            get { return librarian.User.Username; }
            set
            {
                librarian.User.Username = value;
                OnPropertyChange("Username");
            }
        }
        public string Password
        {
            get { return librarian.User.Password; }
            set
            {
                librarian.User.Password = value;
                OnPropertyChange("Password");
            }
        }
        public string UserType
        {
            get { return librarian.User.UserType; }
            set
            {
                librarian.User.UserType = value;
                OnPropertyChange("UserType");
            }
        }
        public bool Active
        {
            get
            {
                if (librarian.User.Active == 0)
                    return false;
                return true;
            }
            set
            {
                if (value == false)
                    librarian.User.Active = 0;
                else
                    librarian.User.Active = 1;

                OnPropertyChange("Active");
            }
        }
        public string Localization
        {
            get { return librarian.User.Localization; }
            set
            {
                librarian.User.Localization = value;
                OnPropertyChange("Localization");
            }
        }
        public string Theme
        {
            get { return librarian.User.Theme; }
            set
            {
                librarian.User.Theme = value;
                OnPropertyChange("Theme");
            }
        }

        public LibrarianViewModel(Librarian accountant)
        {
            this.librarian = accountant;
        }

        public override string ToString()
        {
            return librarian.ToString();
        }
    }
}
