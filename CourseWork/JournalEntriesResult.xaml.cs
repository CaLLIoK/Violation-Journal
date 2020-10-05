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
    /// Логика взаимодействия для JournalEntriesResult.xaml
    /// </summary>
    public partial class JournalEntriesResult : Window
    {
        string connectionString = @"Data Source=(local)\SQLEXPRESS; Initial Catalog=Journal; Integrated Security=True";
        DataTable table;

        public JournalEntriesResult()
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

            if (criterion == "Дата нарушения")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND ViolationsJournal.EntryNumberDate = '" + Convert.ToDateTime(searchCriterion) + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Время нарушения")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND ViolationsJournal.EntryNumberTime = '" + TimeSpan.Parse(searchCriterion) + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Тип нарушения")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND Violation.ViolationName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Номер автомобиля")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND ViolatorCar.CarStatetNumber = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Марка автомобиля")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND CarModel.CarModelName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Фамилия нарушителя")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND Violator.ViolatorSurname = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Имя нарушителя")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND Violator.ViolatorName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Отчество нарушителя")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND Violator.ViolatorPatronymic = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Сумма штрафа (руб.)")
            {
                table = new DataTable();
                using (SqlCommand lastCommnd = connection.CreateCommand())
                {
                    lastCommnd.CommandText = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND Violation.ViolationCost = @cost"; ;

                    lastCommnd.Parameters.AddWithValue("@cost", Convert.ToDouble(searchCriterion));

                    using (IDataReader rdr = lastCommnd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Статус оплаты")
            {
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND ViolationStatus.ViolationStatusName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                JournalEntriesGrid.ItemsSource = table.DefaultView;
            }
            connection.Close();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            SearchJournalEntries searchJournalEntries = new SearchJournalEntries();
            searchJournalEntries.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();
    }
}