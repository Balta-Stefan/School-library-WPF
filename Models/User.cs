using System;
using System.Collections.Generic;

#nullable disable

namespace School_library.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public byte Active { get; set; }
        public string Localization { get; set; }
        public string Theme { get; set; }

        public virtual Accountant Accountant { get; set; }
        public virtual Librarian Librarian { get; set; }
        public virtual Member Member { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   UserId == user.UserId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId);
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
