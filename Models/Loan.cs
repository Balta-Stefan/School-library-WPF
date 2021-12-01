using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Loan
    {
        public int loanID { get; set; }
        public DateTime borrowDateTime { get; set; }
        public Librarian borrowedFromLibrarian { get; set; }
        public Member borrower { get; set; }
        public BookCopy bookCopy { get; set; }
        public Librarian? returnedToLibrarian { get; set; }
        public DateTime? returnDateTIme { get; set; }

        public Loan(int loanID, DateTime borrowDateTime, Librarian borrowedFromLibrarian, Member borrowerID, BookCopy bookCopy, Librarian? returnedToLibrarian = null, DateTime? returnDateTIme = null)
        {
            this.loanID = loanID;
            this.borrowDateTime = borrowDateTime;
            this.borrowedFromLibrarian = borrowedFromLibrarian;
            this.borrower = borrowerID;
            this.bookCopy = bookCopy;
            this.returnedToLibrarian = returnedToLibrarian;
            this.returnDateTIme = returnDateTIme;
        }

        public override bool Equals(object? obj)
        {
            return obj is Loan loan &&
                   loanID == loan.loanID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(loanID);
        }
    }
}
