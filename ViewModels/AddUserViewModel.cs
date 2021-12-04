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
    public class AddUserViewModel
    {
        private readonly mydbContext dbContext;
        private ObservableCollection<UserViewModel> users;
        public ObservableCollection<AccountTypesEnum> UserTypes { get; } = new ObservableCollection<AccountTypesEnum>();
        public AccountTypesEnum? SelectedType { get; set; } = null;


        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICommand AddUserCommand { get; }


        public AddUserViewModel(mydbContext dbContext, ObservableCollection<UserViewModel> users)
        {
            this.dbContext = dbContext;
            this.users = users;

            AddUserCommand = new AddUserCommand(this);

            UserTypes.Add(AccountTypesEnum.MEMBER);
            UserTypes.Add(AccountTypesEnum.ACCOUNTANT);
        }

        public void addUser()
        {
            if (FirstName.Equals(string.Empty) ||
                LastName.Equals(string.Empty) ||
                Username.Equals(string.Empty) ||
                Password.Equals(string.Empty) ||
                SelectedType == null)
            {
                MessageBox.Show(School_library.Resources.AddUserFormRequirementsMessage, "", MessageBoxButton.OK);
                return;
            }

            User userToAdd = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Password = Password,
                UserType = SelectedType.Value.ToString(),
                Active = 1
            };
            dbContext.Users.Add(userToAdd);
            
            switch(SelectedType.Value)
            {
                case AccountTypesEnum.MEMBER:
                    Member newMember = new Member()
                    {
                        UserId = userToAdd.UserId,
                        User = userToAdd
                    };
                    dbContext.Members.Add(newMember);
                    break;

                case AccountTypesEnum.ACCOUNTANT:
                    Accountant newAccountant = new Accountant()
                    {
                        UserId = userToAdd.UserId,
                        User = userToAdd
                    };
                    dbContext.Accountants.Add(newAccountant);
                    break;

                case AccountTypesEnum.LIBRARIAN:
                    Librarian newLibrarian = new Librarian()
                    {
                        UserId = userToAdd.UserId,
                        User = userToAdd
                    };
                    dbContext.Librarians.Add(newLibrarian);
                    break;
            }
            try
            {
                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                MessageBox.Show(School_library.Resources.UserNotAddedError, School_library.Resources.AddLoanWindow_Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
          
            users.Add(new UserViewModel(userToAdd, dbContext));
            MessageBox.Show(School_library.Resources.UserAddedMessage, "", MessageBoxButton.OK);
            
        }
    }
}
