using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.Models
{
    public class Member : User
    {
        public int cardNumber { get; private set; }
        public Member(int userID, string firstName, string lastName, string username, string password, int cardNumber) : base(userID, firstName, lastName, username, password, User.userTypes.MEMBER)
        {
            this.cardNumber = cardNumber;
        }
    }
}
