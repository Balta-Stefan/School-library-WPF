﻿using School_library.Models;
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
        private mydbContext dbContext;

        public User User
        {
            get { return user; }
        }
        public int UserId
        {
            get { return user.UserId; }
            set
            {
                user.UserId = value;
                OnPropertyChange("UserId");
            }
        }
        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                user.FirstName = value;
                OnPropertyChange("FirstName");
            }
        }
        public string LastName
        {
            get { return user.LastName; }
            set
            {
                user.LastName = value;
                OnPropertyChange("LastName");
            }
        }
        public string Username
        {
            get { return user.Username; }
            set
            {
                user.Username = value;
                OnPropertyChange("Username");
            }
        }
        public string Password
        {
            get { return user.Password; }
            set
            {
                user.Password = value;
                OnPropertyChange("Password");
            }
        }
        public string UserType
        {
            get { return user.UserType; }
            set
            {
                user.UserType = value;
                OnPropertyChange("UserType");
            }
        }
        public bool Active
        {
            get
            {
                if (user.Active == 0)
                    return false;
                return true;
            }
            set
            {
                byte oldValue = user.Active;

                try
                {
                    if (value == false)
                        user.Active = 0;
                    else
                        user.Active = 1;

                    dbContext.SaveChanges();
                    OnPropertyChange("Active");
                }
                catch (Exception)
                {
                    user.Active = oldValue;
                }
            }
        }
        public string Localization
        {
            get { return user.Localization; }
            set
            {
                user.Localization = value;
                OnPropertyChange("Localization");
            }
        }
        public string Theme
        {
            get { return user.Theme; }
            set
            {
                user.Theme = value;
                OnPropertyChange("Theme");
            }
        }

        public UserViewModel(User user, mydbContext dbContext)
        {
            this.user = user;
            this.dbContext = dbContext;
        }


        public override string ToString()
        {
            return user.ToString();
        }
    }
}
