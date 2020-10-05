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
    /// Логика взаимодействия для DeleteViolator.xaml
    /// </summary>
    public partial class DeleteViolator : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public DeleteViolator()
        {
            InitializeComponent();
            string violator = string.Empty;
            ObservableCollection<int> codeList = new ObservableCollection<int>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"SELECT ViolatorCode, ViolatorSurname, ViolatorName, ViolatorPatronymic FROM Violator ORDER BY ViolatorSurname";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    violator = dataReader[0].ToString() + " - " + dataReader[1].ToString() + " " + dataReader[2].ToString() + " " + dataReader[3].ToString();
                    violatorsList.Items.Add(violator);
                    codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                    var newList = from i in codeList orderby i select i;
                    deleteViolator.ItemsSource = newList;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (deleteViolator.Text != string.Empty)
            {
                using (SqlConnection deleteRow = new SqlConnection(connectionString))
                using (SqlCommand lastCommnd = deleteRow.CreateCommand())
                {
                    lastCommnd.CommandText = "DELETE FROM Violator WHERE ViolatorCode = @number";

                    lastCommnd.Parameters.AddWithValue("@number", deleteViolator.Text);

                    deleteRow.Open();
                    lastCommnd.ExecuteNonQuery();
                    deleteRow.Close();
                }
                System.Windows.MessageBox.Show("Нарушитель удалён.");
                deleteViolator.SelectedIndex = -1;
                violatorsList.Items.Clear();
                string violator = string.Empty;
                ObservableCollection<int> codeList = new ObservableCollection<int>();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = @"SELECT ViolatorCode, ViolatorSurname, ViolatorName, ViolatorPatronymic FROM Violator ORDER BY ViolatorSurname";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        violator = dataReader[0].ToString() + " - " + dataReader[1].ToString() + " " + dataReader[2].ToString() + " " + dataReader[3].ToString();
                        violatorsList.Items.Add(violator);
                        codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                        var newList = from i in codeList orderby i select i;
                        deleteViolator.ItemsSource = newList;
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Вы не указали код нарушитея, которого собираетесь удалить.");
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
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "udalenie_narushitelya.htm");
        }
    }
}