using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ChangeAccountData.xaml
    /// </summary>
    public partial class ChangeAccountData : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public ChangeAccountData()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamReader readLogin = new StreamReader("UserLogin.txt");
            string currentUserLogin = readLogin.ReadLine();
            readLogin.Close();

            if (nameField.Text != "")
            {
                if (nameField.Text.Length > 2 && nameField.Text.Length <= 30)
                {
                    char[] nameArray = nameField.Text.ToCharArray();
                    for (int i = 0; i < nameArray.Length; i++)
                    {
                        if (!char.IsLetter(nameArray[i]) && nameArray[i] != '-')
                        {
                            System.Windows.MessageBox.Show("Вы указали в имени недопустимые символы.");
                            return;
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Допустимая длина имени 3-30 символов.");
                    return;
                }

                using (SqlConnection changeName = new SqlConnection(connectionString))
                using (SqlCommand command = changeName.CreateCommand())
                {
                    command.CommandText = "UPDATE Users SET UserName = '" + nameField.Text + "' WHERE UserLogin = '" + currentUserLogin + "'";
                    changeName.Open();
                    command.ExecuteNonQuery();
                    changeName.Close();
                }
            }

            if (surnamField.Text != "")
            {
                if (surnamField.Text.Length > 2 && surnamField.Text.Length <= 30)
                {
                    char[] surnameArray = surnamField.Text.ToCharArray();
                    for (int i = 0; i < surnameArray.Length; i++)
                    {
                        if (!char.IsLetter(surnameArray[i]) && surnameArray[i] != '-')
                        {
                            System.Windows.MessageBox.Show("Вы указали в фамилии недопустимые символы.");
                            return;
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Допустимая длина фамилии 3-30 символов.");
                    return;
                }

                using (SqlConnection changeSurname = new SqlConnection(connectionString))
                using (SqlCommand command = changeSurname.CreateCommand())
                {
                    command.CommandText = "UPDATE Users SET UserSurname = '" + surnamField.Text + "' WHERE UserLogin = '" + currentUserLogin + "'";
                    changeSurname.Open();
                    command.ExecuteNonQuery();
                    changeSurname.Close();
                }
            }

            if (patronymicField.Text != "")
            {
                if (patronymicField.Text.Length > 2 && patronymicField.Text.Length <= 30)
                {
                    char[] patronymicArray = patronymicField.Text.ToCharArray();
                    for (int i = 0; i < patronymicArray.Length; i++)
                    {
                        if (!char.IsLetter(patronymicArray[i]) && patronymicArray[i] != '-')
                        {
                            System.Windows.MessageBox.Show("Вы указали в отчестве недопустимые символы.");
                            return;
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Допустимая длина отчества 3-30 символов.");
                    return;
                }

                using (SqlConnection changeSurname = new SqlConnection(connectionString))
                using (SqlCommand command = changeSurname.CreateCommand())
                {
                    command.CommandText = "UPDATE Users SET UserPatronymic = '" + patronymicField.Text + "' WHERE UserLogin = '" + currentUserLogin + "'";
                    changeSurname.Open();
                    command.ExecuteNonQuery();
                    changeSurname.Close();
                }
            }

            if (phoneField.Text != "")
            {
                if (phoneField.Text.Length == 17)
                {
                    if (!Regex.IsMatch(phoneField.Text.ToString(), @"(\+|)(375|)(\ |)(\(|)(29|25|33|44)\)\d{3}\-\d{2}\-\d{2}"))
                    {
                        System.Windows.MessageBox.Show("Вы указали в номере телефона недопустимые символы.");
                        return;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Номер телефона должен выглядеть следующим образом: '+375(25/29/33/44)***-**-**'.");
                    return;
                }

                using (SqlConnection changeSurname = new SqlConnection(connectionString))
                using (SqlCommand command = changeSurname.CreateCommand())
                {
                    command.CommandText = "UPDATE Users SET UserPhoneNumber = '" + phoneField.Text + "' WHERE UserLogin = '" + currentUserLogin + "'";
                    changeSurname.Open();
                    command.ExecuteNonQuery();
                    changeSurname.Close();
                }
            }

            if (loginField.Text != "")
            {
                if (loginField.Text.Length > 2 && loginField.Text.Length <= 20)
                {
                    char[] loginArray = loginField.Text.ToCharArray();
                    for (int i = 0; i < loginArray.Length; i++)
                    {
                        if (!char.IsLetter(loginArray[i]) && !char.IsDigit(loginArray[i]) && loginArray[i] != '_')
                        {
                            System.Windows.MessageBox.Show("Вы указали в логине недопустимые символы.");
                            return;
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Допустимая длина логина 3-20 символов.");
                    return;
                }

                using (SqlConnection changeLogin = new SqlConnection(connectionString))
                using (SqlCommand command = changeLogin.CreateCommand())
                {
                    StreamWriter writeLogin = new StreamWriter("UserLogin.txt");
                    writeLogin.Write(loginField.Text);
                    writeLogin.Close();
                    command.CommandText = "UPDATE Users SET UserLogin = '" + loginField.Text + "' WHERE UserLogin = '" + currentUserLogin + "'";
                    changeLogin.Open();
                    command.ExecuteNonQuery();
                    changeLogin.Close();
                    currentUserLogin = loginField.Text;
                }
            }

            if (passwordField.Password.ToString() != "")
            {
                if (passwordField.Password.ToString().Length > 2 && passwordField.Password.ToString().Length <= 20)
                {
                    char[] passwordArray = passwordField.Password.ToString().ToCharArray();
                    for (int i = 0; i < passwordArray.Length; i++)
                    {
                        if (!char.IsLetter(passwordArray[i]) && !char.IsDigit(passwordArray[i]) && passwordArray[i] != '_' && passwordArray[i] != '*')
                        {
                            System.Windows.MessageBox.Show("Вы указали в пароле недопустимые символы.");
                            return;
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Допустимая длина пароля 3-20 символов.");
                    return;
                }

                using (SqlConnection changePassword = new SqlConnection(connectionString))
                using (SqlCommand command = changePassword.CreateCommand())
                {
                    command.CommandText = "UPDATE Users SET UserPassword = '" + passwordField.Password.ToString() + "' WHERE UserLogin = '" + currentUserLogin + "'";
                    changePassword.Open();
                    command.ExecuteNonQuery();
                    changePassword.Close();
                }
            }

            MessageBoxResult mboxResult = System.Windows.MessageBox.Show("Изменения сохранены. Желаете изменить что-нибудь еще?", "Предупреждение", MessageBoxButton.YesNo);
            if (mboxResult == MessageBoxResult.No)
            {
                MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
                mainMenuEmployee.Show();
                this.Close();
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "izmenenie_dannykh_akkaunta.htm");
        }
    }
}