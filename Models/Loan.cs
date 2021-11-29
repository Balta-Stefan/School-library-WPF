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
        public Librarian? returnedToLibrarian { get; set; } = null;
        public DateTime? returnDateTIme { get; set; } = null;

        public Loan(int loanID, DateTime borrowDateTime, Librarian borrowedFromLibrarian, Member borrowerID, BookCopy bookCopy)
        {
            this.loanID = loanID;
            this.borrowDateTime = borrowDateTime;
            this.borrowedFromLibrarian = borrowedFromLibrarian;
            this.borrower = borrowerID;
            this.bookCopy = bookCopy;
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
