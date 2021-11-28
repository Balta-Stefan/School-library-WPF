﻿using School_library.DAO;
using School_library.Models;
using School_library.ViewModels;
using School_library.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace School_library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        private const string connectionString = "Server=localhost;Database=mydb;Uid=root;Pwd=sigurnost;";

        public List<TabItem> tabs { get; } = new List<TabItem>();
        protected override void OnStartup(StartupEventArgs e)
        {
            BookDAO bookDao = new BookDAO(connectionString);
            PublisherDAO publisherDao = new PublisherDAO(connectionString);
            GenreDAO genreDao = new GenreDAO(connectionString);
            AuthorDAO authorDao = new AuthorDAO(connectionString);
            UserDAO userDao = new UserDAO(connectionString);

            /*ObservableCollection<Book> books = new ObservableCollection<Book>();
            ObservableCollection<Publisher> publishers = new ObservableCollection<Publisher>();
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();

            books.Add(new Book(1, "prvi isbn13", "prvi isbn10", "prva knjiga", 1, new Author(1, "Marko", "Markovic"), new Publisher(1, "prvi izdavac"), new Genre(1, "prvi zanr"), 1));
            books.Add(new Book(1, "drugi isbn13", "drugi isbn10", "druga knjiga", 1, new Author(1, "Zivko", "Zivkovic"), new Publisher(2, "Drugi izdavac"), new Genre(2, "drugi zanr"), 1));

            publishers.Add(new Publisher(1, "prvi izdavac"));
            publishers.Add(new Publisher(1, "drugi izdavac"));

            genres.Add(new Genre(1, "prvi zanr"));
            genres.Add(new Genre(1, "drugi zanr"));*/

            TabItem loansTab = new TabItem();
            loansTab.Header = "Loans";
            loansTab.Content = "just loans";

            TabItem booksTab = new TabItem();
            booksTab.Header = "Books";
            BooksPanelViewModel booksPanelViewModel = new BooksPanelViewModel(bookDao, publisherDao, genreDao, authorDao);
            BooksPanelView booksView = new BooksPanelView()
            {
                DataContext = booksPanelViewModel
            };
            booksTab.Content = booksView;

            TabItem membersTab = new TabItem();
            membersTab.Header = "Members";
            MembersPanelViewModel membersViewModel = new MembersPanelViewModel(userDao);
            MembersPanel membersPanel = new MembersPanel()
            {
                DataContext = membersViewModel
            };
            membersTab.Content = membersPanel;

            TabItem publishersTab = new TabItem();
            publishersTab.Header = "Publishers";
            publishersTab.Content = "Just publishers";

            TabItem genresTab = new TabItem();
            genresTab.Header = "Genres";
            genresTab.Content = "Just genres";

            TabItem settingsTab = new TabItem();
            settingsTab.Header = "Settings";
            settingsTab.Content = "Just settings";

            
            tabs.Add(loansTab);
            tabs.Add(booksTab);
            tabs.Add(membersTab);
            tabs.Add(publishersTab);
            tabs.Add(genresTab);
            tabs.Add(settingsTab);

            MainWindow mainWindow = new MainWindow()
            {
                DataContext = this
            };
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
