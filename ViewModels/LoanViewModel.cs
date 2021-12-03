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

        /*private int loanID;
        private DateTime borrowDateTime;
        private Librarian borrowedFromLibrarian;
        private Member borrower;
        private BookCopy bookCopy;
        private Librarian? returnedToLibrarian;
        private DateTime? returnDateTIme;*/
        private bool isReturned = false;

        #region Properties
        public string BorrowedFromLibrarianFullName
        {
            get { return loan.BorrowedFromLibrarianNavigation.User.FirstName + " " + loan.BorrowedFromLibrarianNavigation.User.LastName; }
        }
       
        public string ReturnedToLibrarianFullName
        {
            get 
            {
                if (isReturned == false)
                    return string.Empty;
                return loan.ReturnedToLibrarianNavigation.User.FirstName + " " + loan.ReturnedToLibrarianNavigation.User.LastName;
            }
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
            get { return loan.LoanId; }
            set
            {
                loan.LoanId = value;
                OnPropertyChange("LoanID");
            }
        }

        public int UserID
        {
            get { return loan.BorrowerId; }
        }
        public DateTime BorrowDateTime
        {
            get { return loan.BorrowDateTime; }
            set
            {
                loan.BorrowDateTime = value;
                OnPropertyChange("BorrowDateTime");
            }
        }

        public Librarian BorrowedFromLibrarian
        {
            get { return loan.BorrowedFromLibrarianNavigation; }
            set
            {
                loan.BorrowedFromLibrarianNavigation = value;
                OnPropertyChange("BorrowedFromLibrarian");
            }
        }

        public string BorrowerFirstName
        {
            get { return loan.Borrower.User.FirstName; }
        }
        public string BorrowerLastName
        {
            get { return loan.Borrower.User.LastName; }
        }
        public Member Borrower
        {
            get { return loan.Borrower.User.Member; }
            set
            {
                loan.Borrower = value;
                OnPropertyChange("Borrower");


            }
        }

        public BookCopy BookCopy
        {
            get { return loan.BookCopy; }
            set
            {
                loan.BookCopy = value;
                OnPropertyChange("BookCopy");


            }
        }
        public Librarian? ReturnedToLibrarian
        {
            get { return loan.ReturnedToLibrarianNavigation; }
            set
            {
                loan.ReturnedToLibrarianNavigation = value;
                if (value != null)
                    isReturned = true;
                else
                    isReturned = false;

                OnPropertyChange("ReturnedToLibrarian");
                OnPropertyChange("ReturnedToLibrarianFullName");

            }
        }

        public DateTime? ReturnDateTIme
        {
            get { return loan.ReturnDateTime; }
            set
            {
                loan.ReturnDateTime = value;
                OnPropertyChange("ReturnDateTIme");

            }
        }
        #endregion

        /*public bool returnLoan(DateTime returnedDateTIme, Librarian returnedTo)
        {
            loan.ReturnDateTime = returnedDateTIme;
            loan.ReturnedToLibrarianNavigation = returnedTo;

            try
            {
                dbContext.SaveChanges();
                returnDateTIme = returnedDateTIme;
                returnedToLibrarian = returnedTo;
                OnPropertyChange("ReturnDateTIme");
                OnPropertyChange("ReturnedToLibrarian");
                return true;
            }
            catch (Exception)
            {
                loan.ReturnedToLibrarianNavigation = returnedToLibrarian;
                loan.ReturnDateTime = returnDateTIme;
                return false;
            }
        }*/

        public LoanViewModel(Loan loan)
        {
            this.loan = loan;

            if (loan.ReturnDateTime != null)
                isReturned = true;
        }
    }
}
