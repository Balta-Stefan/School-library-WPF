using School_library.Commands;
using School_library.Models;
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

        private string cardNumber = string.Empty;
        public string CardNumber
        {
            get { return cardNumber; }
            set
            {
                cardNumber = value;
                OnPropertyChange("CardNumber");
            }
        }

        private ObservableCollection<MemberTypes> types = new ObservableCollection<MemberTypes>();
        public ObservableCollection<MemberTypes> MemberTypesList
        {
            get { return types; }
        }

        private MemberTypes? selectedMemberType = null;
        public MemberTypes? SelectedMemberType
        {
            get { return selectedMemberType; }
            set
            {
                selectedMemberType = value;
                if(value.Equals(MemberTypes.MEMBER) == false)
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
       
        public ICommand ClearFilters { get; }
        public MembersPanelViewModel()
        {
            IEnumerable<MemberTypes> allTypes = Enum.GetValues(typeof(MemberTypes)).Cast<MemberTypes>();
            foreach (MemberTypes t in allTypes) 
                types.Add(t);

            ClearFilters = new ClearMembersPanelFilters(this);
        }

        public void clearFilters()
        {
            FirstName = LastName = CardNumber = string.Empty;
            SelectedMemberType = null;
            CardInputEnabled = true;
        }
    }
}
