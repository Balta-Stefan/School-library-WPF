using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class BookCopy
    {
        public BookCopy()
        {
            Loans = new HashSet<Loan>();
        }

        public int BookCopyId { get; set; }
        public int ConditionId { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public int BookId { get; set; }
        public byte Available { get; set; }

        public virtual Book Book { get; set; }
        public virtual BookCondition Condition { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BookCopy copy &&
                   BookCopyId == copy.BookCopyId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookCopyId);
        }

        public override string ToString()
        {
            return "ID: " + BookCopyId;
        }
    }
}
