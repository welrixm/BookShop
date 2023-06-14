using Storekeeeper.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Storekeeeper.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistPage.xaml
    /// </summary>
    public partial class RegistPage : Page
    {
        public RegistPage()
        {
            InitializeComponent();
        }

        private void FirstNameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void LastNameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PatronymicTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PhoneTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            string firstname = FirstNameTb.Text.Trim();
            string lastname = LastNameTb.Text.Trim();
            string patronymic = PatronymicTb.Text.Trim();
            string email = EmailTb.Text.Trim();
            string phone = PhoneTb.Text.Trim();
            if (login.Length > 0 && password.Length > 0)
            {
                if (Regex.IsMatch(phone, @"^((\+?7|8)[ -]?)?([(]?\d[- ]?[()]?){10}$") && Regex.IsMatch(phone, @"^(\+?7|8)?[\s(-]?[(-]?\d{3,4}[)-]?[ )-]?\d{2,7}[ -]?\d{2,4}[ -]?\d{0,2}$") && Regex.IsMatch(email, @"^[\w.-]+@\w+\.\w+$"))
                {
                    DBConnect.db.User.Add(new User
                    {
                        Login = login,
                        Password = password,
                        FirstName = firstname,
                        LastName = lastname,
                        Patronymic = patronymic,
                        Email = email,
                        RoleId = 2
                    });
                    DBConnect.db.SaveChanges();
                    MessageBox.Show("Вы зарегистрированы!");
                    Navigation.BackPage();
                }

                else
                {
                    MessageBox.Show($" Проверьте почту {email} или телефон {phone} на корректность");
                    EmailTb.Clear();
                    PhoneTb.Clear();
                }
            }
            else
            {
                MessageBox.Show("Пусто! Заполните поля.");
            }
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginTb.Text = "";
            PasswordTb.Password = "";
            FirstNameTb.Text = "";
            LastNameTb.Text = "";
            PatronymicTb.Text = "";
            EmailTb.Text = "";
            PhoneTb.Text = "";
        }

        private void OtmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }
    }
}
