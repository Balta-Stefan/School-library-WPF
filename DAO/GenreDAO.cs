using MySql.Data.MySqlClient;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.DAO
{
    public class GenreDAO
    {
        private const string getGenresQuery = "SELECT * FROM Genres";

        private string connectionString;

        public GenreDAO(string connectionString) => this.connectionString = connectionString;

        public List<Genre> getGenres()
        {
            List<Genre> genres = new List<Genre>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getGenresQuery;

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int ID = result.GetInt32("genreID");
                        string name = result.GetString("genreName");

                        genres.Add(new Genre(ID, name));
                    }
                }
            }

            return genres;
        }
    }
}
