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
    /// Логика взаимодействия для UpdateCarsData.xaml
    /// </summary>
    public partial class UpdateCarsData : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public UpdateCarsData()
        {
            InitializeComponent();
            string violator = string.Empty;
            List<int> codeList = new List<int>();
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
                }
            }
            dataReader.Close();
            string query2 = @"SELECT CarCode FROM ViolatorCar ORDER BY CarCode";
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
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if (criterion.Text != string.Empty && searchCriterion.Text != string.Empty)
            {
                if (criterion.Text == "Марка автомобиля")
                {
                    if (changingCriterion.Text != CheckCar.CheckCarModel(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckCar.CheckCarModel(changingCriterion.Text));
                        return;
                    }
                    string query = @"SELECT CarModelCode FROM CarModel WHERE CarModelName = '" + changingCriterion.Text + "'";
                    int carModelCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            carModelCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такой марки автомобиля нет в базе.");
                        return;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    using (SqlConnection updateCarModel = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateCarModel.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolatorCar SET CarModelCode = @carModel WHERE CarCode = @code";

                        lastCommnd.Parameters.AddWithValue("@carModel", carModelCode);
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateCarModel.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateCarModel.Close();
                    }
                }
                else if (criterion.Text == "Цвет автомобиля")
                {
                    if (changingCriterion.Text != CheckCar.CheckCarColor(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckCar.CheckCarColor(changingCriterion.Text));
                        return;
                    }
                    string query = @"SELECT ColorCode FROM Color WHERE ColorName = '" + changingCriterion.Text + "'";
                    int colorCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            colorCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого цвета нет в базе.");
                        return;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    using (SqlConnection updateColor = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateColor.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolatorCar SET ColorCode = @color WHERE CarCode = @code";

                        lastCommnd.Parameters.AddWithValue("@color", colorCode);
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateColor.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateColor.Close();
                    }
                }
                else if (criterion.Text == "Номер автомобиля")
                {
                    if (changingCriterion.Text != CheckCar.CheckCarStateNumber(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckCar.CheckCarStateNumber(changingCriterion.Text));
                        return;
                    }
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    string findReplays = "SELECT CarCode FROM ViolatorCar WHERE CarStatetNumber = '" + changingCriterion.Text + "'";
                    SqlCommand sqlCommand3 = new SqlCommand(findReplays, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        MessageBox.Show("Вы ввели уже занятый номер автомобиля.");
                        return;
                    }
                    else
                    {
                        using (SqlConnection updateStateNumber = new SqlConnection(connectionString))
                        using (SqlCommand lastCommnd = updateStateNumber.CreateCommand())
                        {
                            lastCommnd.CommandText = "UPDATE ViolatorCar SET CarStatetNumber = @number WHERE CarCode = @code";

                            lastCommnd.Parameters.AddWithValue("@number", changingCriterion.Text);
                            lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                            updateStateNumber.Open();
                            lastCommnd.ExecuteNonQuery();
                            updateStateNumber.Close();
                        }
                    }
                    sqlConnection.Close();
                }
                else if (criterion.Text == "Код владельца")
                {
                    if (changingCriterion.Text == string.Empty)
                    {
                        MessageBox.Show("Вы не указали код нарушителя.");
                    }
                    else
                    {
                        char[] violatorCodeArray = changingCriterion.Text.ToCharArray();
                        for (int i = 0; i < violatorCodeArray.Length; i++)
                        {
                            if (!char.IsDigit(violatorCodeArray[i]))
                            {
                                MessageBox.Show("Вы указали в коде недопустимые символы.");
                                return;
                            }
                        }
                    }
                    string query = @"SELECT ViolatorCode FROM Violator WHERE ViolatorCode = '" + changingCriterion.Text + "'";
                    int violatorCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            violatorCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBoxResult meboxResult = MessageBox.Show("Такого нарушителя нет в списке. Можете добавить нового нарушителя.", "Предупреждение", MessageBoxButton.YesNo);
                        if (meboxResult == MessageBoxResult.Yes)
                        {
                            AddViolator addViolator = new AddViolator();
                            addViolator.Show();
                            this.Close();
                        }
                        return;
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    using (SqlConnection updateViolatorCode = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateViolatorCode.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE ViolatorCar SET ViolatorCode = @violatorCode WHERE CarCode = @code";
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@violatorCode", violatorCode);

                        updateViolatorCode.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateViolatorCode.Close();
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
                ChangeViolatorsCars changeViolarorsCars = new ChangeViolatorsCars();
                changeViolarorsCars.Show();
                this.Close();
            }
        }

        private void BackToWindow(object sender, RoutedEventArgs e)
        {
            ChangeViolatorsCars changeViolarorsCars = new ChangeViolatorsCars();
            changeViolarorsCars.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "obnovlenie_dannykh_avtomobilej.htm");
        }
    }
}