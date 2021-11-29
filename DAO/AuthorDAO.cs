using MySql.Data.MySqlClient;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.DAO
{
    public class AuthorDAO
    {
        private const string getAuthorsQuery = "SELECT * FROM Authors";
        private const string insertAuthorQuery = "INSERT INTO Authors(firstName, lastName) VALUES(@firstName, @lastName)";

        private string connectionString;

        public AuthorDAO(string connectionString) => this.connectionString = connectionString;

        public Author? insertAuthor(Author author)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = insertAuthorQuery;

                MySqlParameter firstNameParam = new MySqlParameter("firstName", MySqlDbType.String);
                firstNameParam.Value = author.firstName;
                query.Parameters.Add(firstNameParam);

                MySqlParameter lastNameParam = new MySqlParameter("lastName", MySqlDbType.String);
                lastNameParam.Value = author.lastName;
                query.Parameters.Add(lastNameParam);

                int rowsAffected = query.ExecuteNonQuery();

                if (rowsAffected == 0)
                    return null;

                author.authorID = (int)query.LastInsertedId;
                return author;
            }
        }
        public List<Author> getAuthors()
        {
            List<Author> authors = new List<Author>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getAuthorsQuery;

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int ID = result.GetInt32("authorID");
                        string firstName = result.GetString("firstName");
                        string lastName = result.GetString("lastName");


                        authors.Add(new Author(ID, firstName, lastName));
                    }
                }
            }

            return authors;
        }
    }
}
