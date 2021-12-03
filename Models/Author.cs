using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Author author &&
                   AuthorId == author.AuthorId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AuthorId);
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
