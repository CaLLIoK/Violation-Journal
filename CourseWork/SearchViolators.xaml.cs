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
    /// Логика взаимодействия для SearchViolators.xaml
    /// </summary>
    public partial class SearchViolators : Window
    {
        public SearchViolators()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (criterion.Text == "Фамилия нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorSurname(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorSurname(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Имя нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorName(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorName(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Отчество нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorPatronymic(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPatronymic(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Паспорт нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorPasportNumber(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPasportNumber(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Телефон нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorPhoneNumber(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorPhoneNumber(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Город нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorTown(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorTown(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Улица нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorStreet(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorStreet(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Дом нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorHouseNumber(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorHouseNumber(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Квартира нарушителя")
            {
                if (searchCriterion.Text != CheckViolator.CheckViolatorApartment(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolator.CheckViolatorApartment(searchCriterion.Text));
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

            ViolatorsSearchResults violatorsSearchResults = new ViolatorsSearchResults();
            violatorsSearchResults.Show();
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
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "poisk_dannykh_sredi_narushitelej.htm");
        }
    }
}