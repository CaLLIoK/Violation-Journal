using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для DeleteViolatorCar.xaml
    /// </summary>
    public partial class DeleteViolatorCar : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public DeleteViolatorCar()
        {
            InitializeComponent();
            string carData = string.Empty;
            ObservableCollection<int> codeList = new ObservableCollection<int>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"SELECT CarCode, ColorName, CarModelName, CarStatetNumber, ViolatorSurname, ViolatorName, ViolatorPatronymic FROM ViolatorCar, Violator, CarModel, Color WHERE ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode ORDER BY CarStatetNumber";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    carData = dataReader[0].ToString() + " - " + dataReader[1].ToString() + " " + dataReader[2].ToString() + " - [" + dataReader[3].ToString() + "] - " + dataReader[4].ToString() + " " + dataReader[5].ToString() + " " + dataReader[6].ToString();
                    violatorCarsList.Items.Add(carData);
                    codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                    var newList = from i in codeList orderby i select i;
                    deleteViolatorCar.ItemsSource = newList;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (deleteViolatorCar.Text != string.Empty)
            {
                using (SqlConnection deleteRow = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = deleteRow.CreateCommand())
                {
                    lastCommnd.CommandText = "DELETE FROM ViolatorCar WHERE CarCode = @number";

                    lastCommnd.Parameters.AddWithValue("@number", deleteViolatorCar.Text);

                    deleteRow.Open();
                    lastCommnd.ExecuteNonQuery();
                    deleteRow.Close();
                }
                System.Windows.MessageBox.Show("Автомобиль удалён.");
                deleteViolatorCar.SelectedIndex = -1;
                violatorCarsList.Items.Clear();
                string carData = string.Empty;
                ObservableCollection<int> codeList = new ObservableCollection<int>();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = @"SELECT CarCode, ColorName, CarModelName, CarStatetNumber, ViolatorSurname, ViolatorName, ViolatorPatronymic FROM ViolatorCar, Violator, CarModel, Color WHERE ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode ORDER BY CarStatetNumber";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        carData = dataReader[0].ToString() + " - " + dataReader[1].ToString() + " " + dataReader[2].ToString() + " - [" + dataReader[3].ToString() + "] - " + dataReader[4].ToString() + " " + dataReader[5].ToString() + " " + dataReader[6].ToString();
                        violatorCarsList.Items.Add(carData);
                        codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                        var newList = from i in codeList orderby i select i;
                        deleteViolatorCar.ItemsSource = newList;
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Вы не указали код автомобиля, который собираетесь удалить.");
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
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "udalenie_avtomobilya.htm");
        }
    }
}
