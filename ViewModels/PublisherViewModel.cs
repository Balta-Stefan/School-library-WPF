using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.ViewModels
{
    public class PublisherViewModel : ViewModelBase
    {
        private Publisher publisher;


        public override string ToString()
        {
            return publisher.ToString();
        }

        public override bool Equals(object? obj)
        {
            return publisher.Equals(obj);
        }

        public override int GetHashCode()
        {
            return publisher.GetHashCode();
        }

        public Publisher Publisher
        {
            get { return publisher; }
        }
        public string PublisherName
        {
            get { return publisher.PublisherName; }
            set
            {
                publisher.PublisherName = value;
                OnPropertyChange("PublisherName");
            }
        }

        public virtual ICollection<Book> Books { get { return publisher.Books; } }
        public PublisherViewModel(Publisher publisher)
        {
            this.publisher = publisher;
        }
    }
}
