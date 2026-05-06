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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            var user = App.context.Users.FirstOrDefault(u => u.login == LoginTb.Text && u.password == PasswordPb.Password);

            if (user != null)
            {
                App.currentUser = user;

                if (user.Roles.Name == "Администратор")
                {
                    //new AdminWindow().Show();
                }
                else
                {
                    new FrameWindow().Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }


        }


        private void RegistrationrBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();
            this.Close();
        }

    }
}
