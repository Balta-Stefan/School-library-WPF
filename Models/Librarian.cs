using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class Librarian
    {
        public Librarian()
        {
            LoanBorrowedFromLibrarianNavigations = new HashSet<Loan>();
            LoanReturnedToLibrarianNavigations = new HashSet<Loan>();
        }

        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Loan> LoanBorrowedFromLibrarianNavigations { get; set; }
        public virtual ICollection<Loan> LoanReturnedToLibrarianNavigations { get; set; }

        public override bool Equals(object obj)
        {
            return User.Equals(obj);
        }
        public override int GetHashCode()
        {
            return User.GetHashCode();
        }
        public override string ToString()
        {
            return User.ToString();
        }
    }
}
