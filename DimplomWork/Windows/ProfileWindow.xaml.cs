using DimplomWork.Model;
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

namespace DimplomWork.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {

        private Users _currentUser;
        public ProfileWindow()
        {
            InitializeComponent();

            // Устанавливаем DataContext для привязок
            this.DataContext = App.currentUser;
            _currentUser = App.currentUser;

            // Загружаем данные пользователя
            LoadUserData();
        }

        private void LoadUserData()
        {
            if (_currentUser != null)
            {
                // Обновляем поля (на случай, если данные изменились)
                FIOTb.Text = _currentUser.full_name;
                PhoneTb.Text = _currentUser.phone;
                EmailTb.Text = _currentUser.email;

           
            }
        }

        // Режим редактирования
        private void EditUserInfBtn_Click(object sender, RoutedEventArgs e)
        {
            // Включаем редактирование полей
            FIOTb.IsEnabled = true;
            PhoneTb.IsEnabled = true;
            EmailTb.IsEnabled = true;

            // Меняем видимость кнопок
            EditUserInfBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Visible;
            CancelBtn.Visibility = Visibility.Visible;

            StatusTb.Text = "Режим редактирования";
            StatusTb.Foreground = System.Windows.Media.Brushes.Orange;

            // Фокус на поле ФИО
            FIOTb.Focus();
        }

        // Сохранение изменений
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполнения полей
                if (string.IsNullOrWhiteSpace(FIOTb.Text))
                {
                    MessageBox.Show("Введите ФИО!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(PhoneTb.Text))
                {
                    MessageBox.Show("Введите телефон!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(EmailTb.Text))
                {
                    MessageBox.Show("Введите Email!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Обновляем данные пользователя
                _currentUser.full_name = FIOTb.Text;
                _currentUser.phone = PhoneTb.Text;
                _currentUser.email = EmailTb.Text;

                // Сохраняем в базу данных
                App.context.SaveChanges();

                // Обновляем глобального пользователя
                App.currentUser = _currentUser;

                // Выключаем режим редактирования
                FIOTb.IsEnabled = false;
                PhoneTb.IsEnabled = false;
                EmailTb.IsEnabled = false;

                EditUserInfBtn.Visibility = Visibility.Visible;
                SaveBtn.Visibility = Visibility.Collapsed;
                CancelBtn.Visibility = Visibility.Collapsed;

                StatusTb.Text = "Данные сохранены!";
                StatusTb.Foreground = System.Windows.Media.Brushes.Green;

                MessageBox.Show("Данные успешно обновлены!", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Отмена редактирования
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаем старые данные
            FIOTb.Text = _currentUser.full_name;
            PhoneTb.Text = _currentUser.phone;
            EmailTb.Text = _currentUser.email;

            // Выключаем режим редактирования
            FIOTb.IsEnabled = false;
            PhoneTb.IsEnabled = false;
            EmailTb.IsEnabled = false;

            EditUserInfBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Collapsed;
            CancelBtn.Visibility = Visibility.Collapsed;

            StatusTb.Text = "";
        }

        // Назад
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    
    }
}
    
   

