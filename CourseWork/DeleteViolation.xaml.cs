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
    /// Логика взаимодействия для DeleteViolation.xaml
    /// </summary>
    public partial class DeleteViolation : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public DeleteViolation()
        {
            InitializeComponent();
            string violations = string.Empty;
            ObservableCollection<int> codeList = new ObservableCollection<int>();
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
                    codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                    var newList = from i in codeList orderby i select i;
                    deleteViolation.ItemsSource = newList;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (deleteViolation.Text != string.Empty)
            {
                using (SqlConnection deleteRow = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = deleteRow.CreateCommand())
                {
                    lastCommnd.CommandText = "DELETE FROM Violation WHERE ViolationCode = @code";

                    lastCommnd.Parameters.AddWithValue("@code", deleteViolation.Text);

                    deleteRow.Open();
                    lastCommnd.ExecuteNonQuery();
                    deleteRow.Close();
                }
                System.Windows.MessageBox.Show("Автонарушение удалено.");
                deleteViolation.SelectedIndex = -1;
                violationsList.Items.Clear();
                string violations = string.Empty;
                ObservableCollection<int> codeList = new ObservableCollection<int>();
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
                        codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                        var newList = from i in codeList orderby i select i;
                        deleteViolation.ItemsSource = newList;
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Вы не указали код автонарушения, которое собираетесь удалить.");
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
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "udalenie_narusheniya.htm");
        }
    }
}