using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nameField.Text != CheckViolator.CheckViolatorName(nameField.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorName(nameField.Text));
                return;
            }

            //if (nameField.Text == "")
            //{
            //    MessageBox.Show("Вы не ввели имя");
            //    return;
            //}
            //else
            //{
            //    if (nameField.Text.Length > 2 && nameField.Text.Length <= 30)
            //    {
            //        char[] nameArray = nameField.Text.ToCharArray();
            //        for (int i = 0; i < nameArray.Length; i++)
            //        {
            //            if (!char.IsLetter(nameArray[i]))
            //            {
            //                MessageBox.Show("Вы указали в имени недопустимые символы.");
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Допустимая длина имени 3-30 символов.");
            //        return;
            //    }
            //}

            //if (surnamField.Text == "")
            //{
            //    MessageBox.Show("Вы не ввели фамилию");
            //    return;
            //}
            //else
            //{
            //    if (surnamField.Text.Length > 2 && surnamField.Text.Length <= 30)
            //    {
            //        char[] surnameArray = surnamField.Text.ToCharArray();
            //        for (int i = 0; i < surnameArray.Length; i++)
            //        {
            //            if (!char.IsLetter(surnameArray[i]))
            //            {
            //                MessageBox.Show("Вы указали в фамилии недопустимые символы.");
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Допустимая фамилии имени 3-30 символов.");
            //        return;
            //    }
            //}

            if (surnamField.Text != CheckViolator.CheckViolatorSurname(surnamField.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorSurname(surnamField.Text));
                return;
            }

            if (patronymicField.Text != CheckViolator.CheckViolatorPatronymic(patronymicField.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPatronymic(patronymicField.Text));
                return;
            }

            //if (patronymicField.Text == "")
            //{
            //    MessageBox.Show("Вы не ввели отчество");
            //    return;
            //}
            //else
            //{
            //    if (patronymicField.Text.Length > 2 && patronymicField.Text.Length <= 30)
            //    {
            //        char[] patronymicArray = surnamField.Text.ToCharArray();
            //        for (int i = 0; i < patronymicArray.Length; i++)
            //        {
            //            if (!char.IsLetter(patronymicArray[i]))
            //            {
            //                MessageBox.Show("Вы указали в отчестве недопустимые символы.");
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Допустимая отчества имени 3-30 символов.");
            //        return;
            //    }
            //}

            if (phoneNumber.Text != CheckViolator.CheckViolatorPhoneNumber(phoneNumber.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPhoneNumber(phoneNumber.Text));
                return;
            }

            //if (phoneNumber.Text == "")
            //{
            //    MessageBox.Show("Вы не ввели номер телефона");
            //    return;
            //}
            //else
            //{
            //    if (phoneNumber.Text.Length == 17)
            //    {
            //        if (!Regex.IsMatch(phoneNumber.Text, @"(\+|)(375|)(\ |)(\(|)[29|25|33|44]\)\d{3}\-\d{2}\-\d{2}"))
            //        {
            //            MessageBox.Show("Вы указали в номере телефона недопустимые символы.");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Допустимая длина номера телефона 17 символов (с учетом символов '+', '(' и ')'.");
            //        return;
            //    }
            //}

            if (loginField.Text == "")
            {
                System.Windows.MessageBox.Show("Вы не ввели логин");
                return;
            }
            else
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
            }

            if (passwordField.Password.ToString() == "")
            {
                System.Windows.MessageBox.Show("Вы не ввели пароль");
                return;
            }
            else
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
            }

            string userLogin = loginField.Text;

            SqlConnection myConnectionString = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=Journal; Integrated Security=True");
            string mySelectQuery = "SELECT * FROM Users WHERE [UserLogin] = '" + userLogin + "'";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(mySelectQuery, myConnectionString))
            {
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    System.Windows.MessageBox.Show("Этот логин занят. Введите другой");
                }
                else if (table.Rows.Count == 0)
                {
                    //db = new UserContext();
                    //var users = new List<User>();
                    //User user = new User(loginField.Text, passwordField.Password.ToString(), nameField.Text, surnamField.Text, patronymicField.Text, phoneNumber.Text);
                    //users.Add(user);
                    //db.Users.AddRange(users);
                    //db.SaveChanges();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT Users (UserLogin, UserPassword, UserName, UserSurname, UserPatronymic, UserPhoneNumber) VALUES (@login, @password, @name, @surname, @patronymic, @phone)";
                    cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = loginField.Text;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = passwordField.Password.ToString();
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = nameField.Text;
                    cmd.Parameters.Add("@surname", SqlDbType.VarChar).Value = surnamField.Text;
                    cmd.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = patronymicField.Text;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phoneNumber.Text;
                    cmd.Connection = myConnectionString;
                    myConnectionString.Open();
                    cmd.ExecuteNonQuery();
                    myConnectionString.Close();
                    System.Windows.MessageBox.Show("Регистрация прошла успешно");
                    LoginWindow login = new LoginWindow();
                    this.Close();
                    login.Show();                  
                }
            }            
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "okno_registratsii.htm");
        }
    }
}