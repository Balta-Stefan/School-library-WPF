using School_library.Commands;
using School_library.DAO;
using School_library.Models;
using School_library.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.ViewModels
{
    public class BooksPanelViewModel : ViewModelBase
    {
        private BookDAO bookDao;
        private PublisherDAO publisherDao;
        private GenreDAO genreDao;
        private AuthorDAO authorDAO;

        private bool filtersClear = true;

        private ObservableCollection<Book> books;
        private ObservableCollection<Publisher> publishers;
        private ObservableCollection<Genre> genres;
        private ObservableCollection<Author> authors;

        public ObservableCollection<Author> Authors
        {
            get { return authors; }
        }

        public ObservableCollection<Book> Books
        {
            get { return books; }
        }

        public ObservableCollection<Publisher> Publishers
        {
            get { return publishers; }
            set { publishers = value; }
        }

        public ObservableCollection<Genre> Genres
        {
            get { return genres; }
            set { genres = value; }
        }

        private string nameFilter = string.Empty;

        public string NameFilter
        {
            get { return nameFilter; }
            set
            {
                filtersClear = false;
                nameFilter = value.Trim();
                OnPropertyChange("NameFilter");
            }
        }

        private int numberOfCopiesFilter = -1;

        public string NumberOfCopiesFilter
        {
            get
            {
                if (numberOfCopiesFilter == -1)
                    return string.Empty;
                else
                    return numberOfCopiesFilter.ToString();
            }
            set
            {
                if(value.Equals(string.Empty))
                {
                    numberOfCopiesFilter = -1;
                    OnPropertyChange("NumberOfCopiesFilter");
                    filtersClear = false;
                    return;
                }
                try
                {
                    numberOfCopiesFilter = Int32.Parse(value);
                    filtersClear = false;
                    OnPropertyChange("NumberOfCopiesFilter");
                }
                catch (Exception) { }
            }
        }

        private bool onlyWithAvailableCopiesFilter = false;

        public bool OnlyWithAvailableCopiesFilter
        {
            get { return onlyWithAvailableCopiesFilter; }
            set
            {
                filtersClear = false;
                onlyWithAvailableCopiesFilter = value;
                OnPropertyChange("OnlyWithAvailableCopiesFilter");
            }
        }

        private Genre? selectedGenre = null;

        public Genre? SelectedGenre
        {
            get { return selectedGenre; }
            set 
            {
                filtersClear = false;
                selectedGenre = value;
                OnPropertyChange("SelectedGenre");
            }
        }

        private Publisher? selectedPublisher = null;

        public Publisher? SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                filtersClear = false;
                selectedPublisher = value;
                OnPropertyChange("SelectedPublisher");
            }
        }

        private Author? selectedAuthor = null;
        public Author? SelectedAuthor
        {
            get { return selectedAuthor; }
            set
            {
                filtersClear = false;
                selectedAuthor = value;
                OnPropertyChange("SelectedAuthor");
            }
        }

        private string isbn10Filter = string.Empty;

        public string Isbn10Filter
        {
            get { return isbn10Filter; }
            set
            {
                isbn10Filter = value.Trim();
                filtersClear = false;
                OnPropertyChange("Isbn10Filter");
            }
        }

        private string isbn13Filter = string.Empty;

        public string Isbn13Filter
        {
            get { return isbn13Filter; }
            set
            {
                isbn13Filter = value.Trim();
                filtersClear = false;
                OnPropertyChange("Isbn13Filter");
            }
        }

        private Book? selectedBook = null;
        public Book? SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                if(value != null)
                    OnBookSelect();
                //OnPropertyChange("SelectedBookIndex");
            }
        }
        public ICommand FilterBooksCommand { get; }
        public ICommand ClearBookFiltersCommand { get; }
        public ICommand BooksPanel_AddNewBookCommand { get; }

        public BooksPanelViewModel(BookDAO bookDao, PublisherDAO publisherDao, GenreDAO genreDao, AuthorDAO authorDAO)
        {
            this.books = new ObservableCollection<Book>(bookDao.getBooks());
            this.publishers = new ObservableCollection<Publisher>(publisherDao.getPublishers());
            this.genres = new ObservableCollection<Genre>(genreDao.getGenres());
            this.authors = new ObservableCollection<Author>(authorDAO.getAuthors());
            this.bookDao = bookDao;
            this.publisherDao = publisherDao;
            this.genreDao = genreDao;
            this.authorDAO = authorDAO;

            FilterBooksCommand = new FilterBooksCommand(this);
            ClearBookFiltersCommand = new ClearBookFiltersCommand(this);
            BooksPanel_AddNewBookCommand = new BooksPanel_AddNewBookCommand(this);
        }

        public void openAddBookWindow()
        {
            AddNewBookViewModel addBookViewModel = new AddNewBookViewModel(bookDao, genres, publishers, authors, books, publisherDao, authorDAO, genreDao);

            AddNewBookWindow window = new AddNewBookWindow()
            {
                DataContext = addBookViewModel
            };

            window.ShowDialog();
        }

        public void clearFilters()
        {
            if (filtersClear == true)
                return;

            NameFilter = string.Empty;
            NumberOfCopiesFilter = string.Empty;
            OnlyWithAvailableCopiesFilter = false;
            SelectedPublisher = null;
            SelectedGenre = null;
            Isbn10Filter = Isbn13Filter = string.Empty;
            SelectedAuthor = null;

            filtersClear = true;

            books.Clear();
            List<Book> newBooks = bookDao.getBooks();
            foreach (Book b in newBooks) books.Add(b);
        }

        private void OnBookSelect()
        {
            EditBookInfo editDataWindow = new EditBookInfo()
            {
                DataContext = new EditBookInfoViewModel(bookDao, genreDao, publisherDao, authorDAO, selectedBook)
            };
            editDataWindow.ShowDialog();
        }

        private bool areFiltersEmpty()
        {
            if (isbn10Filter.Equals(string.Empty) &&
               isbn13Filter.Equals(string.Empty) &&
               nameFilter.Equals(string.Empty) &&
               numberOfCopiesFilter == -1 &&
               selectedPublisher == null &&
               selectedGenre == null &&
               onlyWithAvailableCopiesFilter == false &&
               selectedAuthor == null)
                return true;
            return false;
        }

        public void filterBooks()
        {
            // if all filters are clear, return
            if (areFiltersEmpty() == true)
                return;

            List<Book> allBooks = bookDao.getBooks();

            books.Clear();

            foreach(Book b in allBooks)
            {
                if(isbn10Filter.Equals(string.Empty) == false && b.ISBN10.Equals(isbn10Filter) == false)
                    continue;
                if (isbn13Filter.Equals(string.Empty) == false && b.ISBN13.Equals(isbn13Filter) == false)
                    continue;
                if (nameFilter.Equals(string.Empty) == false && b.BookTitle.Equals(nameFilter) == false)
                    continue;
                if (numberOfCopiesFilter != -1 && b.NumberOfCopies != numberOfCopiesFilter)
                    continue;
                if(selectedPublisher != null && b.Publisher.Equals(selectedPublisher) == false)
                    continue;
                if(selectedGenre != null && b.Genre.Equals(selectedGenre) == false)
                    continue;
                if (selectedAuthor != null && b.Author.Equals(selectedAuthor) == false)
                    continue;
                if(onlyWithAvailableCopiesFilter == true)
                {
                    bool hasAvailableCopies = false;

                    List<BookCopy> copies = bookDao.getBookCopies(b);
                    foreach(BookCopy c in copies)
                    {
                        if (c.available == true)
                        {
                            hasAvailableCopies = true;
                            break;
                        }
                    }
                    if (hasAvailableCopies == false)
                        continue;
                }
                books.Add(b); // the book satisfies all filters
            }
        }
    }
}
