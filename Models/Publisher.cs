using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Publisher publisher &&
                   PublisherId == publisher.PublisherId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PublisherId);
        }

        public override string ToString()
        {
            return PublisherName;
        }
    }
}
