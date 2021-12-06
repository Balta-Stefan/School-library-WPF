using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private Book book;

        public Book Book
        {
            get { return book; }
        }
        public int BookId 
        {
            get { return book.BookId; }
        }
        public string Isbn13 
        {
            get { return book.Isbn13; }
            set
            {
                book.Isbn13 = value;
                OnPropertyChange("Isbn13");
            }
        }
        public string Isbn10
        {
            get { return book.Isbn10; }
            set
            {
                book.Isbn10 = value;
                OnPropertyChange("Isbn10");
            }
        }
        public string BookTitle
        {
            get { return book.BookTitle; }
            set
            {
                book.BookTitle = value;
                OnPropertyChange("BookTitle");
            }
        }
        public short? Edition
        {
            get { return book.Edition; }
            set
            {
                book.Edition = value;
                OnPropertyChange("Edition");
            }
        }
        public int AuthorId
        {
            get { return book.AuthorId; }
            set
            {
                book.AuthorId = value;
                OnPropertyChange("AuthorId");
            }
        }
        public int PublisherId
        {
            get { return book.PublisherId; }
            set
            {
                book.PublisherId = value;
                OnPropertyChange("PublisherId");
            }
        }
        public int Genre
        {
            get { return book.Genre; }
            set
            {
                book.Genre = value;
                OnPropertyChange("Genre");
            }
        }
        public int? NumberOfCopies
        {
            get { return book.NumberOfCopies; }
            set
            {
                book.NumberOfCopies = value;
                OnPropertyChange("NumberOfCopies");
            }
        }

        public Author Author
        {
            get { return book.Author; }
            set
            {
                book.Author = value;
                OnPropertyChange("Author");
            }
        }
        public Genre GenreNavigation
        {
            get { return book.GenreNavigation; }
            set
            {
                book.GenreNavigation = value;
                OnPropertyChange("GenreNavigation");
            }
        }
        public Publisher Publisher
        {
            get { return book.Publisher; }
            set
            {
                book.Publisher = value;
                OnPropertyChange("Publisher");
            }
        }
        public ICollection<BookCopy> BookCopies { get { return book.BookCopies; } }

        public BookViewModel(Book book)
        {
            this.book = book;
        }

        public override string ToString()
        {
            return book.ToString();
        }

        public override bool Equals(object? obj)
        {
            return book.Equals(obj);
        }

        public override int GetHashCode()
        {
            return book.GetHashCode();
        }
    }
}
