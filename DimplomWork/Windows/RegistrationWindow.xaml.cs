using DimplomWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            // Проверка заполненности всех полей
            if (string.IsNullOrEmpty(FIOTb.Text) ||string.IsNullOrEmpty(PhoneTb.Text) ||string.IsNullOrEmpty(EmailTb.Text) ||string.IsNullOrEmpty(LoginRegistrationTb.Text) ||
                string.IsNullOrEmpty(PasswordRegistrationPb.Password))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string fullName = FIOTb.Text;
            string phone = PhoneTb.Text;
            string email = EmailTb.Text;
            string login = LoginRegistrationTb.Text;
            string password = PasswordRegistrationPb.Password;




            // ПРОВЕРКА 3: Проверка на существующего пользователя
            var existingUser = App.context.Users.FirstOrDefault(u => u.login == login || u.email == email);
            if (existingUser != null)
            {
                if (existingUser.login == login)
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show("Пользователь с таким email уже зарегистрирован", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создание нового пользователя
            Users newUser = new Users
            {
                full_name = fullName,
                phone = phone,
                email = email,
                login = login,
                password = password,
                role_id = 1, //роль 2 соответствует роли клиента


            };
            // Добавление пользователя в базу данных

            App.context.Users.Add(newUser);
            App.context.SaveChanges();

            // Создание профиля пользователя


            MessageBox.Show("Пользователь успешно зарегистрирован.");
            FIOTb.Text = string.Empty;
            LoginRegistrationTb.Text = string.Empty;
            PhoneTb.Text = string.Empty;
            EmailTb.Text = string.Empty;
            PasswordRegistrationPb.Password = string.Empty;

            //// Создаем нового пользователя
            //Users newUser = new Users()
            //{

            //    full_name = FIOTb.Text,
            //    phone = PhoneTb.Text,
            //    email = EmailTb.Text,
            //    login = LoginRegistrationTb.Text,
            //    password = PasswordRegistrationPb.Password 
            //};
            //// Добавляем пользователя в контекст
            //App.context.Users.Add(newUser);
            //App.context.SaveChanges();
            //MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // После успешной регистрации открываем главное окно
            FrameWindow frameWindow = new FrameWindow();
            frameWindow.Show();
            // Закрываем окно регистрации
            this.Close();
        }
    }
}
