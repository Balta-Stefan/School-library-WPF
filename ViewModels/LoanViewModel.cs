using School_library.DAO;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class LoanViewModel : ViewModelBase
    {
        private readonly Loan loan;
        private readonly LoansDAO loansDao;

        private int loanID;
        private DateTime borrowDateTime;
        private Librarian borrowedFromLibrarian;
        private Member borrower;
        private BookCopy bookCopy;
        private Librarian? returnedToLibrarian;
        private DateTime? returnDateTIme;
        private bool isReturned = false;

        #region Properties
        public string BorrowedFromLibrarianFullName
        {
            get {return borrowedFromLibrarian.firstName + " " + borrowedFromLibrarian.lastName; }
        }
        public string ReturnedToLibrarianFullName
        {
            get { return returnedToLibrarian?.firstName + " " + returnedToLibrarian?.lastName; }
        }
        public bool IsReturned
        {
            get { return isReturned; }
            set
            {
                isReturned = value;
                OnPropertyChange("IsReturned");
            }
        }

        public int LoanID
        {
            get { return loanID; }
            set
            {
                loan.loanID = value;
                if(loansDao.updateLoan(loan) == true)
                {
                    loanID = value;
                    OnPropertyChange("LoanID");
                }
                else
                {
                    loan.loanID = loanID;
                }
            }
        }

        public DateTime BorrowDateTime
        {
            get { return borrowDateTime; }
            set
            {
                loan.borrowDateTime = value;
                if (loansDao.updateLoan(loan) == true)
                {
                    borrowDateTime = value;
                    OnPropertyChange("BorrowDateTime");
                }
                else
                {
                    loan.borrowDateTime = borrowDateTime;
                }
            }
        }

        public Librarian BorrowedFromLibrarian
        {
            get { return borrowedFromLibrarian; }
            set
            {
                loan.borrowedFromLibrarian = value;
                if (loansDao.updateLoan(loan) == true)
                {
                    borrowedFromLibrarian = value;
                    OnPropertyChange("BorrowedFromLibrarian");
                }
                else
                {
                    loan.borrowedFromLibrarian = borrowedFromLibrarian;
                }
            }
        }

        public Member Borrower
        {
            get { return borrower; }
            set
            {
                loan.borrower = value;
                if (loansDao.updateLoan(loan) == true)
                {
                    borrower = value;
                    OnPropertyChange("Borrower");
                }
                else
                {
                    loan.borrower = borrower;
                }
            }
        }

        public BookCopy BookCopy
        {
            get { return bookCopy; }
            set
            {
                loan.bookCopy = value;
                if (loansDao.updateLoan(loan) == true)
                {
                    bookCopy = value;
                    OnPropertyChange("BookCopy");
                }
                else
                {
                    loan.bookCopy = bookCopy;
                }
            }
        }
        public Librarian? ReturnedToLibrarian
        {
            get { return returnedToLibrarian; }
            set
            {
                loan.returnedToLibrarian = value;
                if (loansDao.updateLoan(loan) == true)
                {
                    returnedToLibrarian = value;
                    OnPropertyChange("ReturnedToLibrarian");
                }
                else
                {
                    loan.returnedToLibrarian = returnedToLibrarian;
                }
            }
        }

        public DateTime? ReturnDateTIme
        {
            get { return returnDateTIme; }
            set
            {
                loan.returnDateTIme = value;
                if (loansDao.updateLoan(loan) == true)
                {
                    returnDateTIme = value;
                    OnPropertyChange("ReturnDateTIme");
                }
                else
                {
                    loan.returnDateTIme = returnDateTIme;
                }
            }
        }
        #endregion

        public bool returnLoan(DateTime returnedDateTIme, Librarian returnedTo)
        {
            loan.returnDateTIme = returnedDateTIme;
            loan.returnedToLibrarian = returnedTo;

            if(loansDao.updateLoan(loan) == true)
            {
                returnDateTIme = returnedDateTIme;
                returnedToLibrarian = returnedTo;
                OnPropertyChange("ReturnDateTIme");
                OnPropertyChange("ReturnedToLibrarian");
                return true;
            }
            else
            {
                loan.returnedToLibrarian = returnedToLibrarian;
                loan.returnDateTIme = returnDateTIme;
                return false;
            }
        }

        public LoanViewModel(Loan loan, LoansDAO loansDao)
        {
            this.loan = loan;
            this.loansDao = loansDao;

            loanID = loan.loanID;
            borrowDateTime = loan.borrowDateTime;
            borrowedFromLibrarian = loan.borrowedFromLibrarian;
            borrower = loan.borrower;
            bookCopy = loan.bookCopy;
            returnedToLibrarian = loan.returnedToLibrarian;
            returnDateTIme = loan.returnDateTIme;
            if (ReturnedToLibrarian != null)
                isReturned = true;
        }
    }
}
