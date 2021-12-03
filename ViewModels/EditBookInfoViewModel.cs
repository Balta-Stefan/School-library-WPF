using School_library.Commands;
using School_library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace School_library.ViewModels
{
    public class EditBookInfoViewModel
    {
        private readonly mydbContext dbContext;

        public ObservableCollection<BookCopy> bookCopies { get; }
        public ObservableCollection<BookCondition> bookConditions { get; }
        public BookCondition? selectedCondition { get; set; } = null;
        public DateTime? selectedDate { get; set; } = DateTime.Now;
        private Book book;

        private string bookTitle;
        public string BookTitle
        {
            get { return bookTitle; }
            set
            {
                bookTitle = value;
            }
        }

        private string isbn10;
        public string ISBN10
        {
            get { return isbn10; }
            set
            {
                isbn10 = value;
            }
        }
        private string isbn13;
        public string ISBN13
        {
            get { return isbn13; }
            set
            {
                isbn13 = value;
            }
        }
        private short edition;
        public string Edition
        {
            get { return edition.ToString(); }
            set
            {
                try
                {
                    edition = short.Parse(value);
                }
                catch (Exception) { }
            }
        }

        public ObservableCollection<Genre> genres { get; }
        public ObservableCollection<Publisher> publishers { get; }
        public ObservableCollection<Author> authors { get; }

        private int selectedCopyIndex = -1;
        public int SelectedCopyIndex
        {
            get { return selectedCopyIndex; }
            set
            {
                selectedCopyIndex = value;
                copySelected = true;
                deleteCopyCommand.executeChanged();
            }
        }

        private int selectedGenreIndex = -1;
        public int SelectedGenreIndex
        {
            get { return selectedGenreIndex; }
            set { selectedGenreIndex = value; }
        }

        private int selectedAuthorIndex = -1;
        public int SelectedAuthorIndex
        {
            get { return selectedAuthorIndex; }
            set { selectedAuthorIndex = value; }
        }

        private int selectedPublisherIndex = -1;
        public int SelectedPublisherIndex
        {
            get { return selectedPublisherIndex; }
            set { selectedPublisherIndex = value; }
        }

        public DeleteBookCopyCommand deleteCopyCommand { get; }
        public ICommand editBookInfoCommand { get; }
        public ICommand addCopyCommand { get; }
        public bool copySelected { get; private set; } = false;
        public EditBookInfoViewModel(mydbContext dbContext, Book book)
        {
            this.dbContext = dbContext;
            this.book = book;
            this.bookCopies  = new ObservableCollection<BookCopy>(dbContext.BookCopies.ToList());
            this.bookConditions = new ObservableCollection<BookCondition>(dbContext.BookConditions.ToList());
            this.genres = new ObservableCollection<Genre>(dbContext.Genres.ToList());
            this.publishers = new ObservableCollection<Publisher>(dbContext.Publishers.ToList());
            this.authors = new ObservableCollection<Author>(dbContext.Authors.ToList());

            selectedGenreIndex = genres.IndexOf(book.GenreNavigation);
            selectedAuthorIndex = authors.IndexOf(book.Author);
            selectedPublisherIndex = publishers.IndexOf(book.Publisher);

            deleteCopyCommand = new DeleteBookCopyCommand(this);
            editBookInfoCommand = new EditBookInfoCommand(this);
            addCopyCommand = new AddBookCopyCommand(this);

            isbn10 = book.Isbn10;
            isbn13 = book.Isbn13;
            bookTitle = book.BookTitle;
            edition = book.Edition.Value;
        }

        public void addCopy()
        {
            if(selectedCondition == null)
            {
                MessageBox.Show(School_library.Resources.SelectConditionMessage, School_library.Resources.NoConditionSpecified, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (selectedDate == null)
            {
                MessageBox.Show(School_library.Resources.SelectDateMessage, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            BookCopy newCopy = new BookCopy()
            {
                Condition = selectedCondition,
                DeliveredAt = selectedDate.Value,
                Book = book
            };

            dbContext.BookCopies.Add(newCopy);

            try
            {
                dbContext.SaveChanges();
            }

            catch(Exception)
            {
                MessageBox.Show(School_library.Resources.CouldntAddCopy, School_library.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            MessageBox.Show(School_library.Resources.CopyAddedMessage, "", MessageBoxButton.OK);
            bookCopies.Add(newCopy);
            book.NumberOfCopies++;
            
        }

        public void deleteCopy()
        {
            BookCopy selectedCopy = bookCopies.ElementAt(selectedCopyIndex);

            dbContext.BookCopies.Remove(selectedCopy);
            try
            {
                dbContext.SaveChanges();
                book.NumberOfCopies--;
                bookCopies.RemoveAt(selectedCopyIndex);
                selectedCopyIndex = -1;

                copySelected = false;
                deleteCopyCommand.executeChanged();
            }
            catch (Exception) { }
        }

        public void updateBookInfo()
        {
            Author selectedAuthor = authors.ElementAt(selectedAuthorIndex);
            Genre selectedGenre = genres.ElementAt(selectedGenreIndex);
            Publisher selectedPublisher = publishers.ElementAt(selectedPublisherIndex);

            book.Isbn13 = isbn13;
            book.Isbn10 = isbn10;
            book.BookTitle = bookTitle;
            book.Edition = edition;
            book.Author = selectedAuthor;
            book.Publisher = selectedPublisher;
            book.GenreNavigation = selectedGenre;

            try
            {
                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                MessageBox.Show("Update not successful", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
