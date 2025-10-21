using Microsoft.EntityFrameworkCore;
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

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDBContext dBContext = new AppDBContext();
        public MainWindow()
        {
            InitializeComponent();
            LoadPartners();

        }
        public void LoadPartners()
        {
            try
            {
                stackPanel1.Children.Clear();
                var partners = dBContext.партнерыs.ToList();
                foreach (var partner in partners)
                {
                    try
                    {

                        var commonVal = dBContext.элементы_Заявокs.Where(r => r.ID == partner.ID).Sum(r => r.количество);

                        double percent = commonVal >= 300000 ? 15 : commonVal >= 50000 ? 10 : commonVal >= 10000 ? 5 : 0;

                        var partnerType = dBContext.партнерыs.FirstOrDefault(t => t.ID == partner.ID)?.тип_партнера ?? "Неизвестный тип";

                        PartnerControl pc = new PartnerControl();
                        pc.DataSet(
                            partner.тип_партнера,
                            partner.название,
                            partner.ФИО_директора,
                            partner.телефон,
                            partner.рейтинг.ToString(),
                            percent
                            );
                        stackPanel1.Children.Add( pc );
                    }
                    catch 
                    {

                        System.Windows.Forms.MessageBox.Show($"Ошибка при загрузке партнера {partner}");
                    }
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Произошла ошибка при загрузке данных. Попробуйте позже.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }

}
