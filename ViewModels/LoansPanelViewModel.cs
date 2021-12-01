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
using System.Windows;
using System.Windows.Input;

namespace School_library.ViewModels
{
    public class LoansPanelViewModel : ViewModelBase
    {
        private readonly LoansDAO loanDao;
        private readonly UserDAO userDao;
        private readonly BookDAO bookDao;
        private readonly User loggedInUser;


        private LoanViewModel? selectedLoan = null;

        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;
        private int copyID = -1;
        private string bookTitle = string.Empty;
        private string isbn10 = string.Empty;
        private bool onlyActive = false;
        private bool onlyInactive = false;
        private bool onlyUnreturned = false;
        private User? selectedMember = null;
        private int userID = -1;
        private Book? selectedBook = null;

        #region Properties
        public ObservableCollection<User> members { get; } = new ObservableCollection<User>();
        public ObservableCollection<Book> books { get; }
        public Book? SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                isbn10 = value.ISBN10;
                bookTitle = value.BookTitle;
                OnPropertyChange("ISBN10");
                OnPropertyChange("SelectedBook");
                OnPropertyChange("BookTitle");
            }
        }

        public ObservableCollection<LoanViewModel> Loans { get; }
        public User? SelectedMember
        {
            get { return selectedMember; }
            set
            {
                selectedMember = value;
                if(value != null)
                {
                    userID = value.userID;
                    firstNameFilter = value.firstName;
                    lastNameFilter = value.lastName;
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

        public LoansPanelViewModel(LoansDAO loanDao, UserDAO userDao, BookDAO bookDao, User loggedInUser)
        {
            this.loanDao = loanDao;
            this.userDao = userDao;
            this.bookDao = bookDao;
            this.loggedInUser = loggedInUser;

            books = new ObservableCollection<Book>(bookDao.getBooks());

            Loans = new ObservableCollection<LoanViewModel>();
            foreach (Loan l in loanDao.getLoans())
                Loans.Add(new LoanViewModel(l, loanDao));

                List<User> users = userDao.getUsers();
            foreach(User u in users)
            {
                if (u.userType.Equals(User.UserTypes.MEMBER))
                    members.Add(u);
            }

            clearLoansFiltersCommand = new ClearLoansFiltersCommand(this);
            filterLoansCommand = new FilterLoansCommand(this);
            openNewLoanWindowCommand = new OpenNewLoanWindowCommand(this);
            returnLoanCommand = new ReturnLoanCommand(this);
        }

        public void returnLoan(LoanViewModel selectedLoan)
        {
            if(selectedLoan.returnLoan(DateTime.Now, (Librarian)loggedInUser) == false)
            {
                MessageBox.Show("An error has occured while returning a loan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void openNewLoanWindow()
        {
            AddNewLoanViewModel newLoanViewModel = new AddNewLoanViewModel((Librarian)loggedInUser, loanDao, userDao, books, Loans, bookDao);
            AddNewLoanWindow newLoanWindow = new AddNewLoanWindow()
            {
                DataContext = newLoanViewModel
            };
            newLoanWindow.ShowDialog();
        }

        public void clearFilters()
        {
            Loans.Clear();
            foreach (Loan l in loanDao.getLoans()) Loans.Add(new LoanViewModel(l, loanDao));


            ISBN10 = BookTitle = CopyID = LastNameFilter = FirstNameFilter = UserID = string.Empty;
            OnlyActive = OnlyInactive = OnlyUnreturned = false;

            SelectedLoan = null;
            SelectedMember = null;
        }
        public void filter()
        {
            Loans.Clear();

            List<LoanViewModel> tempLoans = new List<LoanViewModel>();
            foreach (Loan l in loanDao.getLoans()) tempLoans.Add(new LoanViewModel(l, loanDao));

            foreach (LoanViewModel l in tempLoans)
            {
                if(selectedMember != null)
                {
                    if (l.Borrower.Equals(selectedMember) == false)
                        continue;
                }
                else
                {
                    if (userID != -1)
                    {
                        if (l.Borrower.userID != userID)
                            continue;
                    }
                    else
                    {
                        if (firstNameFilter.Equals(string.Empty) == false && l.Borrower.firstName.Equals(firstNameFilter) == false)
                            continue;
                        if (lastNameFilter.Equals(string.Empty) == false && l.Borrower.lastName.Equals(lastNameFilter) == false)
                            continue;
                    }
                 
                }
                if(selectedBook != null)
                {
                    if (l.BookCopy.book.Equals(selectedBook) == false)
                        continue;
                }
                else
                {
                    if (copyID != -1 && l.BookCopy.bookCopyID != copyID)
                        continue;
                    if (isbn10.Equals(string.Empty) == false && l.BookCopy.book.ISBN10.Equals(isbn10) == false)
                        continue;
                    if (bookTitle.Equals(string.Empty) == false && l.BookCopy.book.BookTitle.Equals(bookTitle) == false)
                        continue;
                }

                if (onlyActive == true && l.Borrower.active == false)
                    continue;
                if (onlyInactive == true && l.Borrower.active == true)
                    continue;
                if (onlyUnreturned == true && l.ReturnedToLibrarian != null)
                    continue;

                Loans.Add(l);
            }
        }
    }
}
