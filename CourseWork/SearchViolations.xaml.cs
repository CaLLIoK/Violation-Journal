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
    /// Логика взаимодействия для SearchViolations.xaml
    /// </summary>
    public partial class SearchViolations : Window
    {
        public SearchViolations()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (criterion.Text == "Название нарушения")
            {
                if (searchCriterion.Text != CheckViolation.CheckViolationName(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolation.CheckViolationName(searchCriterion.Text));
                    return;
                }
            }
            else if (criterion.Text == "Сумма штрафа")
            {
                if (searchCriterion.Text != CheckViolation.CheckViolationCost(searchCriterion.Text))
                {
                    System.Windows.MessageBox.Show(CheckViolation.CheckViolationCost(searchCriterion.Text));
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

            ViolationsSearchResults violationsSearchResults = new ViolationsSearchResults();
            violationsSearchResults.Show();
            this.Close();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            SearchInfo searchInfo = new SearchInfo();
            searchInfo.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "poisk_dannykh_sredi_narushenij.htm");
        }
    }
}
