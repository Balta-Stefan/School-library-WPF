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
    public class AddNewBookViewModel : ViewModelBase
    {
        private BookDAO bookDao;
        private ObservableCollection<Book> books;

        private string isbn10 = string.Empty;
        public string ISBN10
        {
            get { return isbn10; }
            set
            {
                isbn10 = value;
            }
        }

        private string isbn13 = string.Empty;
        public string ISBN13
        {
            get { return isbn13; }
            set
            {
                isbn13 = value;
            }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
            }
        }

        private short edition = 1;
        public string Edition
        {
            get { return edition.ToString(); }
            set
            {
                try
                {
                    edition = Int16.Parse(value);
                }
                catch (Exception) { }
            }
        }

        public ObservableCollection<Author> authors { get; }
        public Author? SelectedAuthor { get; set; } = null;

        public ObservableCollection<Publisher> publishers { get; }
        public Publisher? SelectedPublisher { get; set; } = null;

        public ObservableCollection<Genre> genres { get; }
        public Genre? SelectedGenre { get; set; } = null;

        public ICommand addBookCommand { get; }
        public AddNewBookViewModel(BookDAO bookDao, ObservableCollection<Genre> genres, ObservableCollection<Publisher> publishers, ObservableCollection<Author> authors, ObservableCollection<Book> books)
        {
            this.bookDao = bookDao;
            this.genres = genres;
            this.publishers = publishers;
            this.authors = authors;
            this.books = books;

            addBookCommand = new AddBookCommand(this);
        }

        public void addBook()
        {
            if(SelectedAuthor == null ||
                SelectedGenre == null ||
                SelectedPublisher == null ||
                title.Equals(string.Empty) ||
                Edition.Equals(string.Empty) ||
                isbn13.Equals(string.Empty) ||
                isbn10.Equals(string.Empty))
            {
                MessageBox.Show("All fields must be filled out!", "", MessageBoxButton.OK);
                return;
            }

            Book newBook = new Book(-1, isbn13, isbn10, title, edition, SelectedAuthor, SelectedPublisher, SelectedGenre, 1);
            newBook = bookDao.addBook(newBook);

            if(newBook == null)
            {
                MessageBox.Show("Couldn't add the book!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                books.Add(newBook);
                MessageBox.Show("Book Added", "", MessageBoxButton.OK);
            }
        }
    }
}
