using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class User
    {
        public enum UserTypes { LIBRARIAN, ACCOUNTANT, MEMBER}
        public int userID { get; private set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string username { get; private set; }
        public string password { get; private set; }

        public UserTypes userType { get; }

        public User(int userID, string firstName, string lastName, string username, string password, UserTypes userType)
        {
            this.userID = userID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.userType = userType;
        }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   userID == user.userID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(userID);
        }
    }
}
