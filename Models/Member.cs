using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Member : User
    {
        public Member(int userID, string firstName, string lastName, string username, string password, string? localization, string? theme) : base(userID, firstName, lastName, username, password, User.UserTypes.MEMBER, localization, theme)
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
