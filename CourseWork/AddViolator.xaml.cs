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
    /// Логика взаимодействия для AddViolator.xaml
    /// </summary>
    public partial class AddViolator : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public AddViolator()
        {
            InitializeComponent();
            string town = string.Empty;
            string street = string.Empty;
            List<string> townCodeList = new List<string>();
            List<string> streetCodeList = new List<string>();
            SqlConnection connection = new SqlConnection(connectionString);
            string selectTownInfo = "SELECT TownCode, TownName FROM Town ORDER BY TownName";
            connection.Open();
            SqlCommand cmd = new SqlCommand(selectTownInfo, connection);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    town = read[0].ToString() + " - город " + read[1].ToString();
                    townList.Items.Add(town);
                    townCodeList.Add(read[1].ToString());
                    var newList = from i in townCodeList orderby i select i;
                    violatorTown.ItemsSource = newList;
                }
            }
            read.Close();
            string selectStreetInfo = "SELECT StreetCode, StreetName FROM Street ORDER BY StreetName";
            SqlCommand command = new SqlCommand(selectStreetInfo, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    street = reader[0].ToString() + " - улица " + reader[1].ToString();
                    streetList.Items.Add(street);
                    streetCodeList.Add(reader[1].ToString());
                    var newList = from i in streetCodeList orderby i select i;
                    violatorStreet.ItemsSource = newList;
                }
            }
            reader.Close();
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
            if (violatorName.Text != CheckViolator.CheckViolatorName(violatorName.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorName(violatorName.Text));
                return;
            }
            if (violatorSurname.Text != CheckViolator.CheckViolatorSurname(violatorSurname.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorSurname(violatorSurname.Text));
                return;
            }
            if (violatorPatronymic.Text != CheckViolator.CheckViolatorPatronymic(violatorPatronymic.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPatronymic(violatorPatronymic.Text));
                return;
            }
            if (violatorNumber.Text != CheckViolator.CheckViolatorPhoneNumber(violatorNumber.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPhoneNumber(violatorNumber.Text));
                return;
            }
            if (violatorPasport.Text != CheckViolator.CheckViolatorPasportNumber(violatorPasport.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPasportNumber(violatorPasport.Text));
                return;
            }
            if (violatorTown.Text != CheckViolator.CheckViolatorTown(violatorTown.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorTown(violatorTown.Text));
                return;
            }
            if (violatorStreet.Text != CheckViolator.CheckViolatorStreet(violatorStreet.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorStreet(violatorStreet.Text));
                return;
            }
            if (violatorHouse.Text != CheckViolator.CheckViolatorHouseNumber(violatorHouse.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorHouseNumber(violatorHouse.Text));
                return;
            }
            if (violatorApartment.Text != CheckViolator.CheckViolatorApartment(violatorApartment.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorApartment(violatorApartment.Text));
                return;
            }
            string findReplays = "SELECT ViolatorCode FROM Violator WHERE (ViolatorSurname = '" + violatorSurname.Text + "' AND ViolatorName = '" + violatorName.Text + "' AND ViolatorPatronymic = '" + violatorPatronymic.Text + "' AND ViolatorPasportNumber = '" + violatorPasport.Text + "'"
                + " AND ViolatorPhoneNumber = '" + violatorNumber.Text + "' AND ViolatorTownCode = (SELECT TownCode FROM Town WHERE TownName = '" + violatorTown.Text + "') AND ViolatorStreetCode = (SELECT StreetCode FROM Street WHERE StreetName = '" + violatorStreet.Text + "') AND ViolatorHouseNumber = " + Convert.ToInt32(violatorHouse.Text) + " AND ViolatorApartmentNumber = " + Convert.ToInt32(violatorApartment.Text) + ")"
                + " OR ViolatorPhoneNumber = '" + violatorNumber.Text + "' OR ViolatorPasportNumber = '" + violatorPasport.Text + "'";
            SqlCommand sqlCommand3 = new SqlCommand(findReplays, connection);
            SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                System.Windows.MessageBox.Show("Этот нарушитель уже есть в базе, либо вы ввели номер паспорта/телефона, уже привязанные к другому нарушителю.");
                return;
            }
            else
            {
                using (SqlConnection journalEntry = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = journalEntry.CreateCommand())
                {
                    lastCommnd.CommandText = "INSERT INTO Violator (ViolatorPasportNumber, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPhoneNumber, ViolatorTownCode, ViolatorStreetCode, ViolatorHouseNumber, ViolatorApartmentNumber)" +
                                           " VALUES (@pasport, @name, @surname, @patronymic, @phone, (SELECT TownCode FROM Town WHERE TownName = @town), (SELECT StreetCode FROM Street WHERE StreetName = @street), @house, @apartment)";

                    lastCommnd.Parameters.AddWithValue("@pasport", violatorPasport.Text);
                    lastCommnd.Parameters.AddWithValue("@surname", violatorSurname.Text);
                    lastCommnd.Parameters.AddWithValue("@name", violatorName.Text);
                    lastCommnd.Parameters.AddWithValue("@patronymic", violatorPatronymic.Text);
                    lastCommnd.Parameters.AddWithValue("@phone", violatorNumber.Text);
                    lastCommnd.Parameters.AddWithValue("@town", violatorTown.Text);
                    lastCommnd.Parameters.AddWithValue("@street", violatorStreet.Text);
                    lastCommnd.Parameters.AddWithValue("@house", Convert.ToInt32(violatorHouse.Text));
                    lastCommnd.Parameters.AddWithValue("@apartment", Convert.ToInt32(violatorApartment.Text));
                    journalEntry.Open();
                    lastCommnd.ExecuteNonQuery();
                    journalEntry.Close();
                }
                System.Windows.MessageBox.Show("Нарушитель добавлен.");
            }
            sqlDataReader.Close();
            connection.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "dobavleniya_narushitelya.htm");
        }
    }
}