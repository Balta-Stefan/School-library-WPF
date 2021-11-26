using School_library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class BooksPanelViewModel
    {
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

        public BooksPanelViewModel(ObservableCollection<Book> books, ObservableCollection<Publisher> publishers, ObservableCollection<Genre> genres)
        {
            this.books = books;
            this.publishers = publishers;
            this.genres = genres;
        }
    }
}
