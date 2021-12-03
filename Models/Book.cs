using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace School_library.Models
{
    public partial class Book
    {
        public Book()
        {
            BookCopies = new HashSet<BookCopy>();
        }

        public int BookId { get; set; }
        public string Isbn13 { get; set; }
        public string Isbn10 { get; set; }
        public string BookTitle { get; set; }
        public short? Edition { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int Genre { get; set; }
        public int? NumberOfCopies { get; set; }

        public virtual Author Author { get; set; }
        public virtual Genre GenreNavigation { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookCopy> BookCopies { get; set; }

       
        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   BookId == book.BookId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookId);
        }

        public override string ToString()
        {
            return BookTitle;
        }
    }
}
