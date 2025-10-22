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

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private AppDBContext dbContext = new AppDBContext();
        
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fio = loginTb.Text.Trim();
            string pass = PasswordTb.Password.Trim();

            if (string.IsNullOrEmpty(fio) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Введите ФИО и паспортные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var emp = dbContext.сотрудникиs.FirstOrDefault(empl => empl.ФИО == fio && empl.паспортные_данные == pass);
            switch (emp.роль)
            {
                case "Менеджер":
                    MainWindow show = new MainWindow(this);
                    show.Show();
                    this.Close();
                    break;
                default:
                    MessageBox.Show($"Должность '{emp.роль}' не поддерживается!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
    }
    }

