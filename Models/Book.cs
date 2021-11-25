using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Book
    {
        public int bookID { get; private set; }
        public string isbn13 { get; private set; }
        public string isbn10 { get; private set; }
        public string bookTitle { get; private set; }
        public short edition { get; private set; }
        public Author author { get; private set; }
        public Publisher publisher { get; private set; }

        public Book(int bookID, string isbn13, string isbn10, string bookTitle, short edition, Author author, Publisher publisher)
        {
            this.bookID = bookID;
            this.isbn13 = isbn13;
            this.isbn10 = isbn10;
            this.bookTitle = bookTitle;
            this.edition = edition;
            this.author = author;
            this.publisher = publisher;
        }

        public override bool Equals(object? obj)
        {
            return obj is Book book &&
                   bookID == book.bookID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(bookID);
        }
    }
}
