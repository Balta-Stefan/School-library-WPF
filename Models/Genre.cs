using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Genre
    {
        public int genreID { get; private set; }
        public string name { get; private set; }

        public Genre(int genreID, string name)
        {
            this.genreID = genreID;
            this.name = name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Genre genre &&
                   genreID == genre.genreID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(genreID);
        }
    }
}
