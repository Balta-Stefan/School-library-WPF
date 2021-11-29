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
        private const string insertPublisherQuery = "INSERT INTO Publishers(publisherName) VALUES(@pubName)";

        private string connectionString;

        public PublisherDAO(string connectionString) => this.connectionString = connectionString;

        public Publisher? insertPublisher(Publisher publisher)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = insertPublisherQuery;

                MySqlParameter copyIDParam = new MySqlParameter("pubName", MySqlDbType.String);
                copyIDParam.Value = publisher.name;
                query.Parameters.Add(copyIDParam);

                int rowsAffected = query.ExecuteNonQuery();

                if (rowsAffected == 0)
                    return null;

                publisher.publisherID = (int)query.LastInsertedId;
                return publisher;
            }
        }
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
