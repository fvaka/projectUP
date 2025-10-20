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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public void DataSet(string partner_type, string partner_name, string director, string phone, string rait, double percent)
        {
            try
            {
                typeLB.Content = partner_type;
                partnerNameLB.Content = partner_name;
                discountAmount.Content = percent + "%";
                directorLB.Content = director;
                phoneLB.Content = "+7" + phone;
                ratingLB.Content = "Рейтинг: " + rait;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка инициализации данных: " + ex.Message);
            }
        }
    }
}
