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
