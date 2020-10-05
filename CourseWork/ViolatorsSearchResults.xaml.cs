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
    /// Логика взаимодействия для ViolatorsSearchResults.xaml
    /// </summary>
    public partial class ViolatorsSearchResults : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";
        DataTable table;

        public ViolatorsSearchResults()
        {
            InitializeComponent();
            string criterion = string.Empty;
            StreamReader readCriterion = new StreamReader("Criterion.txt");
            criterion = readCriterion.ReadLine();
            readCriterion.Close();

            string searchCriterion = string.Empty;
            StreamReader readSearchCriterion = new StreamReader("SearchCriterion.txt");
            searchCriterion = readSearchCriterion.ReadLine();
            readSearchCriterion.Close();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (criterion == "Фамилия нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Violator.ViolatorSurname = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Имя нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Violator.ViolatorName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Отчество нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Violator.ViolatorPatronymic = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Паспорт нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Violator.ViolatorPasportNumber = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Телефон нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Violator.ViolatorPhoneNumber = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Город нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Town.TownName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Улица нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Street.StreetName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Дом нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Violator.ViolatorHouseNumber = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Квартира нарушителя")
            {
                string query = "SELECT ViolatorCode, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPasportNumber, ViolatorPhoneNumber, TownName, StreetName, ViolatorHouseNumber, ViolatorApartmentNumber FROM Violator, Town, Street WHERE Violator.ViolatorTownCode = Town.TownCode AND Violator.ViolatorStreetCode = Street.StreetCode AND Violator.ViolatorApartmentNumber = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolatorsGrid.ItemsSource = table.DefaultView;
            }
            connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchViolators searchViolators = new SearchViolators();
            searchViolators.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Info_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}