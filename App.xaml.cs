using School_library.DAO;
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

        private LoanView loanWiew;
        private BooksPanelView booksView;
        private MembersPanel membersPanel;

        public List<TabItem> tabs { get; } = new List<TabItem>();
        protected override void OnStartup(StartupEventArgs e)
        {
            User loggedInUser = new Librarian(4, "ime", "prezime", "username", "pass");

            BookDAO bookDao = new BookDAO(connectionString);
            PublisherDAO publisherDao = new PublisherDAO(connectionString);
            GenreDAO genreDao = new GenreDAO(connectionString);
            AuthorDAO authorDao = new AuthorDAO(connectionString);
            UserDAO userDao = new UserDAO(connectionString);
            LoansDAO loansDao = new LoansDAO(connectionString);

           
            AccessText loansTabItemHotkey = new AccessText();
            loansTabItemHotkey.Text = "_Loans";

            TabItem loansTab = new TabItem();
            loansTab.Header = loansTabItemHotkey;//"_Loans";
            LoansPanelViewModel loansViewModel = new LoansPanelViewModel(loansDao, userDao, bookDao, loggedInUser);
            loanWiew = new LoanView()
            {
                DataContext = loansViewModel
            };
            loansTab.Content = loanWiew;

            AccessText booksTabItemHotkey = new AccessText();
            booksTabItemHotkey.Text = "_Books";
            TabItem booksTab = new TabItem();
            booksTab.Header = booksTabItemHotkey;//"_Books";
            BooksPanelViewModel booksPanelViewModel = new BooksPanelViewModel(bookDao, publisherDao, genreDao, authorDao);
            booksView = new BooksPanelView()
            {
                DataContext = booksPanelViewModel
            };
            booksTab.Content = booksView;

            AccessText membersTabItemHotkey = new AccessText();
            membersTabItemHotkey.Text = "_Members";
            TabItem membersTab = new TabItem();
            membersTab.Header = membersTabItemHotkey;//"Members";
            MembersPanelViewModel membersViewModel = new MembersPanelViewModel(userDao);
            membersPanel = new MembersPanel()
            {
                DataContext = membersViewModel
            };
            membersTab.Content = membersPanel;

            AccessText settingsTabItemHotkey = new AccessText();
            settingsTabItemHotkey.Text = "_Settings";
            TabItem settingsTab = new TabItem();
            settingsTab.Header = settingsTabItemHotkey;// "Settings";
            SettingsView settingsView = new SettingsView();
            settingsTab.Content = settingsView;



            tabs.Add(loansTab);
            tabs.Add(booksTab);
            tabs.Add(membersTab);
            tabs.Add(settingsTab);
            

            MainWindow mainWindow = new MainWindow()
            {
                DataContext = this
            };
            mainWindow.Show();

            base.OnStartup(e);
        }

        public void changeTheme(ResourceDictionary dic)
        {
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(dic);

            loanWiew.Resources.MergedDictionaries.Clear();
            loanWiew.Resources.MergedDictionaries.Add(dic);

            booksView.Resources.MergedDictionaries.Clear();
            booksView.Resources.MergedDictionaries.Add(dic);

            membersPanel.Resources.MergedDictionaries.Clear();
            membersPanel.Resources.MergedDictionaries.Add(dic);
        }
    }
}
