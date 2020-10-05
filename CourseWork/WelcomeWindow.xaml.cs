using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter loginStreamWriter = new StreamWriter("UserLogin.txt");
            loginStreamWriter.Write("");
            loginStreamWriter.Close();
            MainMenuGuest mainMenu = new MainMenuGuest();
            this.Close();
            mainMenu.Show();          
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            StatusConfirmation status = new StatusConfirmation();
            this.Close();
            status.Show();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "nachalo_raboty.htm");
        }
    }
}