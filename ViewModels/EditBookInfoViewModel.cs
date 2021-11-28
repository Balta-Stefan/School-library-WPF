using School_library.Commands;
using School_library.DAO;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace School_library.ViewModels
{
    public class EditBookInfoViewModel
    {
        public ObservableCollection<BookCopy> bookCopies { get; }
        private Book book;
        private BookDAO bookDao;

        private string bookTitle;
        public string BookTitle
        {
            get { return bookTitle; }
            set
            {
                bookTitle = value;
            }
        }

        private string isbn10;
        public string ISBN10
        {
            get { return isbn10; }
            set
            {
                isbn10 = value;
            }
        }
        private string isbn13;
        public string ISBN13
        {
            get { return isbn13; }
            set
            {
                isbn13 = value;
            }
        }
        private short edition;
        public string Edition
        {
            get { return edition.ToString(); }
            set
            {
                try
                {
                    edition = short.Parse(value);
                }
                catch (Exception) { }
            }
        }

        public ObservableCollection<Genre> genres { get; }
        public ObservableCollection<Publisher> publishers { get; }
        public ObservableCollection<Author> authors { get; }

        private int selectedCopyIndex = -1;
        public int SelectedCopyIndex
        {
            get { return selectedCopyIndex; }
            set
            {
                selectedCopyIndex = value;
                copySelected = true;
                deleteCopyCommand.executeChanged();
            }
        }

        private int selectedGenreIndex = -1;
        public int SelectedGenreIndex
        {
            get { return selectedGenreIndex; }
            set { selectedGenreIndex = value; }
        }

        private int selectedAuthorIndex = -1;
        public int SelectedAuthorIndex
        {
            get { return selectedAuthorIndex; }
            set { selectedAuthorIndex = value; }
        }

        private int selectedPublisherIndex = -1;
        public int SelectedPublisherIndex
        {
            get { return selectedPublisherIndex; }
            set { selectedPublisherIndex = value; }
        }

        public DeleteBookCopyCommand deleteCopyCommand { get; }
        public ICommand editBookInfoCommand { get; }
        public bool copySelected { get; private set; } = false;
        public EditBookInfoViewModel(BookDAO bookDao, GenreDAO genreDAO, PublisherDAO publisherDao, AuthorDAO authorDAO, Book book)
        {
            this.bookDao = bookDao;
            this.book = book;
            this.bookCopies  = new ObservableCollection<BookCopy>(bookDao.getBookCopies(book));
            this.genres = new ObservableCollection<Genre>(genreDAO.getGenres());
            this.publishers = new ObservableCollection<Publisher>(publisherDao.getPublishers());
            this.authors = new ObservableCollection<Author>(authorDAO.getAuthors());

            selectedGenreIndex = genres.IndexOf(book.Genre);
            selectedAuthorIndex = authors.IndexOf(book.Author);
            selectedPublisherIndex = publishers.IndexOf(book.Publisher);

            deleteCopyCommand = new DeleteBookCopyCommand(this);
            editBookInfoCommand = new EditBookInfoCommand(this);

            isbn10 = book.ISBN10;
            isbn13 = book.ISBN13;
            bookTitle = book.BookTitle;
            edition = book.Edition;
        }

        public void deleteCopy()
        {
            BookCopy selectedCopy = bookCopies.ElementAt(selectedCopyIndex);
            if (bookDao.removeBookCopy(selectedCopy) == true)
            {
                book.NumberOfCopies--;
                bookCopies.RemoveAt(selectedCopyIndex);
                selectedCopyIndex = -1;

                copySelected = false;
                deleteCopyCommand.executeChanged();
            }
        }

        public void updateBookInfo()
        {
            Author selectedAuthor = authors.ElementAt(selectedAuthorIndex);
            Genre selectedGenre = genres.ElementAt(selectedGenreIndex);
            Publisher selectedPublisher = publishers.ElementAt(selectedPublisherIndex);

            Book newBookData = new Book(book.BookID, isbn13, isbn10, bookTitle, edition, selectedAuthor, selectedPublisher, selectedGenre, book.NumberOfCopies);
            bool updateStatus = bookDao.updateBook(newBookData);

            if(updateStatus == true)
            {
                MessageBox.Show("Update successful", "Success", MessageBoxButton.OK);
                book.ISBN13 = isbn13;
                book.ISBN10 = isbn10;
                book.BookTitle = bookTitle;
                book.Edition = edition;
                book.Author = selectedAuthor;
                book.Publisher = selectedPublisher;
                book.Genre = selectedGenre;
            }
            else
            {
                MessageBox.Show("Update not successful", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
