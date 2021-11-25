using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Loan
    {
        public int loanID { get; private set; }
        public DateTime borrowDateTime { get; private set; }
        public Librarian borrowedFromLibrarian { get; private set; }
        public Member borrower { get; private set; }

        public Loan(int loanID, DateTime borrowDateTime, Librarian borrowedFromLibrarian, Member borrowerID)
        {
            this.loanID = loanID;
            this.borrowDateTime = borrowDateTime;
            this.borrowedFromLibrarian = borrowedFromLibrarian;
            this.borrower = borrowerID;
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
