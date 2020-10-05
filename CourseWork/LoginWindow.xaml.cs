using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = loginField.Text;
            string userPassword = passwordField.Password.ToString();

            string myConnectionString = @"Data Source=(local)\SQLEXPRESS; Initial Catalog = Journal; Integrated Security=True";
            string mySelectQuery = "SELECT * FROM Users WHERE [UserLogin] = '" + userLogin + "'and [UserPassword]='" + userPassword + "'";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(mySelectQuery, myConnectionString))
            {
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    StreamWriter loginFile = new StreamWriter("UserLogin.txt");
                    loginFile.Write(userLogin);
                    loginFile.Close();
                    MainMenuEmployee mainMenu = new MainMenuEmployee();                   
                    this.Close();
                    mainMenu.Show();
                }
                else if (table.Rows.Count == 0)
                {
                    System.Windows.MessageBox.Show("Неверный логин или пароль");
                    return;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Havenoacc_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            this.Close();
            registration.Show();           
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "okno_avtorizatsii.htm");
        }
    }
}