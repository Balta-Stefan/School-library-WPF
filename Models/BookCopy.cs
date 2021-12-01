using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class BookCopy
    {
        public int bookCopyID { get; set; }
        public BookCondition condition { get; set; }
        public DateTime deliveredAt { get; set; }
        public Book book { get; set; }
        public bool available { get; set; }

        public BookCopy(int bookCopyID, BookCondition condition, DateTime deliveredAt, Book book, bool available)
        {
            this.bookCopyID = bookCopyID;
            this.condition = condition;
            this.deliveredAt = deliveredAt;
            this.book = book;
            this.available = available;
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

        public override string ToString()
        {
            return bookCopyID.ToString();
        }
    }
}
