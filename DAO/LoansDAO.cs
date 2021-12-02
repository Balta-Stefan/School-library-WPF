using MySql.Data.MySqlClient;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_library.DAO
{
    public class LoansDAO
    {
        private string connectionString;
        private const string addLoanQuery = "INSERT INTO Loans(borrowDateTime, borrowedFromLibrarian, borrowerID, bookCopyID) VALUES(@borrowDateTime, @borrowedFromLibrarian, @borrowerID, @bookCopyID)";
        private const string updateLoanQuery = "UPDATE Loans SET returnedToLibrarian=@returnedToLibrarian, returnDateTime=@returnDateTime WHERE loanID=@loanID";
        private const string getLoansQuery = "SELECT * FROM Loans";
        private const string getUserQuery = "SELECT * FROM Users WHERE userID=@userID";
        private const string getBookQuery = "SELECT * FROM Books WHERE bookID=@bookID";
        private const string getBookCopyQuery = "SELECT * FROM Book_copies WHERE bookCopyID=@copyID";

        public LoansDAO(string connectionString) => this.connectionString = connectionString;
    
        private Librarian? getLibrarian(int librarianID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getUserQuery;

                MySqlParameter idParam = new MySqlParameter("userID", MySqlDbType.Int32);
                idParam.Value = librarianID;
                query.Parameters.Add(idParam);

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        string firstName = result.GetString("firstName");
                        string lastName = result.GetString("lastName");

                        Librarian lib = new Librarian(librarianID, firstName, lastName, string.Empty, string.Empty, null, null);
                        return lib;
                    }
                }
            }
            return null;
        }

        private Member? getMember(int memberID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getUserQuery;

                MySqlParameter idParam = new MySqlParameter("userID", MySqlDbType.Int32);
                idParam.Value = memberID;
                query.Parameters.Add(idParam);

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        string firstName = result.GetString("firstName");
                        string lastName = result.GetString("lastName");

                        Member mem = new Member(memberID, firstName, lastName, string.Empty, string.Empty, null, null);
                        return mem;
                    }
                }
            }
        
            return null;
        }

        private Book? getBook(int bookID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getBookQuery;

                MySqlParameter idParam = new MySqlParameter("bookID", MySqlDbType.Int32);
                idParam.Value = bookID;
                query.Parameters.Add(idParam);

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        string isbn13 = result.GetString("ISBN13");
                        string isbn10 = result.GetString("isbn10");
                        string title = result.GetString("bookTitle");
                        short edition = result.GetInt16("edition");
                        int authorID = result.GetInt32("authorID");
                        int publisherID = result.GetInt32("publisherID");
                        int genre = result.GetInt32("genre");
                        int numOfCopies = result.GetInt32("numberOfCopies");

                        return new Book(bookID, isbn13, isbn10, title, edition, new Author(authorID, string.Empty, string.Empty), new Publisher(publisherID, string.Empty), new Genre(genre, string.Empty), numOfCopies);
                    }
                }
            }
            return null;
        }

        private BookCopy getBookCopy(int copyID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getBookCopyQuery;

                MySqlParameter idParam = new MySqlParameter("copyID", MySqlDbType.Int32);
                idParam.Value = copyID;
                query.Parameters.Add(idParam);

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int bookID = result.GetInt32("bookID");
                        Book? book = getBook(bookID);

                        int conditionID = result.GetInt32("conditionID");
                        DateTime deliveredAt = result.GetDateTime("deliveredAt");
                        bool available = result.GetBoolean("available");

                        return new BookCopy(copyID, new BookCondition(conditionID, string.Empty), deliveredAt, book, available);
                    }
                }
            }
            return null;
        }

        public List<Loan> getLoans()
        {
            List<Loan> loans = new List<Loan>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = getLoansQuery;

                using (MySqlDataReader result = query.ExecuteReader())
                {
                    while (result.Read())
                    {
                        int borrowerID = result.GetInt32("borrowerID");
                        Member? borrower = getMember(borrowerID);

                        int borrowedFromID = result.GetInt32("borrowedFromLibrarian");
                        Librarian? borrowedFrom = getLibrarian(borrowedFromID);

                        int returnedToIndex = result.GetOrdinal("returnedToLibrarian");
                        Librarian? returnedTo = null;
                        if (result.IsDBNull(returnedToIndex) == false)
                        {
                            int returnedToID = result.GetInt32("returnedToLibrarian");
                            returnedTo = getLibrarian(returnedToID);
                        }

                        DateTime borrowDateTime = result.GetDateTime("borrowDateTime");
                        DateTime? returnDateTime = null;
                        int returnedDateTimeIndex = result.GetOrdinal("returnDateTime");
                        if(result.IsDBNull(returnedDateTimeIndex) == false)
                        {
                            returnDateTime = result.GetDateTime("borrowDateTime");
                        }

                        int loanID = result.GetInt32("loanID");
                        int bookCopyID = result.GetInt32("bookCopyID");
                        BookCopy? copy = getBookCopy(bookCopyID);

                        loans.Add(new Loan(loanID, borrowDateTime, borrowedFrom, borrower, copy, returnedTo, returnDateTime));
                    }
                }
            }

            return loans;
        }
        public bool updateLoan(Loan loan)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = updateLoanQuery;

                MySqlParameter returnedToParam = new MySqlParameter("returnedToLibrarian", MySqlDbType.Int32);
                returnedToParam.Value = loan.returnedToLibrarian.userID;
                query.Parameters.Add(returnedToParam);

                MySqlParameter returnedAtParam = new MySqlParameter("returnDateTime", MySqlDbType.DateTime);
                returnedAtParam.Value = loan.returnDateTIme;
                query.Parameters.Add(returnedAtParam);

                MySqlParameter loanIDParam = new MySqlParameter("loanID", MySqlDbType.Int32);
                loanIDParam.Value = loan.loanID;
                query.Parameters.Add(loanIDParam);

                int rowsAffected = query.ExecuteNonQuery();

                if (rowsAffected == 0)
                    return false;

                
                return true;
            }
        }
        public Loan? addLoan(Loan loan)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand query = connection.CreateCommand();
                query.CommandText = addLoanQuery;

                MySqlParameter borrowDateTimeParam = new MySqlParameter("borrowDateTime", MySqlDbType.DateTime);
                borrowDateTimeParam.Value = loan.borrowDateTime;
                query.Parameters.Add(borrowDateTimeParam);

                MySqlParameter borrowedFromLibrarianParam = new MySqlParameter("borrowedFromLibrarian", MySqlDbType.Int32);
                borrowedFromLibrarianParam.Value = loan.borrowedFromLibrarian.userID;
                query.Parameters.Add(borrowedFromLibrarianParam);

                MySqlParameter borrowerIDParam = new MySqlParameter("borrowerID", MySqlDbType.Int32);
                borrowerIDParam.Value = loan.borrower.userID;
                query.Parameters.Add(borrowerIDParam);

                MySqlParameter bookCopyIDParam = new MySqlParameter("bookCopyID", MySqlDbType.Int32);
                bookCopyIDParam.Value = loan.bookCopy.bookCopyID;
                query.Parameters.Add(bookCopyIDParam);

               

                int rowsAffected = query.ExecuteNonQuery();

                if (rowsAffected == 0)
                    return null;

                loan.loanID = (int)query.LastInsertedId;
                return loan;
            }
        }
    }
}
