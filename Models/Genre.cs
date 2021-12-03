using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Genre genre &&
                   GenreId == genre.GenreId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GenreId);
        }

        public override string ToString()
        {
            return GenreName;
        }
    }
}
