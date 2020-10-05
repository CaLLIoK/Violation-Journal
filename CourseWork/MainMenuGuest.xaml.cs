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
    /// Логика взаимодействия для MainMenuGuest.xaml
    /// </summary>
    public partial class MainMenuGuest : Window
    {
        public MainMenuGuest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (surname.Text != CheckViolator.CheckViolatorSurname(surname.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorSurname(surname.Text));
                return;
            }

            if (name.Text != CheckViolator.CheckViolatorName(name.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorName(name.Text));
                return;
            }

            if (patronymic.Text != CheckViolator.CheckViolatorPatronymic(patronymic.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPatronymic(patronymic.Text));
                return;
            }

            if (pasportNumber.Text != CheckViolator.CheckViolatorPasportNumber(pasportNumber.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPasportNumber(pasportNumber.Text));
                return;
            }

            StreamWriter pasportStreamWriter = new StreamWriter("ViolatorPasportNumber.txt");
            pasportStreamWriter.Write(pasportNumber.Text);
            pasportStreamWriter.Close();

            StreamWriter nameStreamWriter = new StreamWriter("ViolatorName.txt");
            nameStreamWriter.Write(name.Text);
            nameStreamWriter.Close();

            StreamWriter surnameStreamWriter = new StreamWriter("ViolatorSurname.txt");
            surnameStreamWriter.Write(surname.Text);
            surnameStreamWriter.Close();

            StreamWriter patronymicStreamWriter = new StreamWriter("ViolatorPatronymic.txt");
            patronymicStreamWriter.Write(patronymic.Text);
            patronymicStreamWriter.Close();

            ShowViolatorViolations showViolatorViolations = new ShowViolatorViolations();
            showViolatorViolations.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "rukovodstvo_gostya.htm");
        }
    }
}