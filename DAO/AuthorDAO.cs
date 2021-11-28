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

        private string connectionString;

        public AuthorDAO(string connectionString) => this.connectionString = connectionString;

        public List<Author> getAuthors()
        {
            List<Author> genres = new List<Author>();

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


                        genres.Add(new Author(ID, firstName, lastName));
                    }
                }
            }

            return genres;
        }
    }
}
