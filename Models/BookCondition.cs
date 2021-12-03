using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class BookCondition
    {
        public BookCondition()
        {
            BookCopies = new HashSet<BookCopy>();
        }

        public int ConditionId { get; set; }
        public string Condition { get; set; }

        public virtual ICollection<BookCopy> BookCopies { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BookCondition condition &&
                   ConditionId == condition.ConditionId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConditionId);
        }

        public override string ToString()
        {
            return Condition;
        }
    }
}
