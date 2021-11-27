using School_library.Commands;
using School_library.Models;
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
        private bool filtersClear = true;

        private ObservableCollection<Book> books;
        private ObservableCollection<Publisher> publishers;
        private ObservableCollection<Genre> genres;

        public ObservableCollection<Book> Books
        {
            get { return books; }
            set { books = value; }
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

        private int numberOfCopiesFilter = int.MaxValue;

        public string NumberOfCopiesFilter
        {
            get
            {
                if (numberOfCopiesFilter == int.MaxValue)
                    return string.Empty;
                else
                    return numberOfCopiesFilter.ToString();
            }
            set
            {
                if(value.Equals(string.Empty))
                {
                    numberOfCopiesFilter = int.MaxValue;
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
        public ICommand FilterBooksCommand { get; }
        public ICommand ClearBookFiltersCommand { get; }

        public BooksPanelViewModel(ObservableCollection<Book> books, ObservableCollection<Publisher> publishers, ObservableCollection<Genre> genres)
        {
            this.books = books;
            this.publishers = publishers;
            this.genres = genres;

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
        }
    }
}
