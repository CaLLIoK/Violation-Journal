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
using MessageBox = System.Windows.MessageBox;

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для UpdateViolationsData.xaml
    /// </summary>
    public partial class UpdateViolationsData : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public UpdateViolationsData()
        {
            InitializeComponent();
            List<int> codeList = new List<int>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"SELECT ViolationCode FROM Violation ORDER BY ViolationCode";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    codeList.Add(Convert.ToInt32(dataReader[0].ToString()));
                    searchCriterion.ItemsSource = codeList;
                }
            }
            connection.Close();
        }
        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if (criterion.Text != string.Empty && searchCriterion.Text != string.Empty)
            {
                if (criterion.Text == "Название нарушения")
                {
                    if (changingCriterion.Text != CheckViolation.CheckViolationName(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolation.CheckViolationName(changingCriterion.Text));
                        return;
                    }
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    string findReplays = "SELECT ViolationCode FROM Violation WHERE ViolationName = '" + changingCriterion.Text + "'";
                    SqlCommand sqlCommand3 = new SqlCommand(findReplays, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        MessageBox.Show("Данное нарушение уже имеется в базе. Повторно добавить его нельзя.");
                        return;
                    }
                    else
                    {
                        using (SqlConnection updateName = new SqlConnection(connectionString))
                        using (SqlCommand lastCommnd = updateName.CreateCommand())
                        {
                            lastCommnd.CommandText = "UPDATE Violation SET ViolationName = @name WHERE ViolationCode = @code";

                            lastCommnd.Parameters.AddWithValue("@name", changingCriterion.Text);
                            lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                            updateName.Open();
                            lastCommnd.ExecuteNonQuery();
                            updateName.Close();
                        }
                    }
                    sqlConnection.Close();
                }
                else if (criterion.Text == "Стоимость нарушения")
                {
                    if (changingCriterion.Text != CheckViolation.CheckViolationCost(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolation.CheckViolationCost(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updateCost = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateCost.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violation SET ViolationCost = @cost WHERE ViolationCode = @code";

                        lastCommnd.Parameters.AddWithValue("@cost", Convert.ToDouble(changingCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateCost.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateCost.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали данные для изменения.");
                return;
            }
            MessageBoxResult mboxResult = MessageBox.Show("Данные обновлены. Желаете изменить что-нибудь еще?", "Предупреждение", MessageBoxButton.YesNo);
            if (mboxResult == MessageBoxResult.No)
            {
                ChangeViolationsData changeViolationsData = new ChangeViolationsData();
                changeViolationsData.Show();
                this.Close();
            }
        }

        private void BackToWindow(object sender, RoutedEventArgs e)
        {
            ChangeViolationsData changeViolationsData = new ChangeViolationsData();
            changeViolationsData.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "obnovlenie_dannykh_narushenij.htm");
        }
    }
}