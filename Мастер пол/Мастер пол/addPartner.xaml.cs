using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для addPartner.xaml
    /// </summary>
    public partial class addPartner : System.Windows.Window
    {
        private AppDBContext dbContext = new AppDBContext();
        private readonly MainWindow _mainWindow;

        public addPartner(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            LoadPartnerTypes();
            ClearFormFields();
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var main = new MainWindow();
                main.Closed += (s, args) => System.Windows.Application.Current.Shutdown();
                this.Close(); 
                main.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при открытии формы: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadPartnerTypes()
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    var partnerTypes = db.партнерыs
                        .Where(p => p.тип_партнера != null)
                        .Select(p => p.тип_партнера)
                        .Distinct()
                        .OrderBy(t => t)
                        .ToList();

                    typeCMB.ItemsSource = partnerTypes;
                    typeCMB.SelectedIndex = -1;

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Ошибка при загрузке типов партнеров: {ex.Message}");
                }
            }
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameTB.Text) ||
                    typeCMB.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(addressTB.Text) ||
                    string.IsNullOrWhiteSpace(phoneTB.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Заполните все обязательные поля!", "Ошибка");
                    return;
                }

                if (!int.TryParse(ratingTB.Text, out int рейтинг) || рейтинг < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Рейтинг должен быть целым неотрицательным числом!", "Ошибка");
                    return;
                }

                var newPartner = new Партнеры
                {
                    название = nameTB.Text,
                    тип_партнера = (string)typeCMB.SelectedItem, 
                    рейтинг = рейтинг, 
                    юр_адрес = addressTB.Text,
                    ФИО_директора = FIODTb.Text,
                    телефон = phoneTB.Text,
                    email = emailTb.Text,
                    ИНН = InnTB.Text
                };

                using (var db = new AppDBContext())
                {
                    db.партнерыs.Add(newPartner);
                    db.SaveChanges();
                }

                System.Windows.Forms.MessageBox.Show("Партнер успешно добавлен!", "Успех");

                _mainWindow.LoadPartners();
                _mainWindow.Show();
                Close();
            }
            catch (FormatException)
            {
                System.Windows.Forms.MessageBox.Show("Некорректный формат данных в числовых полях!", "Ошибка");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка");
            }
        }
        private void ClearFormFields()
        {
            nameTB.Text = string.Empty;
            typeCMB.SelectedIndex = -1;
            ratingTB.Text = string.Empty;
            addressTB.Text = string.Empty;
            FIODTb.Text = string.Empty;
            phoneTB.Text = string.Empty;
            emailTb.Text = string.Empty;
            InnTB.Text = string.Empty;
        }

    }
}
