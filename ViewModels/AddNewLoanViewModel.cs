using School_library.Commands;
using School_library.DAO;
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
        private Member? selectedMember;
        private Book? selectedBook;
        private BookCopy? selectedCopy;
        private Librarian loggedInLibrarian;
        private LoansDAO loanDao;
        private UserDAO userDao;
        private BookDAO bookDao;
        private ObservableCollection<LoanViewModel> loans;

        #region Properties
        public Member? SelectedMember
        {
            get { return selectedMember; }
            set
            {
                // if the selected member is inactive, disable the button and display a message
                if(value != null && value.active == false)
                {
                    MessageBox.Show("Selected member is inactive", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    selectedMember = null;
                    OnPropertyChange("SelectedMember");
                    return;
                }

                selectedMember = value;
                OnPropertyChange("SelectedMember");
            }
        }

        public Book? SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChange("SelectedBook");

                Copies.Clear();
                if(value != null)
                {
                    foreach (BookCopy bc in bookDao.getBookCopies(value))
                    {
                        if(bc.available == true)
                            Copies.Add(bc);
                    }
                }
            }
        }

        public BookCopy? SelectedCopy
        {
            get { return selectedCopy; }
            set
            {
                selectedCopy = value;
                OnPropertyChange("SelectedCopy");
            }
        }
        public ObservableCollection<Member> Members { get; } = new ObservableCollection<Member>();
        public ObservableCollection<Book> Books { get; }
        public ObservableCollection<BookCopy> Copies { get; } = new ObservableCollection<BookCopy>();
        public ICommand addLoanCommand { get; }
        #endregion

        public AddNewLoanViewModel(Librarian loggedInLibrarian, LoansDAO loanDao, UserDAO userDao, ObservableCollection<Book> books, ObservableCollection<LoanViewModel> loans, BookDAO bookDao)
        {
            this.loggedInLibrarian = loggedInLibrarian;
            this.loanDao = loanDao;
            this.userDao = userDao;
            this.loans = loans;
            this.bookDao = bookDao;

            addLoanCommand = new AddLoanCommand(this);

            Books = books;
            foreach(User u in userDao.getUsers())
            {
                if (u is Member)
                    Members.Add((Member)u);
            }
        }

        public void addLoan()
        {
            if(selectedMember == null)
            {
                MessageBox.Show("You must select a member", "", MessageBoxButton.OK);
                return;
            }
            if(selectedCopy == null)
            {
                MessageBox.Show("You must select a copy", "", MessageBoxButton.OK);
                return;
            }

            Loan? newLoan = new Loan(-1, DateTime.Now, loggedInLibrarian, selectedMember, selectedCopy);
            newLoan = loanDao.addLoan(newLoan);

            if(newLoan == null)
            {
                MessageBox.Show("An error has occured while adding a loan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                loans.Add(new LoanViewModel(newLoan, loanDao));
                MessageBox.Show("A loan has been added", "Success", MessageBoxButton.OK);

                SelectedBook = null;
                SelectedCopy = null;
                SelectedMember = null;
            }
        }
    }
}
