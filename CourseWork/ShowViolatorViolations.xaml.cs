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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для ShowViolatorViolations.xaml
    /// </summary>
    public partial class ShowViolatorViolations : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";
        DataTable table;

        public ShowViolatorViolations()
        {
            InitializeComponent();
            string login = string.Empty;
            StreamReader streamReader = new StreamReader("UserLogin.txt");
            login = streamReader.ReadLine();
            streamReader.Close();
            string mySelectQuery = "SELECT * FROM Users WHERE [UserLogin] = '" + login + "'";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(mySelectQuery, connectionString))
            {
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    StreamReader pasportStreamReader = new StreamReader("ViolatorPasportNumber.txt");
                    string pasport = pasportStreamReader.ReadLine();
                    pasportStreamReader.Close();
                    SqlConnection connection = new SqlConnection(connectionString);
                    string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                                 + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                                 + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND Violator.ViolatorPasportNumber = '" + pasport + "' AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND ViolationStatus.ViolationStatusName = 'Не оплачено'";
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

                    string sum = string.Empty;
                    string query1 = @"SELECT ViolatorPasportNumber, Sum (ViolationCost) FROM ViolationsJournal, ViolatorCar, ViolationStatus, Violator, Violation" +
                                      " WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode" +
                                      " AND Violator.ViolatorPasportNumber = '" + pasport + "' AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode" +
                                      " AND ViolationStatus.ViolationStatusName = 'Не оплачено' GROUP BY ViolatorPasportNumber";
                    SqlCommand sqlCommand = new SqlCommand(query1, connection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            sum = "Общая сумма штрафов: " + dataReader[1].ToString() + " руб.";
                            generalSum.Items.Add(sum);
                        }
                    }
                    else
                    {
                        sum = "Общая сумма штрафов: 0 руб.";
                        generalSum.Items.Add(sum);
                    }
                    dataReader.Close();
                }
                else if (table.Rows.Count == 0)
                {
                    StreamReader pasportStreamReader = new StreamReader("ViolatorPasportNumber.txt");
                    string pasport = pasportStreamReader.ReadLine();
                    pasportStreamReader.Close();

                    StreamReader nameStreamReader = new StreamReader("ViolatorName.txt");
                    string name = nameStreamReader.ReadLine();
                    nameStreamReader.Close();

                    StreamReader surnameStreamReader = new StreamReader("ViolatorSurname.txt");
                    string surname = surnameStreamReader.ReadLine();
                    surnameStreamReader.Close();

                    StreamReader patronymicStreamReader = new StreamReader("ViolatorPatronymic.txt");
                    string patronymic = patronymicStreamReader.ReadLine();
                    patronymicStreamReader.Close();

                    SqlConnection connection = new SqlConnection(connectionString);
                    string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                                 + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                                 + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND Violator.ViolatorPasportNumber = '" + pasport + "' AND Violator.ViolatorSurname = '" + surname + "' AND Violator.ViolatorName = '" + name + "' AND Violator.ViolatorPatronymic = '" + patronymic + "' AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND ViolationStatus.ViolationStatusName = 'Не оплачено'";
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

                    string sum = string.Empty;
                    string query1 = @"SELECT ViolatorPasportNumber, Sum (ViolationCost) FROM ViolationsJournal, ViolatorCar, ViolationStatus, Violator, Violation" +
                                      " WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode" +
                                      " AND Violator.ViolatorPasportNumber = '" + pasport + "' AND Violator.ViolatorSurname = '" + surname + "' AND  Violator.ViolatorName = '" + name + "' AND Violator.ViolatorPatronymic = '" + patronymic + "' AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode" +
                                      " AND ViolationStatus.ViolationStatusName = 'Не оплачено' GROUP BY ViolatorPasportNumber";
                    SqlCommand sqlCommand = new SqlCommand(query1, connection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            sum = "Общая сумма штрафов: " + dataReader[1].ToString() + " руб.";
                            generalSum.Items.Add(sum);
                        }
                    }
                    else
                    {
                        sum = "Общая сумма штрафов: 0 руб.";
                        generalSum.Items.Add(sum);
                    }
                    dataReader.Close();
                }
            }         
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            string login = string.Empty;
            StreamReader streamReader = new StreamReader("UserLogin.txt");
            login = streamReader.ReadLine();
            streamReader.Close();
            string mySelectQuery = "SELECT * FROM Users WHERE [UserLogin] = '" + login + "'";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(mySelectQuery, connectionString))
            {
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
                    mainMenuEmployee.Show();
                    this.Close();
                }
                else if (table.Rows.Count == 0)
                {
                    MainMenuGuest mainMenuGuest = new MainMenuGuest();
                    mainMenuGuest.Show();
                    this.Close();
                }
            }
        }
    }
}