using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class MemberViewModel : ViewModelBase
    {
        private Member member;

        public User User
        {
            get { return member.User; }
        }
        public int UserId
        {
            get { return member.User.UserId; }
            set
            {
                member.UserId = value;
                OnPropertyChange("UserId");
            }
        }
        public string FirstName
        {
            get { return member.User.FirstName; }
            set
            {
                member.User.FirstName = value;
                OnPropertyChange("FirstName");
            }
        }
        public string LastName
        {
            get { return member.User.LastName; }
            set
            {
                member.User.LastName = value;
                OnPropertyChange("LastName");
            }
        }
        public string Username
        {
            get { return member.User.Username; }
            set
            {
                member.User.Username = value;
                OnPropertyChange("Username");
            }
        }
        public string Password
        {
            get { return member.User.Password; }
            set
            {
                member.User.Password = value;
                OnPropertyChange("Password");
            }
        }
        public string UserType
        {
            get { return member.User.UserType; }
            set
            {
                member.User.UserType = value;
                OnPropertyChange("UserType");
            }
        }
        public bool Active
        {
            get
            {
                if (member.User.Active == 0)
                    return false;
                return true;
            }
            set
            {
                if (value == false)
                    member.User.Active = 0;
                else
                    member.User.Active = 1;

                OnPropertyChange("Active");
            }
        }
        public string Localization
        {
            get { return member.User.Localization; }
            set
            {
                member.User.Localization = value;
                OnPropertyChange("Localization");
            }
        }
        public string Theme
        {
            get { return member.User.Theme; }
            set
            {
                member.User.Theme = value;
                OnPropertyChange("Theme");
            }
        }

        public MemberViewModel(Member accountant)
        {
            this.member = accountant;
        }

        public override string ToString()
        {
            return member.ToString();
        }
    }
}
