using School_library.Models;
using School_library.ViewModels;
using School_library.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace School_library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        private string? connectionString = null;//"Server=localhost;Database=mydb;Uid=root;Pwd=sigurnost;";
        private mydbContext dbContext;

        private LoanView loanWiew;
        private BooksPanelView booksView;
        private MembersPanel membersPanel;
        private MainWindow mainWindow;
        private SettingsView settingsView;

        private TabItem? selectedTab = null;

        public List<TabItem> tabs { get; } = new List<TabItem>();

        private void loadResources()
        {
            connectionString = ConfigurationManager.AppSettings["connection_string"];
            dbContext = new mydbContext(connectionString);
        }
        private UserViewModel? login()
        {
            ObservableCollection<LanguageAndFlag> flags = new ObservableCollection<LanguageAndFlag>();

            flags.Add(new LanguageAndFlag("bs", @"..\Resources\Bosanski.png"));
            flags.Add(new LanguageAndFlag("en", @"..\Resources\English.jpg"));

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("bs");

            LoginViewModel loginViewModel = new LoginViewModel(dbContext, flags);
            LoginWindow loginWindow = new LoginWindow
            {
                DataContext = loginViewModel
            };
            
           
            try
            {
                loginWindow.ShowDialog();
            }
            catch(Exception)
            {
                loginWindow.Close();
                throw new Exception("Login error");
            }

            if (loginViewModel.user == null)
                return null;

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(loginViewModel.selectedLanguage.language);
            loginViewModel.user.Localization = loginViewModel.selectedLanguage.language;

            return loginViewModel.user;
        }

        private void setTheme(string theme)
        {
            if (theme == null)
                return;
            if(theme.Equals("dark"))
            {
                using (FileStream fs = new FileStream(@"Themes\DarkTheme.xaml", FileMode.Open))
                {
                    ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                    Resources.MergedDictionaries.Clear();
                    Resources.MergedDictionaries.Add(dic);

                    changeTheme(dic);
                }
            }
            else if(theme.Equals("light"))
            {
                using (FileStream fs = new FileStream(@"Themes\LightTheme.xaml", FileMode.Open))
                {
                    ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                    Resources.MergedDictionaries.Clear();
                    Resources.MergedDictionaries.Add(dic);

                    changeTheme(dic);
                }
            }
            else if(theme.Equals("big font"))
            {
                using (FileStream fs = new FileStream(@"Themes\BigFontTheme.xaml", FileMode.Open))
                {
                    ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                    Resources.MergedDictionaries.Clear();
                    Resources.MergedDictionaries.Add(dic);

                    changeTheme(dic);
                }
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            loadResources();
            
            if(connectionString == null)
            {
                MessageBox.Show(School_library.Resources.AppInitFailed, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //User loggedInUser = new Librarian(4, "ime", "prezime", "username", "pass", "", "");


            UserViewModel? loggedInUser = null;

            try
            {
                loggedInUser = login();
                if (loggedInUser == null)
                    return;
            }
            catch (Exception)
            {
                MessageBox.Show(School_library.Resources.CouldntAccessDatabase, School_library.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("bs");
            AccountTypesEnum LoggedInUserType = (AccountTypesEnum)Enum.Parse<AccountTypesEnum>(loggedInUser.UserType);

            AccessText loansTabItemHotkey = new AccessText();
            loansTabItemHotkey.Text = School_library.Resources.LoansTabName;

            TabItem loansTab = new TabItem();
            loansTab.Header = loansTabItemHotkey;//"_Loans";
            loanWiew = new LoanView();
            LoansPanelViewModel loansViewModel = new LoansPanelViewModel(dbContext, loggedInUser, loanWiew.Resources.MergedDictionaries, LoggedInUserType);
            loanWiew.DataContext = loansViewModel;
            loansTab.Content = loanWiew;

            AccessText booksTabItemHotkey = new AccessText();
            booksTabItemHotkey.Text = School_library.Resources.BooksTabName;
            TabItem booksTab = new TabItem();
            booksTab.Header = booksTabItemHotkey;//"_Books";
            booksView = new BooksPanelView();
            BooksPanelViewModel booksPanelViewModel = new BooksPanelViewModel(dbContext, booksView.Resources.MergedDictionaries, LoggedInUserType);
            booksView.DataContext = booksPanelViewModel;
            booksTab.Content = booksView;

            AccessText membersTabItemHotkey = new AccessText();
            membersTabItemHotkey.Text = School_library.Resources.MembersTabName;
            TabItem membersTab = new TabItem();
            membersTab.Header = membersTabItemHotkey;//"Members";
            membersPanel = new MembersPanel();
            MembersPanelViewModel membersViewModel = new MembersPanelViewModel(dbContext, membersPanel.Resources.MergedDictionaries, LoggedInUserType);
            membersPanel.DataContext = membersViewModel;
            membersTab.Content = membersPanel;

            AccessText settingsTabItemHotkey = new AccessText();
            settingsTabItemHotkey.Text = School_library.Resources.SettingsTabName;
            TabItem settingsTab = new TabItem();
            settingsTab.Header = settingsTabItemHotkey;// "Settings";
            SettingsViewModel settingsViewModel = new SettingsViewModel(dbContext, loggedInUser, this);
            settingsView = new SettingsView()
            {
                DataContext = settingsViewModel
            };
            settingsTab.Content = settingsView;
            


            tabs.Add(loansTab);
            tabs.Add(booksTab);
            tabs.Add(membersTab);
            tabs.Add(settingsTab);
            

            mainWindow = new MainWindow()
            {
                DataContext = this
            };
            mainWindow.TempTabControl.SelectionChanged += TempTabControl_SelectionChanged;
            setTheme(loggedInUser.Theme);
            mainWindow.Show();


            base.OnStartup(e);
        }

        private void TempTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            TabItem newTab = (TabItem)tc.SelectedItem;
            if (newTab == selectedTab)
                return;
            selectedTab = newTab;

            UserControl uc = (UserControl)selectedTab.Content;
            if(uc.DataContext is IWindowWithFilter)
            {
                IWindowWithFilter viewModel = (IWindowWithFilter)uc.DataContext;
                viewModel.filter();
            }
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

            settingsView.Resources.MergedDictionaries.Clear();
            settingsView.Resources.MergedDictionaries.Add(dic);

            mainWindow.MainWindowMainGrid.Resources.MergedDictionaries.Clear();
            mainWindow.MainWindowMainGrid.Resources.MergedDictionaries.Add(dic);
        }

    }
}
