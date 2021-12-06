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
    public class AddNewLoanViewModel : ViewModelBase
    {
        private MemberViewModel? selectedMember;
        private BookViewModel? selectedBook;
        private BookCopyViewModel? selectedCopy;
        private LibrarianViewModel loggedInLibrarian;
        private ObservableCollection<LoanViewModel> loans;

        private string filterBookValue;

        private readonly mydbContext dbContext;

        private Visibility CRUD_visibility;
        public Visibility CRUD_Visibility
        {
            get { return CRUD_visibility; }
        }
        #region Properties
        public MemberViewModel? SelectedMember
        {
            get { return selectedMember; }
            set
            {
                // if the selected member is inactive, disable the button and display a message
                if(value != null && value.User.Active == 0)
                {
                    MessageBox.Show(School_library.Resources.AddLoanWindow_userInactiveMessage, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    selectedMember = null;
                    OnPropertyChange("SelectedMember");
                    return;
                }

                selectedMember = value;
                OnPropertyChange("SelectedMember");
            }
        }
        public string FilterBookValue
        {
            get { return filterBookValue; }
            set
            {
                filterBookValue = value;
                SelectedBook = null;
                Books.Clear();
                if(value.Equals(string.Empty))
                {
                    foreach (Book b in dbContext.Books.ToList())
                        Books.Add(new BookViewModel(b));
                }
                else
                {
                    foreach (Book b in dbContext.Books.Where(b => b.BookTitle.Contains(value)))
                        Books.Add(new BookViewModel(b));
                }
            }
        }
        public BookViewModel? SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChange("SelectedBook");

                Copies.Clear();
                if(value != null)
                {
                    var tmp = dbContext.BookCopies.Where(c => c.Available == 1).ToList();
                    foreach (BookCopy c in tmp)
                    {
                        //dbContext.Entry(c).Reload();
                        Copies.Add(new BookCopyViewModel(c));
                    }
                }
            }
        }

        public BookCopyViewModel? SelectedCopy
        {
            get { return selectedCopy; }
            set
            {
                selectedCopy = value;
                OnPropertyChange("SelectedCopy");
            }
        }
        public ObservableCollection<MemberViewModel> Members { get; private set; } = new ObservableCollection<MemberViewModel>();
        public ObservableCollection<BookViewModel> Books { get; } = new ObservableCollection<BookViewModel>();
        public ObservableCollection<BookCopyViewModel> Copies { get; } = new ObservableCollection<BookCopyViewModel>();
        public ICommand addLoanCommand { get; }
        #endregion

        public AddNewLoanViewModel(mydbContext dbContext, ObservableCollection<LoanViewModel> loans, LibrarianViewModel loggedInLibrarian, Visibility CRUD_visibility)
        {
            this.loggedInLibrarian = loggedInLibrarian;
            this.loans = loans;
            this.dbContext = dbContext;
            this.CRUD_visibility = CRUD_visibility;

            addLoanCommand = new AddLoanCommand(this);

            var mems = dbContext.Members.Where(m => m.User.Active == 1).ToList();

            foreach (Member m in mems)
                Members.Add(new MemberViewModel(m));
            foreach (Book b in dbContext.Books.ToList())
                Books.Add(new BookViewModel(b));
        }

        public void addLoan()
        {
            if(selectedMember == null)
            {
                MessageBox.Show(School_library.Resources.AddLoanWindow_mustSelectMember, "", MessageBoxButton.OK);
                return;
            }
            if(selectedCopy == null)
            {
                MessageBox.Show(School_library.Resources.AddLoanWindow_mustSelectCopy, "", MessageBoxButton.OK);
                return;
            }

            Loan newLoan = new Loan()
            {
                BorrowDateTime = DateTime.Now,
                BorrowedFromLibrarianNavigation = loggedInLibrarian.User.Librarian,
                Borrower = selectedMember.User.Member,
                BookCopy = selectedCopy.BookCopy
            };
            dbContext.Loans.Add(newLoan);

            try
            {
                dbContext.SaveChanges();
            }

            catch(Exception)
            {
                MessageBox.Show(School_library.Resources.AddLoanWindow_ErrorWhileAdding, School_library.Resources.AddLoanWindow_Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
    
            loans.Add(new LoanViewModel(newLoan, CRUD_visibility));
            MessageBox.Show(School_library.Resources.AddLoanWindow_LoanAdded, School_library.Resources.AddLoanWindow_LoanSuccess, MessageBoxButton.OK);

            SelectedBook = null;
            SelectedCopy = null;
            SelectedMember = null;
        }
    }
}
