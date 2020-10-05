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
    /// Логика взаимодействия для UpdateViolatorData.xaml
    /// </summary>
    public partial class UpdateViolatorData : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public UpdateViolatorData()
        {
            InitializeComponent();
            List<int> codeList = new List<int>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"SELECT ViolatorCode FROM Violator ORDER BY ViolatorCode";
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
                if (criterion.Text == "Имя")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorName(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorName(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updateName = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateName.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violator SET ViolatorName = @name WHERE ViolatorCode = @code";

                        lastCommnd.Parameters.AddWithValue("@name", changingCriterion.Text);
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateName.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateName.Close();
                    }
                }
                else if (criterion.Text == "Фамилия")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorSurname(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorSurname(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updateSurname = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updateSurname.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violator SET ViolatorSurname = @surname WHERE ViolatorCode = @code";

                        lastCommnd.Parameters.AddWithValue("@surname", changingCriterion.Text);
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));

                        updateSurname.Open();
                        lastCommnd.ExecuteNonQuery();
                        updateSurname.Close();
                    }
                }
                else if (criterion.Text == "Отчество")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorPatronymic(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorPatronymic(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updatePatronymic = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updatePatronymic.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violator SET ViolatorPatronymic = @patronymic WHERE ViolatorCode = @code";
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@patronymic", changingCriterion.Text);

                        updatePatronymic.Open();
                        lastCommnd.ExecuteNonQuery();
                        updatePatronymic.Close();
                    }
                }
                else if (criterion.Text == "Номер паспорта")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorPasportNumber(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorPasportNumber(changingCriterion.Text));
                        return;
                    }
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    string findReplays = "SELECT ViolatorCode FROM Violator WHERE ViolatorPasportNumber = '" + changingCriterion.Text + "'";
                    SqlCommand sqlCommand3 = new SqlCommand(findReplays, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        MessageBox.Show("Вы ввели уже занятый номер паспорта.");
                        return;
                    }
                    else
                    {
                        using (SqlConnection updatePasportNumber = new SqlConnection(connectionString))
                        using (SqlCommand lastCommnd = updatePasportNumber.CreateCommand())
                        {
                            lastCommnd.CommandText = "UPDATE Violator SET ViolatorPasportNumber = @pasport WHERE ViolatorCode = @code";
                            lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                            lastCommnd.Parameters.AddWithValue("@pasport", changingCriterion.Text);

                            updatePasportNumber.Open();
                            lastCommnd.ExecuteNonQuery();
                            updatePasportNumber.Close();
                        }
                    }
                    sqlConnection.Close();
                }
                else if (criterion.Text == "Номер телефона")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorPhoneNumber(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorPhoneNumber(changingCriterion.Text));
                        return;
                    }
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    string findReplays = "SELECT ViolatorCode FROM Violator WHERE ViolatorPhoneNumber = '" + changingCriterion.Text + "'";
                    SqlCommand sqlCommand3 = new SqlCommand(findReplays, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        MessageBox.Show("Вы ввели уже занятый номер телефона.");
                        return;
                    }
                    else
                    {
                        using (SqlConnection updatePhoneNumber = new SqlConnection(connectionString))
                        using (SqlCommand lastCommnd = updatePhoneNumber.CreateCommand())
                        {
                            lastCommnd.CommandText = "UPDATE Violator SET ViolatorPhoneNumber = @phone WHERE ViolatorCode = @code";
                            lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                            lastCommnd.Parameters.AddWithValue("@phone", changingCriterion.Text);

                            updatePhoneNumber.Open();
                            lastCommnd.ExecuteNonQuery();
                            updatePhoneNumber.Close();
                        }
                    }
                    sqlConnection.Close();
                }
                else if (criterion.Text == "Город")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorTown(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorTown(changingCriterion.Text));
                        return;
                    }
                    string selectTownCode = @"SELECT TownCode FROM Town WHERE TownName = '" + changingCriterion.Text + "'";
                    int townCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(selectTownCode, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            townCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("В базе нет данных нет такого города.");
                        return;
                    }
                    sqlDataReader.Close();
                    using (SqlConnection updatePasportNumber = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updatePasportNumber.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violator SET ViolatorTownCode = @townCode WHERE ViolatorCode = @code";
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@townCode", townCode);

                        updatePasportNumber.Open();
                        lastCommnd.ExecuteNonQuery();
                        updatePasportNumber.Close();
                    }
                }
                else if (criterion.Text == "Улица")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorStreet(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorStreet(changingCriterion.Text));
                        return;
                    }
                    string selectTownCode = @"SELECT StreetCode FROM Street WHERE StreetName = '" + changingCriterion.Text + "'";
                    int streetCode = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(selectTownCode, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            streetCode = Convert.ToInt32(sqlDataReader[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("В базе нет данных нет такой улицы.");
                        return;
                    }
                    sqlDataReader.Close();
                    using (SqlConnection updatePasportNumber = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updatePasportNumber.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violator SET ViolatorStreetCode = @streetCode WHERE ViolatorCode = @code";
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@streetCode", streetCode);

                        updatePasportNumber.Open();
                        lastCommnd.ExecuteNonQuery();
                        updatePasportNumber.Close();
                    }
                }
                else if (criterion.Text == "Номер дома")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorHouseNumber(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorHouseNumber(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updatePatronymic = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updatePatronymic.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violator SET ViolatorHouseNumber = @house WHERE ViolatorCode = @code";
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@house", changingCriterion.Text);

                        updatePatronymic.Open();
                        lastCommnd.ExecuteNonQuery();
                        updatePatronymic.Close();
                    }
                }
                else if (criterion.Text == "Номер квартиры")
                {
                    if (changingCriterion.Text != CheckViolator.CheckViolatorApartment(changingCriterion.Text))
                    {
                        MessageBox.Show(CheckViolator.CheckViolatorApartment(changingCriterion.Text));
                        return;
                    }
                    using (SqlConnection updatePatronymic = new SqlConnection(connectionString))
                    using (SqlCommand lastCommnd = updatePatronymic.CreateCommand())
                    {
                        lastCommnd.CommandText = "UPDATE Violator SET ViolatorApartmentNumber = @apartment WHERE ViolatorCode = @code";
                        lastCommnd.Parameters.AddWithValue("@code", Convert.ToInt32(searchCriterion.Text));
                        lastCommnd.Parameters.AddWithValue("@apartment", changingCriterion.Text);

                        updatePatronymic.Open();
                        lastCommnd.ExecuteNonQuery();
                        updatePatronymic.Close();
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
                ChangeViolatorData changeSparePartsData = new ChangeViolatorData();
                changeSparePartsData.Show();
                this.Close();
            }
        }

        private void BackToWindow(object sender, RoutedEventArgs e)
        {
            ChangeViolatorData changeViolatorData = new ChangeViolatorData();
            changeViolatorData.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "obnovlenie_dannykh_narushitelej.htm");
        }
    }
}