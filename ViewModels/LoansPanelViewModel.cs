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
    public class LoansPanelViewModel : ViewModelBase, IWindowWithFilter
    {
        private readonly mydbContext dbContext;

        private readonly UserViewModel loggedInUser;

        private Collection<ResourceDictionary> resourceDictionary;

        private LoanViewModel? selectedLoan = null;

        private Visibility CRUD_visibility;
        public Visibility CRUD_Visibility
        {
            get { return CRUD_visibility; }
        }

        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;
        private int copyID = -1;
        private string bookTitle = string.Empty;
        private string isbn10 = string.Empty;
        private bool onlyActive = false;
        private bool onlyInactive = false;
        private bool onlyUnreturned = false;
        private MemberViewModel? selectedMember = null;
        private int userID = -1;
        private BookViewModel? selectedBook = null;
        private bool onlyReturnedFilter = false;

        #region Properties
        public bool OnlyReturnedFilter
        {
            get { return onlyReturnedFilter; }
            set
            {
                onlyReturnedFilter = value;
                OnPropertyChange("OnlyReturnedFilter");
            }
        }
        public ObservableCollection<MemberViewModel> members { get; private set; } = new ObservableCollection<MemberViewModel>();
        public ObservableCollection<BookViewModel> books { get; private set; } = new ObservableCollection<BookViewModel>();
        public BookViewModel? SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                isbn10 = value.Isbn10;
                bookTitle = value.BookTitle;
                OnPropertyChange("ISBN10");
                OnPropertyChange("SelectedBook");
                OnPropertyChange("BookTitle");
            }
        }

        public ObservableCollection<LoanViewModel> Loans { get; private set; } = new ObservableCollection<LoanViewModel>();
        public MemberViewModel? SelectedMember
        {
            get { return selectedMember; }
            set
            {
                selectedMember = value;
                if(value != null)
                {
                    userID = value.UserId;
                    firstNameFilter = value.FirstName;
                    lastNameFilter = value.LastName;
                    OnPropertyChange("FirstNameFilter");
                    OnPropertyChange("LastNameFilter");
                    OnPropertyChange("UserID");
                }
            }
        }
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
                        selectedMember = null;
                        firstNameFilter = lastNameFilter = string.Empty;
                        OnPropertyChange("SelectedMember");
                        OnPropertyChange("firstNameFilter");
                        OnPropertyChange("LastNameFilter");
                    }
                    catch (Exception) { }
                }
                OnPropertyChange("UserID");
            }
        }

        public LoanViewModel? SelectedLoan
        {
            get { return selectedLoan; }
            set
            {
                selectedLoan = value;
                OnPropertyChange("SelectedLoan");
            }
        }

        public ICommand clearLoansFiltersCommand { get; }
        public ICommand filterLoansCommand { get; }
        public ICommand openNewLoanWindowCommand { get; }
        public ICommand returnLoanCommand { get; }

        public string FirstNameFilter
        {
            get { return firstNameFilter; }
            set
            {
                selectedMember = null;
                firstNameFilter = value;
                userID = -1;
                OnPropertyChange("UserID");
                OnPropertyChange("FirstNameFilter");
                OnPropertyChange("SelectedMember");
            }
        }
        public string LastNameFilter
        {
            get { return lastNameFilter; }
            set
            {
                selectedMember = null;
                lastNameFilter = value;
                userID = -1;
                OnPropertyChange("UserID");
                OnPropertyChange("LastNameFilter");
                OnPropertyChange("SelectedMember");
            }
        }

        public string CopyID
        {
            get
            {
                if (copyID == -1)
                    return string.Empty;
                return copyID.ToString();
            }
            set
            {
                if (value.Equals(string.Empty))
                {
                    copyID = -1;
                }
                else
                {
                    try
                    {
                        copyID = Int32.Parse(value);
                    }
                    catch (Exception) { }
                }
                OnPropertyChange("CopyID");
            }
        }

        public string BookTitle
        {
            get { return bookTitle; }
            set
            {
                bookTitle = value;
                selectedBook = null;
                isbn10 = string.Empty;
                OnPropertyChange("BookTitle");
                OnPropertyChange("SelectedBook");
                OnPropertyChange("ISBN10");
            }
        }
        public string ISBN10
        {
            get { return isbn10; }
            set
            {
                isbn10 = value;
                bookTitle = string.Empty;
                selectedBook = null;
                OnPropertyChange("BookTitle");
                OnPropertyChange("SelectedBook");
                OnPropertyChange("ISBN10");
            }
        }

        public bool OnlyActive
        {
            get { return onlyActive; }
            set
            {
                onlyActive = value;
                onlyInactive = false;
                OnPropertyChange("OnlyInactive");
                OnPropertyChange("OnlyActive");
            }
        }
        public bool OnlyInactive
        {
            get { return onlyInactive; }
            set
            {
                onlyInactive = value;
                onlyActive = false;
                OnPropertyChange("OnlyInactive");
                OnPropertyChange("OnlyActive");
            }
        }
        public bool OnlyUnreturned
        {
            get { return onlyUnreturned; }
            set
            {
                onlyUnreturned = value;
                OnPropertyChange("OnlyUnreturned");
            }
        }

        
        #endregion

        public LoansPanelViewModel(mydbContext dbContext, UserViewModel loggedInUser, Collection<ResourceDictionary> resourceDictionary, AccountTypesEnum userType)
        {
            this.dbContext = dbContext;
            this.loggedInUser = loggedInUser;
            this.resourceDictionary = resourceDictionary;

            switch(userType)
            {
                case AccountTypesEnum.LIBRARIAN:
                    CRUD_visibility = Visibility.Visible;
                    break;
                default:
                    CRUD_visibility = Visibility.Hidden;
                    break;
            }

            foreach (Book b in dbContext.Books.ToList())
                books.Add(new BookViewModel(b));

            foreach (Loan l in dbContext.Loans.ToList())
                Loans.Add(new LoanViewModel(l, CRUD_visibility));

            foreach (Member m in dbContext.Members.ToList())
                members.Add(new MemberViewModel(m));
           

            clearLoansFiltersCommand = new ClearLoansFiltersCommand(this);
            filterLoansCommand = new FilterLoansCommand(this);
            openNewLoanWindowCommand = new OpenNewLoanWindowCommand(this);
            returnLoanCommand = new ReturnLoanCommand(this);
        }

        public void returnLoan(LoanViewModel selectedLoan)
        {
            selectedLoan.ReturnDateTIme = DateTime.Now;
            selectedLoan.ReturnedToLibrarian = loggedInUser.User.Librarian;
            
            try
            {
                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                MessageBox.Show("An error has occured while returning a loan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                selectedLoan.ReturnDateTIme = null;
                selectedLoan.ReturnedToLibrarian = null;
            }
        }

        public void openNewLoanWindow()
        {
            Librarian lib = loggedInUser.User.Librarian;

            AddNewLoanViewModel newLoanViewModel = new AddNewLoanViewModel(dbContext, books, Loans, new LibrarianViewModel(lib), CRUD_visibility);
            AddNewLoanWindow newLoanWindow = new AddNewLoanWindow()
            {
                DataContext = newLoanViewModel
            };
            newLoanWindow.Resources.MergedDictionaries.Clear();
            foreach (var c in resourceDictionary) newLoanWindow.Resources.MergedDictionaries.Add(c);
            newLoanWindow.ShowDialog();
        }

        public void clearFilters()
        {
            Loans.Clear();
            foreach (Loan l in dbContext.Loans.ToList()) Loans.Add(new LoanViewModel(l, CRUD_visibility));


            ISBN10 = BookTitle = CopyID = LastNameFilter = FirstNameFilter = UserID = string.Empty;
            OnlyActive = OnlyInactive = OnlyUnreturned = OnlyReturnedFilter = false;

            SelectedLoan = null;
            SelectedMember = null;
        }
        public void filter()
        {
            Loans.Clear();

            List<LoanViewModel> tempLoans = new List<LoanViewModel>();
            foreach (Loan l in dbContext.Loans.ToList())
            {
                dbContext.Entry(l).Reload();
                tempLoans.Add(new LoanViewModel(l, CRUD_visibility));
            }

            foreach (LoanViewModel l in tempLoans)
            {
                if (onlyReturnedFilter == true && l.ReturnedToLibrarian == null)
                    continue;
                if(selectedMember != null)
                {
                    if (l.Borrower.Equals(selectedMember.User) == false)
                        continue;
                }
                else
                {
                    if (userID != -1)
                    {
                        if (l.Borrower.UserId != userID)
                            continue;
                    }
                    else
                    {
                        if (firstNameFilter.Equals(string.Empty) == false && l.Borrower.User.FirstName.Equals(firstNameFilter) == false)
                            continue;
                        if (lastNameFilter.Equals(string.Empty) == false && l.Borrower.User.LastName.Equals(lastNameFilter) == false)
                            continue;
                    }
                 
                }
                if(selectedBook != null)
                {
                    if(selectedBook.Equals(l.BookCopy.Book) == false)
                        continue;
                }
                else
                {
                    if (copyID != -1 && l.BookCopy.BookCopyId != copyID)
                        continue;
                    if (isbn10.Equals(string.Empty) == false && l.BookCopy.Book.Isbn10.Equals(isbn10) == false)
                        continue;
                    if (bookTitle.Equals(string.Empty) == false && l.BookCopy.Book.BookTitle.Contains(bookTitle) == false)
                        continue;
                }

                if (onlyActive == true && l.Borrower.User.Active == 0)
                    continue;
                if (onlyInactive == true && l.Borrower.User.Active == 1)
                    continue;
                if (onlyUnreturned == true && l.ReturnedToLibrarian != null)
                    continue;

                Loans.Add(l);
            }
        }
    }
}
