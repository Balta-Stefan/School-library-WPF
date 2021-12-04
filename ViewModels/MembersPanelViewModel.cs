using School_library.Commands;
using School_library.Models;
using School_library.Views;
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
    public class MembersPanelViewModel : ViewModelBase, IWindowWithFilter
    {
        private readonly mydbContext dbContext;

        private Collection<ResourceDictionary> resourceDictionaries;

        private Visibility CRUD_visibility;
        public Visibility CRUD_Visibility
        {
            get { return CRUD_visibility; }
        }
        private bool canEditUserActivity;
        public bool CanEditUserActivity
        { get { return canEditUserActivity; } }

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

        /*private int cardNumber = -1;
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
        }*/

        private ObservableCollection<AccountTypesEnum> types = new ObservableCollection<AccountTypesEnum>();
        public ObservableCollection<AccountTypesEnum> UserTypesList
        {
            get { return types; }
        }

        private AccountTypesEnum? selectedMemberType = null;
        public AccountTypesEnum? SelectedMemberType
        {
            get { return selectedMemberType; }
            set
            {
                selectedMemberType = value;
                /*if(value.Equals(User.UserTypes.MEMBER) == false)
                {
                    CardInputEnabled = false;
                    CardNumber = string.Empty;
                }
                else
                {
                    CardInputEnabled = true;
                }*/
                OnPropertyChange("SelectedMemberType");
            }
        }
        /*private bool cardInputEnabled = true;
        public bool CardInputEnabled
        {
            get { return cardInputEnabled; }
            set
            {
                cardInputEnabled = value;
                OnPropertyChange("CardInputEnabled");
            }
        }*/

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
        private int userID = -1;
        public string UserID
        {
            get 
            {
                if (userID == -1)
                    return string.Empty;
                return userID.ToString();
            }
            set
            {
                if(value.Equals(string.Empty))
                {
                    userID = -1;
                }
                else
                {
                    try
                    {
                        userID = Int32.Parse(value);
                    }
                    catch (Exception) { }
                }
                OnPropertyChange("UserID");
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
        public MembersPanelViewModel(mydbContext dbContext, Collection<ResourceDictionary> resourceDictionaries, AccountTypesEnum userType)
        {
            this.dbContext = dbContext;
            this.resourceDictionaries = resourceDictionaries;

            switch(userType)
            {
                case AccountTypesEnum.LIBRARIAN:
                    CRUD_visibility = Visibility.Visible;
                    canEditUserActivity = true;
                    break;
                default:
                    CRUD_visibility = Visibility.Hidden;
                    canEditUserActivity = false;
                    break;
            }

            IEnumerable<AccountTypesEnum> allTypes = Enum.GetValues(typeof(AccountTypesEnum)).Cast<AccountTypesEnum>();
            foreach (AccountTypesEnum t in allTypes) 
                types.Add(t);

            ClearFilters = new ClearMembersPanelFilters(this);
            FilterMembers = new FilterMembersCommand(this);
            AddMemberCommand = new OpenAddUserWindowCommand(this);


            foreach (User u in dbContext.Users.ToList()) users.Add(new UserViewModel(u, dbContext));
        }

        public void clearFilters()
        {
            FirstName = LastName = UserID = string.Empty;//CardNumber = string.Empty;
            SelectedMemberType = null;
            //CardInputEnabled = true;
            OnlyActiveMembersFilter = false;

            users.Clear();
            foreach (User u in dbContext.Users.ToList()) users.Add(new UserViewModel(u, dbContext));
        }

        private bool areFiltersEmpty()
        {
            if (firstName.Equals(string.Empty) &&
               lastName.Equals(string.Empty) &&
               //cardNumber.Equals(string.Empty) &&
               selectedMemberType == null &&
               UserID.Equals(string.Empty))
                return true;
            return false;
        }

        public void filter()
        {
            if (areFiltersEmpty() == true)
                return;

            List<User> allUsers = dbContext.Users.ToList();
            users.Clear();

            foreach(User u in allUsers)
            {
                dbContext.Entry(u).Reload();
                if (userID != -1 && u.UserId != userID)
                    continue;
                if (firstName.Equals(string.Empty) == false && u.FirstName.Equals(firstName) == false)
                    continue;
                if (lastName.Equals(string.Empty) == false && u.LastName.Equals(lastName) == false)
                    continue;
                if (onlyActiveMembers == true && u.Active == 0)
                    continue;
                /*if(CardNumber.Equals(string.Empty) == false)
                {
                    if (u.GetType() == typeof(Member))
                    {
                        Member mem = (Member)u;
                        if (mem.userID != cardNumber)
                            continue;
                    }
                    else
                        continue;
                }*/
                if (selectedMemberType != null && u.UserType.Equals(selectedMemberType.ToString()) == false)
                    continue;

                users.Add(new UserViewModel(u, dbContext));
            }

        }
    
        public void addMember()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel(dbContext, users);
            AddUserWindow addUserWindow = new AddUserWindow()
            {
                DataContext = addUserViewModel
            };
            foreach (var c in resourceDictionaries) addUserWindow.Resources.MergedDictionaries.Add(c);
            addUserWindow.ShowDialog();
        }
   
        private void userSelected()
        {

        }
    }
}
