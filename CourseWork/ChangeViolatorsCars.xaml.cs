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
    /// Логика взаимодействия для ChangeViolarorsCars.xaml
    /// </summary>
    public partial class ChangeViolatorsCars : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";
        DataTable table;

        public ChangeViolatorsCars()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT CarCode, CarModelName, ColorName, CarStatetNumber, ViolatorCar.ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic "
                         + " FROM ViolatorCar, CarModel, Color, Violator WHERE ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode";
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
            connection.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCarsData updateCarsData = new UpdateCarsData();
            updateCarsData.Show();
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
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "izmenenie_dannykh_avtomobilej.htm");
        }
    }
}
