using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class BookCopy
    {
        public int bookCopyID { get; private set; }
        public BookCondition condition { get; private set; }
        public DateTime deliveredAt { get; private set; }
        public Book book { get; private set; }

        public BookCopy(int bookCopyID, BookCondition condition, DateTime deliveredAt, Book book)
        {
            this.bookCopyID = bookCopyID;
            this.condition = condition;
            this.deliveredAt = deliveredAt;
            this.book = book;
        }

        public override bool Equals(object? obj)
        {
            return obj is BookCopy copy &&
                   bookCopyID == copy.bookCopyID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(bookCopyID);
        }
    }
}
