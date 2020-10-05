using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для ChooseReportPeriod.xaml
    /// </summary>
    public partial class ChooseReportPeriod : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Journal;Integrated Security=True";

        public ChooseReportPeriod()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        [DllImport("user32")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId0);

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (!firstPeriod.SelectedDate.HasValue)
            {
                System.Windows.MessageBox.Show("Вы не выбрали дату начала периода.");
                return;
            }
            if (!lastPeriod.SelectedDate.HasValue)
            {
                System.Windows.MessageBox.Show("Вы не выбрали дату конца периода.");
                return;
            }
            if (firstPeriod.SelectedDate.Value.Date > lastPeriod.SelectedDate.Value.Date)
            {
                System.Windows.MessageBox.Show("Начало периода не может быть больше конца периода.");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks excelappworkbooks;
            Microsoft.Office.Interop.Excel.Workbook excelappworkbook;
            Microsoft.Office.Interop.Excel.Sheets excelsheets;
            Microsoft.Office.Interop.Excel.Worksheet excelworksheet;
            Microsoft.Office.Interop.Excel.Range excelcells;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.Interactive = false;
            uint processId;
            GetWindowThreadProcessId((IntPtr)excelapp.Hwnd, out processId);
            try
            {
                excelappworkbooks = excelapp.Workbooks;
                excelappworkbook = excelapp.Workbooks.Open(System.IO.Path.GetFullPath(@"ШаблонОтчета.xlsx"));
                excelsheets = excelappworkbook.Worksheets;
                excelworksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelsheets.get_Item(1);
                SqlConnection connection = new SqlConnection(connectionString);
                excelcells = excelworksheet.get_Range("G1");
                excelcells.Value = firstPeriod.SelectedDate.Value.Date;
                excelcells = excelworksheet.get_Range("I1");
                excelcells.Value = lastPeriod.SelectedDate.Value.Date;
                string query = "SELECT EntryNumber, EntryNumberDate, EntryNumberTime, ViolationName, CarStatetNumber, CarModelName, ViolatorSurname, ViolatorName, ViolatorPatronymic, ViolationCost, ViolationStatusName "
                         + " FROM ViolationsJournal, ViolatorCar, CarModel, ViolationStatus, Violator, Violation WHERE ViolationsJournal.ViolationCode = Violation.ViolationCode AND ViolationsJournal.CarCode = ViolatorCar.CarCode "
                         + " AND ViolatorCar.CarModelCode = CarModel.CarModelCode AND ViolatorCar.ViolatorCode = Violator.ViolatorCode AND ViolationsJournal.ViolationStatusCode = ViolationStatus.ViolationStatusCode AND EntryNumberDate between '" + firstPeriod.SelectedDate.Value.Date + "' AND '" + lastPeriod.SelectedDate.Value.Date + "'";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                int i = 4;
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        int k = 0;
                        for (int j = 1; j < 12; j++)
                        {
                            excelcells = (Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, j];
                            excelcells.Font.Name = "Times New Roman";
                            excelcells.Font.Size = 14;
                            excelcells.Borders.ColorIndex = 0;
                            excelcells.EntireColumn.AutoFit();
                            excelcells.EntireRow.AutoFit();
                            excelcells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            if (k == 1)
                            {
                                excelcells.Value2 = Convert.ToDateTime(sqlDataReader[k].ToString()).ToShortDateString();
                            }
                            else if (k == 9)
                            {
                                excelcells.Value2 = Convert.ToDouble(sqlDataReader[k].ToString());
                            }
                            else
                            {
                                excelcells.Value2 = sqlDataReader[k].ToString();
                            }
                            if (k < sqlDataReader.FieldCount)
                                ++k;
                        }
                        ++i;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Нет автонарушений за указанный период.");
                    return;
                }
                sqlDataReader.Close();
                path += @"\Отчет по автонарушениям за " + DateTime.Now.ToShortDateString() + ".xlsx";
                excelappworkbooks = excelapp.Workbooks;
                excelappworkbook = excelappworkbooks[1];
                excelappworkbook.SaveAs(path);
                connection.Close();
            }
            catch (Exception q)
            {
                System.Windows.MessageBox.Show(q.Message);
            }
            finally
            {
                Process.GetProcessById((int)processId).Kill();
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = System.Windows.Forms.HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "help.chm", navigator, "sozdanie_otcheta.htm");
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            MainMenuEmployee mainMenuEmployee = new MainMenuEmployee();
            mainMenuEmployee.Show();
            this.Close();
        }
    }
}