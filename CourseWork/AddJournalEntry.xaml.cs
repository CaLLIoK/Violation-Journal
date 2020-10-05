using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для AddJournalEntry.xaml
    /// </summary>
    public partial class AddJournalEntry : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public AddJournalEntry()
        {
            InitializeComponent();
            string violations = string.Empty;
            List<int> violationCodeList = new List<int>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"SELECT ViolationCode, ViolationName, ViolationCost FROM Violation ORDER BY ViolationName";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    violations = dataReader[0].ToString() + " - " + dataReader[1].ToString() + " - " + dataReader[2].ToString() + " руб.";
                    violationsList.Items.Add(violations);
                    violationCodeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                    var newList = from i in violationCodeList orderby i select i;
                    violationCode.ItemsSource = newList;
                }
            }
            dataReader.Close();

            string carData = string.Empty;
            List<int> codeList = new List<int>();
            string query1 = @"SELECT CarCode, ColorName, CarModelName, CarStatetNumber, ViolatorSurname, ViolatorName, ViolatorPatronymic FROM ViolatorCar, Violator, CarModel, Color WHERE ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode ORDER BY CarStatetNumber";
            SqlCommand sqlCommand1 = new SqlCommand(query1, connection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            if (dataReader1.HasRows)
            {
                while (dataReader1.Read())
                {
                    carData = dataReader1[0].ToString() + " - " + dataReader1[1].ToString() + " " + dataReader1[2].ToString() + " - [" + dataReader1[3].ToString() + "] - " + dataReader1[4].ToString() + " " + dataReader1[5].ToString() + " " + dataReader1[6].ToString();
                    violatorCarsList.Items.Add(carData);
                }
            }
            dataReader1.Close();
            connection.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
            mainMenuEmployee.Show();
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (violationCode.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("Вы не выбрали код нарушения");
                return;
            }
            if (entryDate.Text != CheckJournalEntry.CheckEnrtyDate(entryDate.Text))
            {
                System.Windows.MessageBox.Show(CheckJournalEntry.CheckEnrtyDate(entryDate.Text));
                return;
            }
            if (entryTime.Text != CheckJournalEntry.CheckEntryTime(entryTime.Text))
            {
                System.Windows.MessageBox.Show(CheckJournalEntry.CheckEntryTime(entryTime.Text));
                return;
            }        
            if (carStateNumber.Text != CheckCar.CheckCarStateNumber(carStateNumber.Text))
            {
                System.Windows.MessageBox.Show(CheckCar.CheckCarStateNumber(carStateNumber.Text));
                return;
            }
            int carCode = 0;
            string query = @"SELECT CarCode FROM ViolatorCar WHERE CarStatetNumber = '" + carStateNumber.Text +"'";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    carCode = Convert.ToInt32(dataReader[0].ToString());
                }
            }
            else
            {
                MessageBoxResult meboxResult = System.Windows.MessageBox.Show("Машины с таким номером нет в базе. Можете добавить добавить её.", "Предупреждение", MessageBoxButton.YesNo);
                if (meboxResult == MessageBoxResult.Yes)
                {
                    AddViolatorCar addViolatorCar = new AddViolatorCar();
                    addViolatorCar.Show();
                    this.Close();
                }
                return;
            }
            dataReader.Close();
            sqlConnection.Close();
            int inspectorCode = 0;
            string login = string.Empty;
            StreamReader streamReader = new StreamReader("UserLogin.txt");
            login = streamReader.ReadLine();
            streamReader.Close();
            string query1 = @"SELECT UserCode FROM Users WHERE UserLogin = '" + login + "'";
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            sqlConnection1.Open();
            SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection1);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            if (dataReader1.HasRows)
            {
                while (dataReader1.Read())
                {
                    inspectorCode = Convert.ToInt32(dataReader1[0].ToString());
                }
            }
            dataReader1.Close();
            sqlConnection1.Close();

            int statusCode = 0;
            string query2 = @"SELECT ViolationStatusCode FROM ViolationStatus WHERE ViolationStatusName = 'Не оплачено'";
            SqlConnection sqlConnection2 = new SqlConnection(connectionString);
            sqlConnection2.Open();
            SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection2);
            SqlDataReader dataReader2 = sqlCommand2.ExecuteReader();
            if (dataReader2.HasRows)
            {
                while (dataReader2.Read())
                {
                    statusCode = Convert.ToInt32(dataReader2[0].ToString());
                }
            }
            dataReader2.Close();
            sqlConnection2.Close();
            string findReplays = "SELECT EntryNumber FROM ViolationsJournal WHERE EntryNumberDate = '" + Convert.ToDateTime(entryDate.Text) + "' AND EntryNumberTime = '" + TimeSpan.Parse(entryTime.Text) + "' AND UserCode = " + inspectorCode + " AND ViolationCode = " + Convert.ToInt32(violationCode.Text) + " AND CarCode = " + carCode + " AND ViolationStatusCode = " + statusCode + "";
            SqlCommand sqlCommand3 = new SqlCommand(findReplays, connection);
            SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                System.Windows.MessageBox.Show("Такая запись уже есть в журнале.");
                return;
            }
            else
            {
                using (SqlConnection journalEntry = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = journalEntry.CreateCommand())
                {
                    lastCommnd.CommandText = "INSERT INTO ViolationsJournal (EntryNumberDate, EntryNumberTime, UserCode, ViolationCode, CarCode, ViolationStatusCode) VALUES (@date, @time, @uc, @vc, @cc, @status)";

                    lastCommnd.Parameters.AddWithValue("@date", Convert.ToDateTime(entryDate.Text));
                    lastCommnd.Parameters.AddWithValue("@time", TimeSpan.Parse(entryTime.Text));
                    lastCommnd.Parameters.AddWithValue("@uc", inspectorCode);
                    lastCommnd.Parameters.AddWithValue("@vc", Convert.ToInt32(violationCode.Text));
                    lastCommnd.Parameters.AddWithValue("@cc", carCode);
                    lastCommnd.Parameters.AddWithValue("@status", statusCode);

                    journalEntry.Open();
                    lastCommnd.ExecuteNonQuery();
                    journalEntry.Close();
                }
                System.Windows.MessageBox.Show("Запись добавлена в журнал.");
            }
            sqlDataReader.Close();
            connection.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "dobavlenie_zapisi_v_zhurnal_ucheta_narushenij_pdd.htm");
        }
    }
}