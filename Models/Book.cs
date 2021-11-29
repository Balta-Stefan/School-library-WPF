using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Book : ModelBase
    {
        private int bookID;
        public int BookID
        {
            get { return bookID; }
            set
            {
                bookID = value;
                OnPropertyChanged("BookID");
            }
        }
        private string isbn13;
        public string ISBN13
        {
            get { return isbn13; }
            set
            {
                isbn13 = value;
                OnPropertyChanged("ISBN13");
            }
        }
        private string isbn10;
        public string ISBN10
        {
            get { return isbn10; }
            set
            {
                isbn10 = value;
                OnPropertyChanged("ISBN10");
            }
        }
        private string bookTitle;
        public string BookTitle
        {
            get { return bookTitle; }
            set
            {
                bookTitle = value;
                OnPropertyChanged("BookTitle");
            }
        }
        private short edition;
        public short Edition
        {
            get { return edition; }
            set
            {
                edition = value;
                OnPropertyChanged("Edition");
            }
        }
        private Author author;
        public Author Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }
        private Publisher publisher;
        public Publisher Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                OnPropertyChanged("Publisher");
            }
        }
        private Genre genre;
        public Genre Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                OnPropertyChanged("Genre");
            }
        }

        private int numberOfCopies;
        public int NumberOfCopies
        {
            get { return numberOfCopies; }
            set
            {
                numberOfCopies = value;
                OnPropertyChanged("NumberOfCopies");
            }
        }

        public Book(int bookID, string isbn13, string isbn10, string bookTitle, short edition, Author author, Publisher publisher, Genre genre, int numberOfCopies)
        {
            this.bookID = bookID;
            this.isbn13 = isbn13;
            this.isbn10 = isbn10;
            this.bookTitle = bookTitle;
            this.edition = edition;
            this.author = author;
            this.publisher = publisher;
            this.genre = genre;
            this.numberOfCopies = numberOfCopies;
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

        public override string ToString()
        {
            return bookTitle;
        }
    }
}
