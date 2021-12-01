using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Accountant : User
    {
        public Accountant(int userID, string firstName, string lastName, string username, string password) : base(userID, firstName, lastName, username, password, User.UserTypes.ACCOUNTANT)
        {
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
