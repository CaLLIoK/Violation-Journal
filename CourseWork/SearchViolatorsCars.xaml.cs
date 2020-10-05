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
    /// Логика взаимодействия для SearchViolatorsCars.xaml
    /// </summary>
    public partial class SearchViolatorsCars : Window
    {
        public SearchViolatorsCars()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (criterion.Text == "Модель автомобиля")
            {
                if (searchCriterion.Text != CheckCar.CheckCarModel(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckCar.CheckCarModel(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Цвет автомобиля")
            {
                if (searchCriterion.Text != CheckCar.CheckCarColor(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckCar.CheckCarColor(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Номер автомобиля")
            {
                if (searchCriterion.Text != CheckCar.CheckCarStateNumber(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckCar.CheckCarStateNumber(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Фамилия владельца")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorSurname(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorSurname(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Имя владельца")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorName(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorName(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Отчество владельца")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorPatronymic(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPatronymic(searchCriterion.Text));
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Вы не выбрали критерий поиска.");
                return;
            }
            StreamWriter writeCriterion = new StreamWriter("Criterion.txt");
            writeCriterion.Write(criterion.Text);
            writeCriterion.Close();

            StreamWriter writeSearchCriterion = new StreamWriter("SearchCriterion.txt");
            writeSearchCriterion.Write(searchCriterion.Text);
            writeSearchCriterion.Close();

            ViolatorsCarsSearchResult violatorsCarsSearchResult = new ViolatorsCarsSearchResult();
            violatorsCarsSearchResult.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            SearchInfo searchInfo = new SearchInfo();
            searchInfo.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "poisk_dannykh_sredi_avtomobilej_narushitelej.htm");
        }
    }
}