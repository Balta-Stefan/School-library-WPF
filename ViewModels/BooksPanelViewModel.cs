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

        private int selectedGenreIndex = -1;

        public int SelectedGenreIndex
        {
            get { return selectedGenreIndex; }
            set 
            {
                filtersClear = false;
                selectedGenreIndex = value;
                OnPropertyChange("SelectedGenreIndex");
            }
        }

        private int selectedPublisherIndex = -1;

        public int SelectedPublisherIndex
        {
            get { return selectedPublisherIndex; }
            set
            {
                filtersClear = false;
                selectedPublisherIndex = value;
                OnPropertyChange("SelectedPublisherIndex");
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

        private int selectedBookIndex = -1;
        public int SelectedBookIndex
        {
            get { return selectedBookIndex; }
            set
            {
                selectedBookIndex = value;
                OnBookSelect();
                //OnPropertyChange("SelectedBookIndex");
            }
        }
        public ICommand FilterBooksCommand { get; }
        public ICommand ClearBookFiltersCommand { get; }

        public BooksPanelViewModel(BookDAO bookDao, PublisherDAO publisherDao, GenreDAO genreDao, AuthorDAO authorDAO)
        {
            this.books = new ObservableCollection<Book>(bookDao.getBooks());
            this.publishers = new ObservableCollection<Publisher>(publisherDao.getPublishers());
            this.genres = new ObservableCollection<Genre>(genreDao.getGenres());
            this.bookDao = bookDao;
            this.publisherDao = publisherDao;
            this.genreDao = genreDao;
            this.authorDAO = authorDAO;

            FilterBooksCommand = new FilterBooksCommand(this);
            ClearBookFiltersCommand = new ClearBookFiltersCommand(this);
        }

        public void clearFilters()
        {
            if (filtersClear == true)
                return;

            NameFilter = string.Empty;
            NumberOfCopiesFilter = string.Empty;
            OnlyWithAvailableCopiesFilter = false;
            SelectedGenreIndex = SelectedPublisherIndex = - 1;
            Isbn10Filter = Isbn13Filter = string.Empty;

            filtersClear = true;

            books.Clear();
            List<Book> newBooks = bookDao.getBooks();
            foreach (Book b in newBooks) books.Add(b);
        }

        private void OnBookSelect()
        {
            Book selectedBook = books.ElementAt(selectedBookIndex);


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
               selectedPublisherIndex == -1 &&
               selectedGenreIndex == -1 &&
               onlyWithAvailableCopiesFilter == false)
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
                if(selectedPublisherIndex != -1)
                {
                    Publisher selectedPublisher = publishers.ElementAt(selectedPublisherIndex);
                    if (b.Publisher.Equals(selectedPublisher) == false)
                        continue;
                }
                if(selectedGenreIndex != -1)
                {
                    Genre selectedGenre = genres.ElementAt(selectedGenreIndex);
                    if (b.Genre.Equals(selectedGenre) == false)
                        continue;
                }
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
