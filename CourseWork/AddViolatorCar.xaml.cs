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
    /// Логика взаимодействия для AddViolatorCar.xaml
    /// </summary>
    public partial class AddViolatorCar : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public AddViolatorCar()
        {
            InitializeComponent();
            string color = string.Empty;
            string violator = string.Empty;
            List<string> colorList = new List<string>();
            List<string> carModelList = new List<string>();
            List<int> violatorCodeList = new List<int>();
            SqlConnection connection = new SqlConnection(connectionString);
            string selectModelInfo = "SELECT CarModelName FROM CarModel ORDER BY CarModelName";
            connection.Open();
            SqlCommand cmd = new SqlCommand(selectModelInfo, connection);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    carModelList.Add(read[0].ToString());
                    //var newList = from i in carModelList orderby i select i; 
                    carModel.ItemsSource = carModelList;//newList;
                }
            }
            read.Close();
            string selectStreetInfo = "SELECT ColorName FROM Color ORDER BY ColorName";
            SqlCommand command = new SqlCommand(selectStreetInfo, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {;
                    colorList.Add(reader[0].ToString());
                   // var newList = from i in colorList orderby i select i;
                    carColor.ItemsSource = colorList;
                }
            }
            reader.Close();
            string query = @"SELECT ViolatorCode, ViolatorSurname, ViolatorName, ViolatorPatronymic FROM Violator ORDER BY ViolatorSurname";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    violator = dataReader[0].ToString() + " - " + dataReader[1].ToString() + " " + dataReader[2].ToString() + " " + dataReader[3].ToString();
                    violatorsList.Items.Add(violator);
                    violatorCodeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                    var newList = from i in violatorCodeList orderby i select i;
                    carOwner.ItemsSource = newList;
                }
            }
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
            if (carModel.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("Вы не выбрали марку автомобиля.");
                return;
            }
            if (carColor.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("Вы не выбрали цвет автомобиля.");
                return;
            }
            if (carOwner.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("Вы не выбрали код владельца автомобиля.");
                return;
            }
            if (carNumber.Text != CheckCar.CheckCarStateNumber(carNumber.Text))
            {
                System.Windows.MessageBox.Show(CheckCar.CheckCarStateNumber(carNumber.Text));
                return;
            }
            string findReplays = "SELECT CarCode FROM ViolatorCar WHERE (CarModelCode = (SELECT CarModelCode FROM CarModel WHERE CarModelName = '" + carModel.Text + "') AND ColorCode = (SELECT ColorCode FROM Color WHERE ColorName = '" + carColor.Text + "') AND CarStatetNumber = '" + carNumber.Text + "' AND ViolatorCode = " + Convert.ToInt32(carOwner.Text) + ") OR CarStatetNumber = '" + carNumber.Text + "'";
            SqlCommand sqlCommand3 = new SqlCommand(findReplays, connection);
            SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                System.Windows.MessageBox.Show("Этот автомобиль уже есть в базе, либо вы ввели уже занятый номер автомобиля.");
                return;
            }
            else
            {
                using (SqlConnection journalEntry = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = journalEntry.CreateCommand())
                {
                    lastCommnd.CommandText = "INSERT INTO ViolatorCar(CarModelCode, ColorCode, CarStatetNumber, ViolatorCode) VALUES ((SELECT CarModelCode FROM CarModel WHERE CarModelName = @carName), (SELECT ColorCode FROM Color WHERE ColorName = @carColor), @carNumber, @carOwner)";

                    lastCommnd.Parameters.AddWithValue("@carName", carModel.Text);
                    lastCommnd.Parameters.AddWithValue("@carColor", carColor.Text);
                    lastCommnd.Parameters.AddWithValue("@carNumber", carNumber.Text);
                    lastCommnd.Parameters.AddWithValue("@carOwner", Convert.ToInt32(carOwner.Text));
                    journalEntry.Open();
                    lastCommnd.ExecuteNonQuery();
                    journalEntry.Close();
                }
                System.Windows.MessageBox.Show("Автомобиль добавлен.");
            }
            sqlDataReader.Close();
            connection.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "dobavlenie_avtomobilya_narushitelya.htm");
        }
    }
}