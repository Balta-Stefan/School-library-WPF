using MySql.Data.MySqlClient;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.DAO
{
    public class BookDAO
    {
        private string connectionString;

        private const string getBooksQuery = "SELECT * FROM Books b INNER JOIN Publishers p on b.publisherID=p.publisherID INNER JOIN Genres g on b.genre=g.genreID INNER JOIN Authors a on b.authorID=a.authorID";
        private const string getCopiesQuery = "SELECT * FROM Book_copies copies INNER JOIN Book_conditions conds ON copies.conditionID=conds.conditionID WHERE copies.bookID=@bookID";
        private const string deleteCopyQuery = "DELETE FROM Book_copies WHERE bookCopyID=@copyID";
        private const string updateBookQuery = "UPDATE Books SET ISBN13=@isbn13, ISBN10=@isbn10, bookTitle=@newTitle, edition=@newEdition, authorID=@newAuthor, publisherID=@newPublisher, genre=@newGenre, numberOfCopies=@newNumberOfCopies WHERE bookID=@bookID";

        public BookDAO(string connectionString) => this.connectionString = connectionString;

        public List<Book> getBooks()
        {
            List<Book> books = new List<Book>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getBooksQuery;

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int bookID = result.GetInt32("bookID");
                        string isbn13 = result.GetString("ISBN13");
                        string isbn10 = result.GetString("ISBN10");
                        string bookTitle = result.GetString("bookTitle");
                        short edition = result.GetInt16("edition");
                        Author author = new Author(result.GetInt32("authorID"), result.GetString("firstName"), result.GetString("lastName"));
                        Publisher publisher = new Publisher(result.GetInt32("publisherID"), result.GetString("publisherName"));
                        Genre genre = new Genre(result.GetInt32("genreID"), result.GetString("genreName"));
                        int numberOfCopies = result.GetInt32("numberOfCopies");

                        books.Add(new Book(bookID, isbn13, isbn10, bookTitle, edition, author, publisher, genre, numberOfCopies));
                    }
                }
            }

            return books;
        }

        public bool updateBook(Book book)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand query = connection.CreateCommand();
                    query.CommandText = updateBookQuery;

                    MySqlParameter bookIDParam = new MySqlParameter("bookID", MySqlDbType.Int32);
                    bookIDParam.Value = book.BookID;

                    MySqlParameter isbn13 = new MySqlParameter("isbn13", MySqlDbType.String);
                    isbn13.Value = book.ISBN13;

                    MySqlParameter isbn10 = new MySqlParameter("isbn10", MySqlDbType.String);
                    isbn10.Value = book.ISBN10;

                    MySqlParameter title = new MySqlParameter("newTitle", MySqlDbType.String);
                    title.Value = book.BookTitle;

                    MySqlParameter edition = new MySqlParameter("newEdition", MySqlDbType.Int16);
                    edition.Value = book.Edition;

                    MySqlParameter author = new MySqlParameter("newAuthor", MySqlDbType.Int32);
                    author.Value = book.Author.authorID;

                    MySqlParameter publisher = new MySqlParameter("newPublisher", MySqlDbType.Int32);
                    publisher.Value = book.Publisher.publisherID;

                    MySqlParameter genre = new MySqlParameter("newGenre", MySqlDbType.Int32);
                    genre.Value = book.Genre.genreID;

                    MySqlParameter numOfCopies = new MySqlParameter("newNumberOfCopies", MySqlDbType.Int32);
                    numOfCopies.Value = book.NumberOfCopies;


                    query.Parameters.Add(bookIDParam);
                    query.Parameters.Add(isbn13);
                    query.Parameters.Add(isbn10);
                    query.Parameters.Add(title);
                    query.Parameters.Add(edition);
                    query.Parameters.Add(author);
                    query.Parameters.Add(publisher);
                    query.Parameters.Add(genre);
                    query.Parameters.Add(numOfCopies);

                    //query.Parameters.AddWithValue("@bookID", wantedBook.bookID);

                    int rowsAffected = query.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        return true;
                }
            }
            catch (Exception) { }
            return false;
        }
        public bool removeBookCopy(BookCopy copy)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = deleteCopyQuery;

                MySqlParameter copyIDParam = new MySqlParameter("copyID", MySqlDbType.Int32);
                copyIDParam.Value = copy.bookCopyID;
                query.Parameters.Add(copyIDParam);


                //query.Parameters.AddWithValue("@bookID", wantedBook.bookID);

                int rowsAffected = query.ExecuteNonQuery();

                if (rowsAffected > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<BookCopy> getBookCopies(Book wantedBook)
        {
            List<BookCopy> copies = new List<BookCopy>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getCopiesQuery;

                MySqlParameter bookIdParam = new MySqlParameter("bookID", MySqlDbType.Int32);
                bookIdParam.Value = wantedBook.BookID;
                query.Parameters.Add(bookIdParam);


                //query.Parameters.AddWithValue("@bookID", wantedBook.bookID);

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int bookCopyID = result.GetInt32("bookCopyID");
                        BookCondition condition = new BookCondition(result.GetInt32("conditionID"), result.GetString("condition"));
                        DateTime deliveredAt = result.GetDateTime("deliveredAt");
                        bool available = result.GetBoolean("available");

                        copies.Add(new BookCopy(bookCopyID, condition, deliveredAt, wantedBook, available));
                    }
                }
            }

            return copies;
        }
    }
}
