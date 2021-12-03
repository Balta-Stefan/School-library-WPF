using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_library.Models;

namespace School_library.ViewModels
{
    public class GenreViewModel : ViewModelBase
    {
        private Genre genre;

        public override string ToString()
        {
            return genre.ToString();
        }
        public Genre Genre
        {
            get { return genre; }
        }
        public string GenreName
        {
            get { return genre.GenreName; }
            set
            {
                genre.GenreName = value;
                OnPropertyChange("GenreName");
            }
        }

        public virtual ICollection<Book> Books { get { return genre.Books; } }

        public GenreViewModel(Genre genre)
        {
            this.genre = genre;
        }
    }
}
