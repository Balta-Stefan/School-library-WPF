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
        public int userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public UserTypes userType { get; set; }
        public bool active { get; set; } = true;
        public string? localization { get; set; }
        public string? theme { get; set; }

        public User(int userID, string firstName, string lastName, string username, string password, UserTypes userType, string? localization, string? theme)
        {
            this.userID = userID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.userType = userType;
            this.localization = localization;
            this.theme = theme;
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

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}
