using School_library.Models;
using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace School_library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            ObservableCollection<Publisher> publishers = new ObservableCollection<Publisher>();
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();

            books.Add(new Book(1, "", "", "prva knjiga", 1, new Author(1, "Marko", "Markovic"), null, null));
            books.Add(new Book(1, "", "", "druga knjiga", 1, new Author(1, "Zivko", "Zivkovic"), null, null));

            publishers.Add(new Publisher(1, "prvi izdavac"));
            publishers.Add(new Publisher(1, "drugi izdavac"));

            genres.Add(new Genre(1, "prvi zanr"));
            genres.Add(new Genre(1, "drugi zanr"));


            BooksPanelViewModel booksPanelViewModel = new BooksPanelViewModel(books, publishers, genres);

            MainWindow mainWindow = new MainWindow()
            {
                DataContext = booksPanelViewModel
            };
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
