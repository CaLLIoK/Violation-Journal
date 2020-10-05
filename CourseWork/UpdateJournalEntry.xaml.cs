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
    /// Логика взаимодействия для UpdateJournalEntry.xaml
    /// </summary>
    public partial class UpdateJournalEntry : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public UpdateJournalEntry()
        {
            InitializeComponent();
            string violations = string.Empty;
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
                }
            }
            dataReader.Close();

            string carData = string.Empty;
            string query1 = @"SELECT CarCode, ColorName, CarModelName, CarStatetNumber, ViolatorSurname, ViolatorName, ViolatorPatronymic FROM ViolatorCar, Violator, CarModel, Color WHERE ViolatorCar.ColorCode = Color.ColorCode AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode ORDER BY CarStatetNumber";
            SqlCommand sqlCommand1 = new SqlCommand(query1, connection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            if (dataReader1.HasRows)
            {
                while (dataReader1.Read())
                {
                    carData = dataReader1[0].ToString() + " - " + dataReader1[1].ToString() + " " + dataReader1[2].ToString() + " - [" + dataReader1[3].ToString() + "] - " + dataReader1[4].ToString() + " " + dataReader1[5].ToString() + " " + dataReader1[6].ToString();
                    violatorsCarsList.Items.Add(carData);
                }
            }
            dataReader1.Close();

            List<int> codeList = new List<int>();
            string query2 = @"SELECT EntryNumber FROM ViolationsJournal ORDER BY EntryNumber";
            SqlCommand sqlCommand2 = new SqlCommand(query2, connection);
            SqlDataReader dataReader2 = sqlCommand2.ExecuteReader();
            if (dataReader2.HasRows)
            {
                while (dataReader2.Read())
                {
                    codeList.Add(Convert.ToInt32(dataReader2[0].ToString()));
                    searchCriterion.ItemsSource = codeList;
                }
            }
            dataReader2.Close();
            connection.Close();
        }

        private void BackToWindow(object sender, RoutedEventArgs e)
        {
            ChangeJournalEntry changeJournalEntry = new ChangeJournalEntry();
            changeJournalEntry.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if (criterion.Text != string.Empty && searchCriterion.Text != string.Empty)
            {
                if (criterion.Text == "Дата нарушения")
                {
                    if (changingCriterion.Text != CheckJournalEntry.CheckEnrtyDate(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckJournalEntry.CheckEnrtyDate(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updateDate = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateDate.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolationsJournal SET EntryNumberDate = @date WHERE EntryNumber = @code";

                        lastCommnd.Parameters.AddWithValue("@date", Convert.ToDateTime(changingCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateDate.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateDate.Close();
                    }
                }
                else if (criterion.Text == "Время нарушения")
                {
                    if (changingCriterion.Text != CheckJournalEntry.CheckEntryTime(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckJournalEntry.CheckEntryTime(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updateTime = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateTime.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolationsJournal SET EntryNumberTime = @time WHERE EntryNumber = @code";

                        lastCommnd.Parameters.AddWithValue("@time", TimeSpan.Parse(changingCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateTime.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateTime.Close();
                    }
                }
                else if (criterion.Text == "Тип нарушения")
                {
                    if (changingCriterion.Text == string.Empty)
                    {
                        MessageBox.Show("Вы не ввели код нарушения.");
                        return;
                    }
                    else
                    {
                        char[] codeArray = changingCriterion.Text.ToCharArray();
                        for (int i = 0; i < codeArray.Length; i++)
                        {
                            if (!char.IsDigit(codeArray[i]))
                            {
                                MessageBox.Show("Вы указали в коде нарушения недопустимые символы.");
                                return;
                            }
                        }
                    }
                    string query = @"SELECT ViolationCode FROM Violation WHERE ViolationCode = '" + changingCriterion.Text + "'";
                    int violationCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            violationCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого нарушения нет в базе.");
                        return;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    using (SqlConnection updateViolationType = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateViolationType.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolationsJournal SET ViolationCode = @viol WHERE EntryNumber = @code";

                        lastCommnd.Parameters.AddWithValue("@viol", Convert.ToInt32(changingCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateViolationType.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateViolationType.Close();
                    }
                }
                else if (criterion.Text == "Автомобиль и владелец")
                {
                    if (changingCriterion.Text == string.Empty)
                    {
                        MessageBox.Show("Вы не ввели код автомобиля.");
                        return;
                    }
                    else
                    {
                        char[] codeArray = changingCriterion.Text.ToCharArray();
                        for (int i = 0; i < codeArray.Length; i++)
                        {
                            if (!char.IsDigit(codeArray[i]))
                            {
                                MessageBox.Show("Вы указали в коде автомобиля недопустимые символы.");
                                return;
                            }
                        }
                    }
                    string query = @"SELECT CarCode FROM ViolatorCar WHERE CarCode = '" + changingCriterion.Text + "'";
                    int violationCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            violationCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого автомобиля нет в базе.");
                        return;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    using (SqlConnection updateCarAndViolator = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateCarAndViolator.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolationsJournal SET CarCode = @cc WHERE EntryNumber = @code";

                        lastCommnd.Parameters.AddWithValue("@cc", Convert.ToInt32(changingCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateCarAndViolator.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateCarAndViolator.Close();
                    }
                }
                else if (criterion.Text == "Статус оплаты")
                {
                    if (changingCriterion.Text != CheckJournalEntry.CheckPaymentStatus(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckJournalEntry.CheckPaymentStatus(changingCriterion.Text));
                        return;
                    }
                    string query = @"SELECT ViolationStatusCode FROM ViolationStatus WHERE ViolationStatusName = '" + changingCriterion.Text + "'";
                    int statusCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            statusCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    using (SqlConnection updateCarAndViolator = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateCarAndViolator.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolationsJournal SET ViolationStatusCode = @status WHERE EntryNumber = @code";

                        lastCommnd.Parameters.AddWithValue("@status", statusCode);
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateCarAndViolator.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateCarAndViolator.Close();
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
                ChangeJournalEntry changeJournalEntry = new ChangeJournalEntry();
                changeJournalEntry.Show();
                this.Close();
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "obnovlenie_dannykh_zapisej.htm");
        }
    }
}