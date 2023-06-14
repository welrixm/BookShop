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
using BookShop.Components;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace BookShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditEmployeePage.xaml
    /// </summary>
    public partial class AddEditEmployeePage : Page
    {
        Components.User user;
        public AddEditEmployeePage(Components.User _user)
        {
            InitializeComponent();
           
            user = _user;
            DataContext = user;
            RoleCbx.ItemsSource = DBConnect.db.Role.Where(x => x.Id != 2).ToList();
            RoleCbx.DisplayMemberPath = "Name";
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
        }

        private void DepartamentBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new DepartmentPage()));
        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new EmployeePage()));
        }

        private void StockBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new StockPage()));
        }

        private void TicketBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new OrderPage()));
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.isAuth = false;
            Navigation.navs.Clear();
            Navigation.NextPage(new Nav("Авторизация", new AuthPage()));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneTbx.Text.Length > 0 && EmailTbx.Text.Length > 0)
            {
                string email = EmailTbx.Text;
                string phone = PhoneTbx.Text;
                if (Regex.IsMatch(phone, @"^((\+?7|8)[ -]?)?([(]?\d[- ]?[()]?){10}$") && Regex.IsMatch(phone, @"^(\+?7|8)?[\s(-]?[(-]?\d{3,4}[)-]?[ )-]?\d{2,7}[ -]?\d{2,4}[ -]?\d{0,2}$") && Regex.IsMatch(email, @"^[\w.-]+@\w+\.\w+$"))
                {

                    
                    if (user.Id == 0)
                    {
                        DBConnect.db.User.Add(user);
                        

                    }
                    if (MessageBox.Show("Вы точно хотите сохранить", "Уведомления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        DBConnect.db.SaveChanges();
                        MessageBox.Show("Пользователь успешно добавлен");
                        Navigation.BackPage();
                    }



                }
                else
                {
                    MessageBox.Show($" Проверьте почту {email} или телефон {phone} на корректность");
                    EmailTbx.Clear();
                    PhoneTbx.Clear();
                }
            }
            else MessageBox.Show("Заполните почту или телефон");
        }
       

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }

        private void ShipBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new ShipmentPage()));
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new HistoryPage()));
        }
    }
}
