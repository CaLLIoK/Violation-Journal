using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для ViolatorsCarsSearchResult.xaml
    /// </summary>
    public partial class ViolatorsCarsSearchResult : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";
        DataTable table;

        public ViolatorsCarsSearchResult()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string criterion = string.Empty;
            StreamReader readCriterion = new StreamReader("Criterion.txt");
            criterion = readCriterion.ReadLine();
            readCriterion.Close();

            string searchCriterion = string.Empty;
            StreamReader readSearchCriterion = new StreamReader("SearchCriterion.txt");
            searchCriterion = readSearchCriterion.ReadLine();
            readSearchCriterion.Close();
            if (criterion == "Модель автомобиля")
            {
                string query = "SELECT CarCode, CarModelName, ColorName, CarStatetNumber, ViolatorCar.ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic "
                        + " FROM ViolatorCar, CarModel, Color, Violator WHERE ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND CarModel.CarModelName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsCarsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Цвет автомобиля")
            {
                string query = "SELECT CarCode, CarModelName, ColorName, CarStatetNumber, ViolatorCar.ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic "
                        + " FROM ViolatorCar, CarModel, Color, Violator WHERE ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND Color.ColorName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsCarsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Номер автомобиля")
            {
                string query = "SELECT CarCode, CarModelName, ColorName, CarStatetNumber, ViolatorCar.ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic "
                        + " FROM ViolatorCar, CarModel, Color, Violator WHERE ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolatorCar.CarStatetNumber = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsCarsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Фамилия владельца")
            {
                string query = "SELECT CarCode, CarModelName, ColorName, CarStatetNumber, ViolatorCar.ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic "
                        + " FROM ViolatorCar, CarModel, Color, Violator WHERE ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND Violator.ViolatorSurname = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsCarsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Имя владельца")
            {
                string query = "SELECT CarCode, CarModelName, ColorName, CarStatetNumber, ViolatorCar.ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic "
                        + " FROM ViolatorCar, CarModel, Color, Violator WHERE ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND Violator.ViolatorName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsCarsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Отчество владельца")
            {
                string query = "SELECT CarCode, CarModelName, ColorName, CarStatetNumber, ViolatorCar.ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic "
                        + " FROM ViolatorCar, CarModel, Color, Violator WHERE ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND Violator.ViolatorPatronymic = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsCarsGrid.ItemsSource = table.DefaultView;
            }
            connection.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchViolatorsCars searchViolatorsCars = new SearchViolatorsCars();
            searchViolatorsCars.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}