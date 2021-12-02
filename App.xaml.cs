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
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private MainWindow mainWindow;

        public List<TabItem> tabs { get; } = new List<TabItem>();

        private User? login(UserDAO userDao)
        {
            ObservableCollection<LanguageAndFlag> flags = new ObservableCollection<LanguageAndFlag>();

            flags.Add(new LanguageAndFlag("en", @"..\Resources\English.jpg"));
            flags.Add(new LanguageAndFlag("bs", @"..\Resources\Bosanski.png"));


            LoginViewModel loginViewModel = new LoginViewModel(userDao, flags);
            LoginWindow loginWindow = new LoginWindow
            {
                DataContext = loginViewModel
            };
            loginWindow.ShowDialog();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(loginViewModel.selectedLanguage.language);

            return loginViewModel.user;
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            //User loggedInUser = new Librarian(4, "ime", "prezime", "username", "pass", "", "");

            BookDAO bookDao = new BookDAO(connectionString);
            PublisherDAO publisherDao = new PublisherDAO(connectionString);
            GenreDAO genreDao = new GenreDAO(connectionString);
            AuthorDAO authorDao = new AuthorDAO(connectionString);
            UserDAO userDao = new UserDAO(connectionString);
            LoansDAO loansDao = new LoansDAO(connectionString);

            User? loggedInUser = login(userDao);
            if (loggedInUser == null)
                return;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("bs");


            AccessText loansTabItemHotkey = new AccessText();
            loansTabItemHotkey.Text = School_library.Resources.LoansTabName;

            TabItem loansTab = new TabItem();
            loansTab.Header = loansTabItemHotkey;//"_Loans";
            loanWiew = new LoanView();
            LoansPanelViewModel loansViewModel = new LoansPanelViewModel(loansDao, userDao, bookDao, loggedInUser, loanWiew.Resources.MergedDictionaries);
            loanWiew.DataContext = loansViewModel;
            loansTab.Content = loanWiew;

            AccessText booksTabItemHotkey = new AccessText();
            booksTabItemHotkey.Text = School_library.Resources.BooksTabName;
            TabItem booksTab = new TabItem();
            booksTab.Header = booksTabItemHotkey;//"_Books";
            booksView = new BooksPanelView();
            BooksPanelViewModel booksPanelViewModel = new BooksPanelViewModel(bookDao, publisherDao, genreDao, authorDao, booksView.Resources.MergedDictionaries);
            booksView.DataContext = booksPanelViewModel;
            booksTab.Content = booksView;

            AccessText membersTabItemHotkey = new AccessText();
            membersTabItemHotkey.Text = School_library.Resources.MembersTabName;
            TabItem membersTab = new TabItem();
            membersTab.Header = membersTabItemHotkey;//"Members";
            membersPanel = new MembersPanel();
            MembersPanelViewModel membersViewModel = new MembersPanelViewModel(userDao, membersPanel.Resources.MergedDictionaries);
            membersPanel.DataContext = membersViewModel;
            membersTab.Content = membersPanel;

            AccessText settingsTabItemHotkey = new AccessText();
            settingsTabItemHotkey.Text = School_library.Resources.SettingsTabName;
            TabItem settingsTab = new TabItem();
            settingsTab.Header = settingsTabItemHotkey;// "Settings";
            SettingsView settingsView = new SettingsView();
            settingsTab.Content = settingsView;



            tabs.Add(loansTab);
            tabs.Add(booksTab);
            tabs.Add(membersTab);
            tabs.Add(settingsTab);
            

            mainWindow = new MainWindow()
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

            mainWindow.TempTabControl.Resources.MergedDictionaries.Clear();
            mainWindow.TempTabControl.Resources.MergedDictionaries.Add(dic);

            mainWindow.MainWindowMainGrid.Resources.MergedDictionaries.Clear();
            mainWindow.MainWindowMainGrid.Resources.MergedDictionaries.Add(dic);
        }

    }
}
