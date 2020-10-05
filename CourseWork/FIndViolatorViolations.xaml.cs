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
    /// Логика взаимодействия для FIndViolatorViolations.xaml
    /// </summary>
    public partial class FIndViolatorViolations : Window
    {
        public FIndViolatorViolations()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pasportNumber.Text != CheckViolator.CheckViolatorPasportNumber(pasportNumber.Text))
            {
                System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPasportNumber(pasportNumber.Text));
                return;
            }
            StreamWriter streamWriter = new StreamWriter("ViolatorPasportNumber.txt");
            streamWriter.Write(pasportNumber.Text);
            streamWriter.Close();
            ShowViolatorViolations showViolatorViolations = new ShowViolatorViolations();
            showViolatorViolations.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
            mainMenuEmployee.Show();
            this.Close();          
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "poisk_shtrafov_konktretnogo_narushenitelya.htm");
        }
    }
}