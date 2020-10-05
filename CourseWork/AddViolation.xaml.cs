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
    /// Логика взаимодействия для AddViolation.xaml
    /// </summary>
    public partial class AddViolation : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public AddViolation()
        {
            InitializeComponent();
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
            if (violationName.Text != CheckViolation.CheckViolationName(violationName.Text))
            {
                System.Windows.MessageBox.Show(CheckViolation.CheckViolationName(violationName.Text));
                return;
            }
            if (violationCost.Text != CheckViolation.CheckViolationCost(violationCost.Text))
            {
                System.Windows.MessageBox.Show(CheckViolation.CheckViolationCost(violationCost.Text));
                return;
            }
            string findReplays = "SELECT ViolationCode FROM Violation WHERE (ViolationName =  '" + violationName.Text + "' AND ViolationCost = '" + Convert.ToDouble(violationCost.Text) + "') OR ViolationName =  '" + violationName.Text + "'";
            SqlCommand sqlCommand3 = new SqlCommand(findReplays, connection);
            SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                System.Windows.MessageBox.Show("Этот нарушение уже есть в базе.");
                return;
            }
            else
            {
                using (SqlConnection changeSum = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = changeSum.CreateCommand())
                {
                    lastCommnd.CommandText = "INSERT INTO Violation(ViolationName, ViolationCost) VALUES (@name, @cost)";

                    lastCommnd.Parameters.AddWithValue("@name", violationName.Text);
                    lastCommnd.Parameters.AddWithValue("@cost", Convert.ToDouble(violationCost.Text));

                    changeSum.Open();
                    lastCommnd.ExecuteNonQuery();
                    changeSum.Close();
                }
                System.Windows.MessageBox.Show("Автонарушение добавлено.");
                connection.Close();
            }
            sqlDataReader.Close();
            connection.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "dobavlenie_narusheniya.htm");
        }
    }
}