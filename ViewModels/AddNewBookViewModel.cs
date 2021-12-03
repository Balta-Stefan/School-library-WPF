using School_library.Commands;
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
        private readonly mydbContext dbContext;
        private ObservableCollection<BookViewModel> books;

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

        public ObservableCollection<AuthorViewModel> authors { get; }
        
        private AuthorViewModel? selectedAuthor = null;
        public AuthorViewModel? SelectedAuthor 
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

        public ObservableCollection<PublisherViewModel> publishers { get; }

        private PublisherViewModel? selectedPublisher = null;
        public PublisherViewModel? SelectedPublisher
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

        public ObservableCollection<GenreViewModel> genres { get; }
        private GenreViewModel? selectedGenre = null;
        public GenreViewModel? SelectedGenre 
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
        public AddNewBookViewModel(mydbContext dbContext,
            ObservableCollection<GenreViewModel> genres, 
            ObservableCollection<PublisherViewModel> publishers,
            ObservableCollection<AuthorViewModel> authors,
            ObservableCollection<BookViewModel> books)
        {
            this.dbContext = dbContext;
            this.genres = genres;
            this.publishers = publishers;
            this.authors = authors;
            this.books = books;
           

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
                MessageBox.Show(School_library.Resources.DataInputsEmpty, "", MessageBoxButton.OK);
                return;
            }

            Publisher? pub;
            Genre genre;
            Author? author;

            if (SelectedAuthor != null)
                author = selectedAuthor.Author;
            else
            {
                if(newAuthorFirstName.Equals(string.Empty) || newAuthorLastName.Equals(string.Empty))
                {
                    MessageBox.Show(School_library.Resources.NewAuthorEmptyInputs, "", MessageBoxButton.OK);
                    return;
                }
                author = new Author()
                {
                    FirstName = newAuthorFirstName,
                    LastName = newAuthorLastName
                };

                dbContext.Authors.Add(author);
                try
                {
                    dbContext.SaveChanges();
                }
                catch(Exception)
                {
                    MessageBox.Show(School_library.Resources.CouldntAddNewAuthor, School_library.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (selectedGenre != null)
                genre = selectedGenre.Genre;
            else
            {
                if (newGenreName.Equals(string.Empty))
                {
                    MessageBox.Show(School_library.Resources.NewGenreEmptyInputs, "", MessageBoxButton.OK);
                    return;
                }
                genre = new Genre()
                {
                    GenreName = newGenreName
                };
                dbContext.Genres.Add(genre);

                try
                {
                    dbContext.SaveChanges();
                }
                catch(Exception)
                {
                    MessageBox.Show(School_library.Resources.couldntAddNewGenre, School_library.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (selectedPublisher != null)
                pub = selectedPublisher.Publisher;
            else
            {
                if (newPublisherName.Equals(string.Empty))
                {
                    MessageBox.Show(School_library.Resources.NewPublisherFieldsEmpty, "", MessageBoxButton.OK);
                    return;
                }
                pub = new Publisher()
                {
                    PublisherName = newPublisherName
                };
                dbContext.Publishers.Add(pub);
                try
                {
                    dbContext.SaveChanges();
                }
                catch(Exception)
                {
                    MessageBox.Show(School_library.Resources.NewPublisherNotAdded, School_library.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Book newBook = new Book()
            {
                Isbn13 = isbn13,
                Isbn10 = isbn10,
                BookTitle = title,
                Edition = edition,
                Author = author,
                Publisher = pub,
                GenreNavigation = genre,
                NumberOfCopies = 1
            };

            try
            {
                dbContext.Books.Add(newBook);
                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                MessageBox.Show(School_library.Resources.CouldntAddNewBook, School_library.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            books.Add(new BookViewModel(newBook));
            MessageBox.Show(School_library.Resources.BookAdded, "", MessageBoxButton.OK);
        }
    }
}
