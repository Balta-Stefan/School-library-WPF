using School_library.Commands;
using School_library.Models;
using School_library.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace School_library.ViewModels
{
    public class BooksPanelViewModel : ViewModelBase
    {
        private readonly mydbContext dbContext;

        private bool filtersClear = true;

        private ObservableCollection<BookViewModel> books = new ObservableCollection<BookViewModel>();
        private ObservableCollection<PublisherViewModel> publishers = new ObservableCollection<PublisherViewModel>();
        private ObservableCollection<GenreViewModel> genres = new ObservableCollection<GenreViewModel>();
        private ObservableCollection<AuthorViewModel> authors = new ObservableCollection<AuthorViewModel>();

        private Collection<ResourceDictionary> resourceDictionary;

        public ObservableCollection<AuthorViewModel> Authors
        {
            get { return authors; }
        }

        public ObservableCollection<BookViewModel> Books
        {
            get { return books; }
        }

        public ObservableCollection<PublisherViewModel> Publishers
        {
            get { return publishers; }
            set { publishers = value; }
        }

        public ObservableCollection<GenreViewModel> Genres
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
                nameFilter = value;
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

        private GenreViewModel? selectedGenre = null;

        public GenreViewModel? SelectedGenre
        {
            get { return selectedGenre; }
            set 
            {
                filtersClear = false;
                selectedGenre = value;
                OnPropertyChange("SelectedGenre");
            }
        }

        private PublisherViewModel? selectedPublisher = null;

        public PublisherViewModel? SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                filtersClear = false;
                selectedPublisher = value;
                OnPropertyChange("SelectedPublisher");
            }
        }

        private AuthorViewModel? selectedAuthor = null;
        public AuthorViewModel? SelectedAuthor
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

        private BookViewModel? selectedBook = null;
        public BookViewModel? SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                if(value != null)
                {
                    OnBookSelect();
                }

                OnPropertyChange("SelectedBook");
            }
        }
        public ICommand FilterBooksCommand { get; }
        public ICommand ClearBookFiltersCommand { get; }
        public ICommand BooksPanel_AddNewBookCommand { get; }

        public BooksPanelViewModel(mydbContext dbContext, Collection<ResourceDictionary> resourceDictionary)
        {
            this.dbContext = dbContext;
            foreach (Book b in dbContext.Books.ToList()) books.Add(new BookViewModel(b));
            foreach (Publisher b in dbContext.Publishers.ToList()) publishers.Add(new PublisherViewModel(b));
            foreach (Genre b in dbContext.Genres.ToList()) genres.Add(new GenreViewModel(b));
            foreach (Author b in dbContext.Authors.ToList()) authors.Add(new AuthorViewModel(b));


            /*this.books = new ObservableCollection<Book>(dbContext.Books.ToList());
            this.publishers = new ObservableCollection<Publisher>(dbContext.Publishers.ToList());
            this.genres = new ObservableCollection<Genre>(dbContext.Genres.ToList());
            this.authors = new ObservableCollection<Author>(dbContext.Authors.ToList());*/
            this.resourceDictionary = resourceDictionary;

            FilterBooksCommand = new FilterBooksCommand(this);
            ClearBookFiltersCommand = new ClearBookFiltersCommand(this);
            BooksPanel_AddNewBookCommand = new BooksPanel_AddNewBookCommand(this);
        }

        public void openAddBookWindow()
        {
            AddNewBookViewModel addBookViewModel = new AddNewBookViewModel(dbContext, genres, publishers, authors, books);

            AddNewBookWindow window = new AddNewBookWindow()
            {
                DataContext = addBookViewModel
            };
            foreach (var c in resourceDictionary) window.Resources.MergedDictionaries.Add(c);

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
            foreach (Book b in dbContext.Books.ToList()) books.Add(new BookViewModel(b));
        }

        private void OnBookSelect()
        {
            EditBookInfo editDataWindow = new EditBookInfo()
            {
                DataContext = new EditBookInfoViewModel(dbContext, selectedBook)
            };
            foreach (var c in resourceDictionary) editDataWindow.Resources.MergedDictionaries.Add(c);
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
            //if (areFiltersEmpty() == true)
                //return;


            books.Clear();

            var tempBooks = dbContext.Books.Where(b => (
                                                        (string.IsNullOrEmpty(isbn10Filter) || b.Isbn10.Equals(isbn10Filter))
                                                     && (string.IsNullOrEmpty(isbn13Filter) || b.Isbn13.Equals(isbn13Filter))
                                                     && (string.IsNullOrEmpty(NameFilter) || b.BookTitle.Equals(NameFilter))
                                                     && (numberOfCopiesFilter == -1 || b.NumberOfCopies == numberOfCopiesFilter)
                                                     && (selectedPublisher == null || b.Publisher.Equals(selectedPublisher))
                                                     && (selectedGenre == null || b.Genre.Equals(selectedGenre))
                                                     && (selectedAuthor == null || b.Author.Equals(selectedAuthor)))).ToList();

            if(onlyWithAvailableCopiesFilter == true)
            {
                foreach (Book b in tempBooks)
                {
                    bool available = true;
                    foreach (BookCopy bc in b.BookCopies)
                    {
                        if(bc.Available == 0)
                        {
                            available = false;
                            break;
                        }
                    }

                    if (available == true)
                        books.Add(new BookViewModel(b));
                }
            }
            else
            {
                foreach (Book b in tempBooks) books.Add(new BookViewModel(b));
            }
                                                     

            
        }
    }
}
