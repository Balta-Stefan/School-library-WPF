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
        private PublisherDAO publisherDao;
        private GenreDAO genreDAO;
        private AuthorDAO authorDao;
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
        
        private Author? selectedAuthor = null;
        public Author? SelectedAuthor 
        {
            get { return selectedAuthor; }
            set
            {
                selectedAuthor = value;
                OnPropertyChange("SelectedAuthor");
                newAuthorFirstName = newAuthorLastName = string.Empty;
                OnPropertyChange("NewAuthorFirstName");
                OnPropertyChange("NewAuthorLastName");
            }
        }

        public ObservableCollection<Publisher> publishers { get; }

        private Publisher? selectedPublisher = null;
        public Publisher? SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                selectedPublisher = value;
                newPublisherName = string.Empty;
                OnPropertyChange("NewPublisherName");
                OnPropertyChange("SelectedPublisher");
            }
        }

        public ObservableCollection<Genre> genres { get; }
        private Genre? selectedGenre = null;
        public Genre? SelectedGenre 
        {
            get { return selectedGenre; }
            set
            {
                newGenreName = string.Empty;
                OnPropertyChange("newGenreName");
                selectedGenre = value;
                OnPropertyChange("SelectedGenre");
            }
        }

        private string newAuthorFirstName = string.Empty;
        private string newAuthorLastName = string.Empty;

        public string NewAuthorFirstName
        {
            get { return newAuthorFirstName; }
            set
            {
                newAuthorFirstName = value;
                selectedAuthor = null;
                OnPropertyChange("NewAuthorFirstName");
                OnPropertyChange("SelectedAuthor");
            }
        }
        public string NewAuthorLastName
        {
            get { return newAuthorLastName; }
            set
            {
                newAuthorLastName = value;
                selectedAuthor = null;
                OnPropertyChange("NewAuthorLastName");
                OnPropertyChange("SelectedAuthor");
            }
        }

        private string newPublisherName = string.Empty;
        public string NewPublisherName
        {
            get { return newPublisherName; }
            set
            {
                selectedPublisher = null;
                newPublisherName = value;
                OnPropertyChange("NewPublisherName");
                OnPropertyChange("SelectedPublisher");
            }
        }

        private string newGenreName = string.Empty;
        public string NewGenreName
        {
            get { return newGenreName; }
            set
            {
                newGenreName = value;
                selectedGenre = null;
                OnPropertyChange("NewGenreName");
                OnPropertyChange("SelectedGenre");
            }
        }
        
        public ICommand addBookCommand { get; }
        public AddNewBookViewModel(BookDAO bookDao,
            ObservableCollection<Genre> genres, 
            ObservableCollection<Publisher> publishers,
            ObservableCollection<Author> authors,
            ObservableCollection<Book> books,
            PublisherDAO publisherDao,
            AuthorDAO authorDao,
            GenreDAO genreDao)
        {
            this.bookDao = bookDao;
            this.genres = genres;
            this.publishers = publishers;
            this.authors = authors;
            this.books = books;
            this.publisherDao = publisherDao;
            this.authorDao = authorDao;
            this.genreDAO = genreDao;

            addBookCommand = new AddBookCommand(this);
        }

        public void addBook()
        {
            if(SelectedAuthor == null && (newAuthorFirstName.Equals(string.Empty) || newAuthorLastName.Equals(string.Empty) ||
                SelectedGenre == null && (newGenreName.Equals(string.Empty)) ||
                SelectedPublisher == null && (newPublisherName.Equals(string.Empty)) ||
                title.Equals(string.Empty) ||
                Edition.Equals(string.Empty) ||
                isbn13.Equals(string.Empty) ||
                isbn10.Equals(string.Empty)))
            {
                MessageBox.Show("All fields must be filled out!", "", MessageBoxButton.OK);
                return;
            }

            Publisher? pub;
            Genre? genre;
            Author? author;

            if (SelectedAuthor != null)
                author = selectedAuthor;
            else
            {
                if(newAuthorFirstName.Equals(string.Empty) || newAuthorLastName.Equals(string.Empty))
                {
                    MessageBox.Show("All fields for the new author must be filled out!", "", MessageBoxButton.OK);
                    return;
                }
                author = new Author(-1, newAuthorFirstName, newAuthorLastName);
                author = authorDao.insertAuthor(author);
                if(author == null)
                {
                    MessageBox.Show("Couldn't add new author!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (selectedGenre != null)
                genre = selectedGenre;
            else
            {
                if (newGenreName.Equals(string.Empty))
                {
                    MessageBox.Show("All fields for the new genre must be filled out!", "", MessageBoxButton.OK);
                    return;
                }
                genre = new Genre(-1, newGenreName);
                genre = genreDAO.insertGenre(genre);
                if(genre == null)
                {
                    MessageBox.Show("Couldn't add new genre!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (selectedPublisher != null)
                pub = selectedPublisher;
            else
            {
                if (newPublisherName.Equals(string.Empty))
                {
                    MessageBox.Show("All fields for the new publisher must be filled out!", "", MessageBoxButton.OK);
                    return;
                }
                pub = new Publisher(-1, newPublisherName);
                pub = publisherDao.insertPublisher(pub);
                if (pub == null)
                {
                    MessageBox.Show("Couldn't add new publisher!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Book? newBook = new Book(-1, isbn13, isbn10, title, edition, author, pub, genre, 1);
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
