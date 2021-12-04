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
    public class EditBookInfoViewModel : ViewModelBase
    {
        private readonly mydbContext dbContext;

        public ObservableCollection<BookCopyViewModel> bookCopies { get; } = new ObservableCollection<BookCopyViewModel>();
        public ObservableCollection<BookConditionViewModel> bookConditions { get; } = new ObservableCollection<BookConditionViewModel>();
        public BookConditionViewModel? selectedCondition { get; set; } = null;
        public DateTime? selectedDate { get; set; } = DateTime.Now;
        private BookViewModel book;

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

        public ObservableCollection<GenreViewModel> genres { get; } = new ObservableCollection<GenreViewModel>();
        public ObservableCollection<PublisherViewModel> publishers { get; } = new ObservableCollection<PublisherViewModel>();
        public ObservableCollection<AuthorViewModel> authors { get; } = new ObservableCollection<AuthorViewModel>();

        private BookCopyViewModel? selectedCopy = null;
        public BookCopyViewModel? SelectedCopy
        {
            get { return selectedCopy; }
            set
            {
                copySelected = true;
                selectedCopy = value;
                OnPropertyChange("SelectedCopy");
                deleteCopyCommand.executeChanged();
            }
        }
        private GenreViewModel? selectedGenre = null;
        public GenreViewModel? SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                selectedGenre = value;
                OnPropertyChange("SelectedGenre");
            }
        }

        private AuthorViewModel? selectedAuthor = null;
        public AuthorViewModel? SelectedAuthor
        {
            get { return selectedAuthor; }
            set
            {
                selectedAuthor = value;
                OnPropertyChange("SelectedAuthor");
            }
        }

        private PublisherViewModel? selectedPublisher = null;
        public PublisherViewModel? SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                selectedPublisher = value;
                OnPropertyChange("SelectedPublisher");
            }
        }
        
        public DeleteBookCopyCommand deleteCopyCommand { get; }
        public ICommand editBookInfoCommand { get; }
        public ICommand addCopyCommand { get; }
        public bool copySelected { get; private set; } = false;
        public EditBookInfoViewModel(mydbContext dbContext, BookViewModel book)
        {
            this.dbContext = dbContext;
            this.book = book;

            foreach (Genre g in dbContext.Genres.ToList()) genres.Add(new GenreViewModel(g));
            foreach (Publisher g in dbContext.Publishers.ToList()) publishers.Add(new PublisherViewModel(g));
            foreach (Author g in dbContext.Authors.ToList()) authors.Add(new AuthorViewModel(g));
            foreach (BookCondition g in dbContext.BookConditions.ToList()) bookConditions.Add(new BookConditionViewModel(g));
            foreach (BookCopy g in dbContext.BookCopies.ToList()) bookCopies.Add(new BookCopyViewModel(g));

            SelectedAuthor = new AuthorViewModel(book.Author);
            SelectedGenre = new GenreViewModel(book.GenreNavigation);
            SelectedPublisher = new PublisherViewModel(book.Publisher);

            /*genres = new ObservableCollection<Genre>(dbContext.Genres.ToList());
            publishers = new ObservableCollection<Publisher>(dbContext.Publishers.ToList());
            authors = new ObservableCollection<Author>(dbContext.Authors.ToList());
            bookConditions = new ObservableCollection<BookCondition>(dbContext.BookConditions.ToList());
            bookCopies = new ObservableCollection<BookCopy>(dbContext.BookCopies.ToList());*/


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
                Condition = selectedCondition.BookCondition,
                DeliveredAt = selectedDate.Value,
                Book = book.Book
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
            bookCopies.Add(new BookCopyViewModel(newCopy));
            book.NumberOfCopies++;
            
        }

        public void deleteCopy()
        {
            if(selectedCopy.Available == false)
            {
                var result = MessageBox.Show(School_library.Resources.BookCopyTakenWarning, "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result.Equals(MessageBoxResult.No))
                {
                    return;
                }
            }

            try
            {
                dbContext.BookCopies.Remove(selectedCopy.BookCopy);
                dbContext.SaveChanges();
                book.NumberOfCopies--;
                bookCopies.Remove(selectedCopy);
                SelectedCopy = null;

                copySelected = false;
                deleteCopyCommand.executeChanged();
            }
            catch (Exception)
            {
                MessageBox.Show(School_library.Resources.CouldntDeleteBookCopy, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void updateBookInfo()
        {
            book.Isbn13 = isbn13;
            book.Isbn10 = isbn10;
            book.BookTitle = bookTitle;
            book.Edition = edition;
            book.Author = selectedAuthor.Author;
            book.Publisher = selectedPublisher.Publisher;
            book.GenreNavigation = selectedGenre.Genre;

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
