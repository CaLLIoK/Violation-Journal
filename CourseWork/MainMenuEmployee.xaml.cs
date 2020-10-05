using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для MainMenuEmployee.xaml
    /// </summary>
    public partial class MainMenuEmployee : Window
    {
        public MainMenuEmployee()
        {
            InitializeComponent();
        }

        private void AddVoilator_Click(object sender, RoutedEventArgs e)
        {
            AddViolator addViolator = new AddViolator();
            addViolator.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            this.Close();
        }

        private void DeleteViolator_Click(object sender, RoutedEventArgs e)
        {
            DeleteViolator deleteViolator = new DeleteViolator();
            deleteViolator.Show();
            this.Close();
        }

        private void ChangeViolator_Click(object sender, RoutedEventArgs e)
        {
            ChangeViolatorData changeViolatorData = new ChangeViolatorData();
            changeViolatorData.Show();
            this.Close();
        }

        private void AddViolation_Click(object sender, RoutedEventArgs e)
        {
            AddViolation addViolation = new AddViolation();
            addViolation.Show();
            this.Close();
        }

        private void DeleteViolation_Click(object sender, RoutedEventArgs e)
        {
            DeleteViolation deleteViolation = new DeleteViolation();
            deleteViolation.Show();
            this.Close();
        }

        private void ChangeViolations_Click(object sender, RoutedEventArgs e)
        {
            ChangeViolationsData changeViolationsData = new ChangeViolationsData();
            changeViolationsData.Show();
            this.Close();
        }

        private void AddVilatorCar_Click(object sender, RoutedEventArgs e)
        {
            AddViolatorCar addViolatorCar = new AddViolatorCar();
            addViolatorCar.Show();
            this.Close();
        }

        private void DeleteViolatorCar_Click(object sender, RoutedEventArgs e)
        {
            DeleteViolatorCar deleteViolatorCar = new DeleteViolatorCar();
            deleteViolatorCar.Show();
            this.Close();
        }

        private void ChangeViolatorsCars_Click(object sender, RoutedEventArgs e)
        {
            ChangeViolatorsCars changeViolarorsCars = new ChangeViolatorsCars();
            changeViolarorsCars.Show();
            this.Close();
        }

        private void AddJournalEntry_Click(object sender, RoutedEventArgs e)
        {
            AddJournalEntry addJournalEntry = new AddJournalEntry();
            addJournalEntry.Show();
            this.Close();
        }

        private void ChangeViolationsJournal_Click(object sender, RoutedEventArgs e)
        {
            ChangeJournalEntry changeJournalEntry = new ChangeJournalEntry();
            changeJournalEntry.Show();
            this.Close();
        }

        private void ShowViolatorViolations_Click(object sender, RoutedEventArgs e)
        {
            FIndViolatorViolations fIndViolatorViolations = new FIndViolatorViolations();
            fIndViolatorViolations.Show();
            this.Close();
        }

        private void ChangeAccountData_Click(object sender, RoutedEventArgs e)
        {
            ChangeAccountData changeAccountData = new ChangeAccountData();
            changeAccountData.Show();
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchInfo searchInfo = new SearchInfo();
            searchInfo.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "ehlementy_glavnogo_menyu.htm");
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            ChooseReportPeriod chooseReportPeriod = new ChooseReportPeriod();
            chooseReportPeriod.Show();
            this.Close();
        }
    }
}