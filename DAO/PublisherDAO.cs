using MySql.Data.MySqlClient;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.DAO
{
    public class PublisherDAO
    {
        private const string getPublishersQuery = "SELECT * FROM Publishers";

        private string connectionString;

        public PublisherDAO(string connectionString) => this.connectionString = connectionString;

        public List<Publisher> getPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getPublishersQuery;

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int ID = result.GetInt32("publisherID");
                        string name = result.GetString("publisherName");

                        publishers.Add(new Publisher(ID, name));
                    }
                }
            }

            return publishers;
        }
    }
}
