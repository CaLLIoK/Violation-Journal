using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для StatusConfirmation.xaml
    /// </summary>
    public partial class StatusConfirmation : Window
    {
        string uniqueCode = "un19emp10yeec0d3";
        int counter = 4;
        public StatusConfirmation()
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

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (code.Text == uniqueCode)
            {
                LoginWindow login = new LoginWindow();
                this.Close();
                login.Show();
            }
            else
            {
                MessageBox.Show("Вы ввели неверный код. Осталось попыток: " + (counter-1));
                --counter;
                if (counter == 0)
                {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
