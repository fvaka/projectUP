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
using System.Windows.Shapes;

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для history.xaml
    /// </summary>
    public partial class history : Window
    {
        private readonly AppDBContext db = new AppDBContext();
        private MainWindow _mainWindow;
        public history(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            LoadPartners();
            cmbPartners.SelectedIndex = 1;
        }

        private void LoadPartners()
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var partners = db.партнерыs 
                        .OrderBy(t => t.название)
                        .Select(p => new
                        {
                            id = p.ID,
                            partner_name = p.название
                        })
                        .ToList();

                    cmbPartners.DisplayMemberPath = "partner_name"; 
                    cmbPartners.SelectedValuePath = "id";
                    cmbPartners.ItemsSource = partners;
                    Console.WriteLine($"Загружено {partners.Count} партнеров в ComboBox");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при загрузке партнеров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadHistory(int partnerId)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var history = (from h in db.история_Продажs 
                                   join o in db.заявкиs on h.id_заявки equals o.ID
                                   join oi in db.элементы_Заявокs on o.ID equals oi.id_заявки
                                   join p in db.продукцияs on oi.id_продукции equals p.ID
                                   where o.id_партнера == partnerId
                                   select new
                                   {
                                       OrderID = o.ID,
                                       ProductName = p.название,
                                       ImplementationDate = oi.дата_производства,
                                       Quantity = oi.количество,
                                       Status = h.статус_заявки,
                                       StatusChangeDate = h.дата_изменения_статуса
                                   }).ToList();

                    dataGridView1.ItemsSource = history; 
                    Console.WriteLine($"Загружено {history.Count} записей истории для партнера ID={partnerId}");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при возврате к главному окну: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPartners.SelectedValue != null)
            {
                int partnerId = (int)cmbPartners.SelectedValue;
                LoadHistory(partnerId);
            }
        }

        private void CmbPartners_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPartners.SelectedValue != null)
            {
                int partnerId = (int)cmbPartners.SelectedValue;
                LoadHistory(partnerId);
            }
        }
    }
}
