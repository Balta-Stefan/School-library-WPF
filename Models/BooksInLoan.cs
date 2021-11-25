using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class BooksInLoan
    {
        public int loanID { get; private set; }
        public int bookCopyID { get; private set; }
        public Librarian returnedToLibrarian { get; private set; }
        public DateTime returnDateTime { get; private set; }

        public BooksInLoan(int loanID, int bookCopyID, Librarian returnedToLibrarian, DateTime returnDateTime)
        {
            this.loanID = loanID;
            this.bookCopyID = bookCopyID;
            this.returnedToLibrarian = returnedToLibrarian;
            this.returnDateTime = returnDateTime;
        }

        public override bool Equals(object? obj)
        {
            return obj is BooksInLoan loan &&
                   loanID == loan.loanID &&
                   bookCopyID == loan.bookCopyID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(loanID, bookCopyID);
        }
    }
}
