using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Author
    {
        public int authorID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Author(int authorID, string firstName, string lastName)
        {
            this.authorID = authorID;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public override bool Equals(object? obj)
        {
            return obj is Author author &&
                   authorID == author.authorID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(authorID);
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}
