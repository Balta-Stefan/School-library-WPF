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
        private const string addUserQuery = "INSERT INTO Users(firstName, lastName, username, password, userType) VALUES(@firstName, @lastName, @username, @password, @userType)";
        private const string addAccountantQuery = "INSERT INTO Accountants(userID) VALUES(@userID)";
        private const string addMemberQuery = "INSERT INTO Members(userID) VALUES(@userID)";
        private const string updateUserQuery = "UPDATE Users SET firstName=@firstName, lastName=@lastName, username=@username, password=@password, userType=@userType, active=@active WHERE userID=@userID";
        private const string getUserQuery = "SELECT * FROM Users WHERE username=@username";
        //private const string getMemberCardQuery = "SELECT cardNumber FROM Members WHERE userID=@userID";

        public UserDAO(string connectionString) => this.connectionString = connectionString;

        public User? getUser(string username)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand query = connection.CreateCommand();
                    query.CommandText = getUserQuery;

                    MySqlParameter usernameParam = new MySqlParameter("username", MySqlDbType.String);
                    usernameParam.Value = username;
                    query.Parameters.Add(usernameParam);

                    using (MySqlDataReader result = query.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            int userID = result.GetInt32("userID");
                            string firstName = result.GetString("firstName");
                            string lastName = result.GetString("lastName");
                            string password = result.GetString("password");
                            string userTypeString = result.GetString("userType");
                            bool active = result.GetBoolean("active");

                            string? localization = null;
                            int localizationColumn = result.GetOrdinal("localization");
                            if(result.IsDBNull(localizationColumn) == false)
                            {
                                localization = result.GetString("localization");
                            }

                            string? theme = null;
                            int themeColumn = result.GetOrdinal("theme");
                            if(result.IsDBNull(themeColumn) == false)
                            {
                                theme = result.GetString("theme");
                            }


                            User.UserTypes userType = (User.UserTypes)Enum.Parse(typeof(User.UserTypes), userTypeString);

                            User user = new User(userID, firstName, lastName, username, password, userType, localization, theme);
                            return user;
                        }
                    }
                }
            }
            catch (Exception) { return null; }
            return null;
        }
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
                        bool isActive = result.GetBoolean("active");

                        string? localization = null;
                        int localizationColumn = result.GetOrdinal("localization");
                        if (result.IsDBNull(localizationColumn) == false)
                        {
                            localization = result.GetString("localization");
                        }

                        string? theme = null;
                        int themeColumn = result.GetOrdinal("theme");
                        if (result.IsDBNull(themeColumn) == false)
                        {
                            theme = result.GetString("theme");
                        }

                        User.UserTypes userTypeEnum = (User.UserTypes)Enum.Parse(typeof(User.UserTypes), userType);

                        if(userTypeEnum.Equals(User.UserTypes.MEMBER))
                        {
                            Member member = new Member(userID, firstName, lastName, userName, password, localization, theme);
                            member.active = isActive;
                            users.Add(member);
                        }
                        else
                        {
                            User user = new User(userID, firstName, lastName, userName, password, userTypeEnum, localization, theme);
                            user.active = isActive;
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public bool updateUser(User user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand query = connection.CreateCommand();
                    query.CommandText = updateUserQuery;

                    MySqlParameter firstNameParam = new MySqlParameter("firstName", MySqlDbType.String);
                    firstNameParam.Value = user.firstName;
                    query.Parameters.Add(firstNameParam);


                    MySqlParameter lastNameParam = new MySqlParameter("lastName", MySqlDbType.String);
                    lastNameParam.Value = user.lastName;
                    query.Parameters.Add(lastNameParam);

                    MySqlParameter usernameParam = new MySqlParameter("username", MySqlDbType.String);
                    usernameParam.Value = user.username;
                    query.Parameters.Add(usernameParam);

                    MySqlParameter passwordParam = new MySqlParameter("password", MySqlDbType.String);
                    passwordParam.Value = user.password;
                    query.Parameters.Add(passwordParam);

                    MySqlParameter userTypeParam = new MySqlParameter("userType", MySqlDbType.String);
                    userTypeParam.Value = user.userType.ToString();
                    query.Parameters.Add(userTypeParam);

                    MySqlParameter activeParam = new MySqlParameter("active", MySqlDbType.Byte);
                    activeParam.Value = user.active;
                    query.Parameters.Add(activeParam);

                    MySqlParameter userIDParam = new MySqlParameter("userID", MySqlDbType.Int32);
                    userIDParam.Value = user.userID;
                    query.Parameters.Add(userIDParam);


                    int rowsAffected = query.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        return false;
                }
            }
            catch (Exception) { return false; }

            return true;
        }
        public User? addUser(User user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand query = connection.CreateCommand();
                    query.CommandText = addUserQuery;

                    MySqlParameter firstNameParam = new MySqlParameter("firstName", MySqlDbType.String);
                    firstNameParam.Value = user.firstName;
                    query.Parameters.Add(firstNameParam);


                    MySqlParameter lastNameParam = new MySqlParameter("lastName", MySqlDbType.String);
                    lastNameParam.Value = user.lastName;
                    query.Parameters.Add(lastNameParam);

                    MySqlParameter usernameParam = new MySqlParameter("username", MySqlDbType.String);
                    usernameParam.Value = user.username;
                    query.Parameters.Add(usernameParam);

                    MySqlParameter passwordParam = new MySqlParameter("password", MySqlDbType.String);
                    passwordParam.Value = user.password;
                    query.Parameters.Add(passwordParam);

                    MySqlParameter userTypeParam = new MySqlParameter("userType", MySqlDbType.String);
                    userTypeParam.Value = user.userType.ToString();
                    query.Parameters.Add(userTypeParam);


                    query.ExecuteNonQuery();
                    user.userID = (int)query.LastInsertedId;

                    if(user is Accountant)
                    {
                        MySqlCommand tempQuery = connection.CreateCommand();
                        tempQuery.CommandText = addAccountantQuery;

                        MySqlParameter tempParam = new MySqlParameter("userID", MySqlDbType.Int32);
                        tempParam.Value = user.userID;
                        tempQuery.Parameters.Add(tempParam);

                        tempQuery.ExecuteNonQuery();
                    }
                    else if(user is Member)
                    {
                        MySqlCommand tempQuery = connection.CreateCommand();
                        tempQuery.CommandText = addMemberQuery;

                        MySqlParameter tempParam = new MySqlParameter("userID", MySqlDbType.Int32);
                        tempParam.Value = user.userID;
                        tempQuery.Parameters.Add(tempParam);

                        tempQuery.ExecuteNonQuery();
                    }

                    return user;
                }
            }
            catch (Exception) { return null; }
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
