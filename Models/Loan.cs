using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class Loan
    {
        public int LoanId { get; set; }
        public DateTime BorrowDateTime { get; set; }
        public int BorrowedFromLibrarian { get; set; }
        public int BorrowerId { get; set; }
        public int BookCopyId { get; set; }
        public int? ReturnedToLibrarian { get; set; }
        public DateTime? ReturnDateTime { get; set; }

        public virtual BookCopy BookCopy { get; set; }
        public virtual Librarian BorrowedFromLibrarianNavigation { get; set; }
        public virtual Member Borrower { get; set; }
        public virtual Librarian ReturnedToLibrarianNavigation { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Loan loan &&
                   LoanId == loan.LoanId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LoanId);
        }
    }
}
