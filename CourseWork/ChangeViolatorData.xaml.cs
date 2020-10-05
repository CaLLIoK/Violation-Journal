using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ChangeViolatorData.xaml
    /// </summary>
    public partial class ChangeViolatorData : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";
        DataTable table;

        public ChangeViolatorData()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode";
            connection.Open();
            table = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (IDataReader rdr = cmd.ExecuteReader())
                {
                    table.Load(rdr);
                }
            }
            ViolatorsGrid.ItemsSource = table.DefaultView;
            connection.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateViolatorData updateViolatorData = new UpdateViolatorData();
            updateViolatorData.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
            mainMenuEmployee.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "izmenenie_dannykh_narushitelej.htm");
        }
    }
}