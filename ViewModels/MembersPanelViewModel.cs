using School_library.Commands;
using School_library.DAO;
using School_library.Models;
using School_library.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.ViewModels
{
    public class MembersPanelViewModel : ViewModelBase
    {
        private UserDAO userDao;

        private string firstName = string.Empty;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChange("FirstName");
            }
        }

        private string lastName = string.Empty;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChange("LastName");
            }
        }

        private int cardNumber = -1;
        public string CardNumber
        {
            get 
            {
                if (cardNumber == -1)
                    return string.Empty;
                else
                    return cardNumber.ToString();
            }
            set
            {
                if(value.Equals(string.Empty))
                {
                    cardNumber = -1;
                }
                else
                {
                    try
                    {
                        cardNumber = Int32.Parse(value);
                    }
                    catch (Exception) { }
                }
               
                OnPropertyChange("CardNumber");
            }
        }

        private ObservableCollection<User.UserTypes> types = new ObservableCollection<User.UserTypes>();
        public ObservableCollection<User.UserTypes> UserTypesList
        {
            get { return types; }
        }

        private User.UserTypes? selectedMemberType = null;
        public User.UserTypes? SelectedMemberType
        {
            get { return selectedMemberType; }
            set
            {
                selectedMemberType = value;
                if(value.Equals(User.UserTypes.MEMBER) == false)
                {
                    CardInputEnabled = false;
                    CardNumber = string.Empty;
                }
                else
                {
                    CardInputEnabled = true;
                }
                OnPropertyChange("SelectedMemberType");
            }
        }
        private bool cardInputEnabled = true;
        public bool CardInputEnabled
        {
            get { return cardInputEnabled; }
            set
            {
                cardInputEnabled = value;
                OnPropertyChange("CardInputEnabled");
            }
        }

        private bool onlyActiveMembers = false;
        public bool OnlyActiveMembersFilter
        {
            get { return onlyActiveMembers; }
            set
            {
                onlyActiveMembers = value;
                OnPropertyChange("OnlyActiveMembersFilter");
            }
        }
        
        private UserViewModel? selectedUser = null;
        public UserViewModel? SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                userSelected();
                OnPropertyChange("SelectedUser");
            }
        }

        public ICommand ClearFilters { get; }
        public ICommand FilterMembers { get; }

        public OpenAddUserWindowCommand AddMemberCommand { get; }

        private ObservableCollection<UserViewModel> users = new ObservableCollection<UserViewModel>();
        public ObservableCollection<UserViewModel> Users
        {
            get { return users; }
        }
        public MembersPanelViewModel(UserDAO userDao)
        {
            IEnumerable<User.UserTypes> allTypes = Enum.GetValues(typeof(User.UserTypes)).Cast<User.UserTypes>();
            foreach (User.UserTypes t in allTypes) 
                types.Add(t);

            ClearFilters = new ClearMembersPanelFilters(this);
            FilterMembers = new FilterMembersCommand(this);
            AddMemberCommand = new OpenAddUserWindowCommand(this, userDao);

            this.userDao = userDao;

            foreach (User u in userDao.getUsers()) users.Add(new UserViewModel(u, userDao));
        }

        public void clearFilters()
        {
            FirstName = LastName = CardNumber = string.Empty;
            SelectedMemberType = null;
            CardInputEnabled = true;
            OnlyActiveMembersFilter = false;

            List<User> allUsers = userDao.getUsers();
            users.Clear();
            foreach (User u in allUsers) users.Add(new UserViewModel(u, userDao));
        }

        private bool areFiltersEmpty()
        {
            if (firstName.Equals(string.Empty) &&
               lastName.Equals(string.Empty) &&
               cardNumber.Equals(string.Empty) &&
               selectedMemberType == null)
                return true;
            return false;
        }

        public void filterMembers()
        {
            if (areFiltersEmpty() == true)
                return;

            List<User> allUsers = userDao.getUsers();
            users.Clear();

            foreach(User u in allUsers)
            {
                if (firstName.Equals(string.Empty) == false && u.firstName.Equals(firstName) == false)
                    continue;
                if (lastName.Equals(string.Empty) == false && u.lastName.Equals(lastName) == false)
                    continue;
                if (onlyActiveMembers == true && u.active == false)
                    continue;
                if(CardNumber.Equals(string.Empty) == false)
                {
                    if (u.GetType() == typeof(Member))
                    {
                        Member mem = (Member)u;
                        if (mem.userID != cardNumber)
                            continue;
                    }
                    else
                        continue;
                }
                if (selectedMemberType != null && u.userType.Equals(selectedMemberType) == false)
                    continue;

                users.Add(new UserViewModel(u, userDao));
            }

        }
    
        public void addMember()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel(userDao, users);
            AddUserWindow addUserWindow = new AddUserWindow()
            {
                DataContext = addUserViewModel
            };
            addUserWindow.ShowDialog();
        }
   
        private void userSelected()
        {

        }
    }
}
