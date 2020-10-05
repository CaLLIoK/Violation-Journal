using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для SearchInfo.xaml
    /// </summary>
    public partial class SearchInfo : Window
    {
        public SearchInfo()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void SearchJournalEntries_Click(object sender, RoutedEventArgs e)
        {
            SearchJournalEntries searchJournalEntries = new SearchJournalEntries();
            searchJournalEntries.Show();
            this.Close();
        }

        private void SearchViolators_Click(object sender, RoutedEventArgs e)
        {
            SearchViolators searchViolators = new SearchViolators();
            searchViolators.Show();
            this.Close();
        }

        private void SearchViolations_Click(object sender, RoutedEventArgs e)
        {
            SearchViolations searchViolations = new SearchViolations();
            searchViolations.Show();
            this.Close();
        }

        private void SearchViolatorsCars_Click(object sender, RoutedEventArgs e)
        {
            SearchViolatorsCars searchViolatorsCars = new SearchViolatorsCars();
            searchViolatorsCars.Show();
            this.Close();
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
            mainMenuEmployee.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "poisk_dannykh.htm");
        }
    }
}