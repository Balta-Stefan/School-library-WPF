using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class AuthorViewModel : ViewModelBase
    {
        private Author author;

        public override string ToString()
        {
            return author.ToString();
        }

        public override bool Equals(object? obj)
        {
            return author.Equals(obj);
        }

        public override int GetHashCode()
        {
            return author.GetHashCode();
        }

        public Author Author
        {
            get { return author; }
        }
        public int AuthorId 
        {
            get { return author.AuthorId; }
        }
        public string FirstName 
        {
            get { return author.FirstName; }
            set
            {
                author.FirstName = value;
                OnPropertyChange("FirstName");
            }
        }
        public string LastName
        {
            get { return author.LastName; }
            set
            {
                author.LastName = value;
                OnPropertyChange("LastName");
            }
        }

        public ICollection<Book> Books 
        {
            get { return author.Books; }
        }

        public AuthorViewModel(Author author)
        {
            this.author = author;
        }
    }
}
