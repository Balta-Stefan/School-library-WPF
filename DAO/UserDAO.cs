using MySql.Data.MySqlClient;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.DAO
{
    public class UserDAO
    {
        private string connectionString;

        private const string getUsersQuery = "SELECT * FROM Users";
        //private const string getMemberCardQuery = "SELECT cardNumber FROM Members WHERE userID=@userID";

        public UserDAO(string connectionString) => this.connectionString = connectionString;

        public List<User> getUsers()
        {
            List<User> users = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getUsersQuery;


                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int userID = result.GetInt32("userID");
                        string firstName = result.GetString("firstName");
                        string lastName = result.GetString("lastName");
                        string userName = result.GetString("userName");
                        string password = result.GetString("password");
                        string userType = result.GetString("userType");
                        User.UserTypes userTypeEnum = (User.UserTypes)Enum.Parse(typeof(User.UserTypes), userType);
                        if(userTypeEnum.Equals(User.UserTypes.MEMBER))
                        {
                            Member member = new Member(userID, firstName, lastName, userName, password);
                            users.Add(member);
                        }
                        else
                        {
                            User user = new User(userID, firstName, lastName, userName, password, userTypeEnum);
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        /*private int? getMemberCard(int userID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getMemberCardQuery;

                MySqlParameter userIDParam = new MySqlParameter("userID", MySqlDbType.Int32);
                userIDParam.Value = userID;
                query.Parameters.Add(userIDParam);

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        return result.GetInt32("cardNumber");
                    }
                }
            }
            return null;
        }*/
    }
}
