using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для DeleteJournalEntry.xaml
    /// </summary>
    public partial class ChangeJournalEntry : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";
        DataTable table;

        public ChangeJournalEntry()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode";
            connection.Open();
            table = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (IDataReader rdr = cmd.ExecuteReader())
                {
                    table.Load(rdr);
                }
            }
            ViolatorsCarsGrid.ItemsSource = table.DefaultView;

            ObservableCollection<int> codeList = new ObservableCollection<int>();
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                    journalEntry.ItemsSource = codeList;
                }
            }
            connection.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateJournalEntry updateJournalEntry = new UpdateJournalEntry();
            updateJournalEntry.Show();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (journalEntry.Text != string.Empty)
            {
                using (SqlConnection deleteRow = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = deleteRow.CreateCommand())
                {
                    lastCommnd.CommandText = "DELETE FROM ViolationsJournal WHERE EntryNumber = @code";

                    lastCommnd.Parameters.AddWithValue("@code", journalEntry.Text);

                    deleteRow.Open();
                    lastCommnd.ExecuteNonQuery();
                    deleteRow.Close();
                }
                System.Windows.MessageBox.Show("Запись удалена.");
                journalEntry.SelectedIndex = -1;
                SqlConnection connection = new SqlConnection(connectionString);
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                             + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                             + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode";
                connection.Open();
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsCarsGrid.ItemsSource = table.DefaultView;

                ObservableCollection<int> codeList = new ObservableCollection<int>();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                        journalEntry.ItemsSource = codeList;
                    }
                }
                connection.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Вы не указали код записи, которую собираетесь удалить.");
                return;
            }
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
            mainMenuEmployee.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "izmenenie_dannykh_zhurnala_ucheta_narushenij_pdd.htm");
        }
    }
}