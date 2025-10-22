using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для PartnerControl.xaml
    /// </summary>
    public partial class PartnerControl : System.Windows.Controls.UserControl
    {
        public int PartnerID { get; set; }
        private MainWindow _mainWindow;
        public PartnerControl(MainWindow main)
        {
            InitializeComponent();
            _mainWindow = main;
        }

        public void DataSet(string partner_type, string partner_name, string director, string phone, string rait, double percent, int partnerID)
        {
            try
            {
                typeLB.Content = partner_type;
                partnerNameLB.Content = partner_name;
                discountAmount.Content = percent + "%";
                directorLB.Content = director;
                phoneLB.Content = "+7" + phone;
                ratingLB.Content = "Рейтинг: " + rait;
                PartnerID = partnerID;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка инициализации данных: " + ex.Message);
            }
        }

        private void PartnerControl_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var updateForm = new editPartner(PartnerID, _mainWindow);
                updateForm.Closed += (s, args) => _mainWindow.LoadPartners(); 
                updateForm.Show();
                Window parentWindow = Window.GetWindow(this);
                parentWindow?.Hide(); 
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при открытии формы: {ex.Message}", "Ошибка");
            }
        }

    }

}
