using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Publisher
    {
        public int publisherID { get; private set; }
        public string name { get; private set; }

        public Publisher(int publisherID, string name)
        {
            this.publisherID = publisherID;
            this.name = name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Publisher publisher &&
                   publisherID == publisher.publisherID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(publisherID);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
