using BookShop.Components;
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

namespace BookShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            EmployeeLW.ItemsSource = DBConnect.db.User.ToList();
            List<Role> listrole = DBConnect.db.Role.ToList();
            listrole.Insert(0, new Role { Name = "Все роли" });
            RoleCb.ItemsSource = listrole.Where(x => x.Id != 2).ToList(); ;
            RoleCb.SelectedIndex = 0;
            
            Refresh();
        }

        private void Refresh()
        {
            IEnumerable<User> employeslist = DBConnect.db.User.ToList();
            if (RoleCb.SelectedIndex != 0 )
            {
                Role selectedrole = (Role)RoleCb.SelectedItem;
                employeslist = employeslist.Where(x => x.RoleId == selectedrole.Id).ToList();
            }

            if (FindTb == null)
                return;
            if (FindTb.Text.Length > 0)
            {
                employeslist = employeslist.Where(x => x.FirstName.StartsWith(FindTb.Text) || x.LastName.StartsWith(FindTb.Text) || x.Patronymic.StartsWith(FindTb.Text));
            }
            EmployeeLW.ItemsSource = employeslist.ToList();
        }

        private void DeletBtn_Click(object sender, RoutedEventArgs e)
        {
            var selUser = (sender as Button).DataContext as User;
            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var userToRemove = DBConnect.db.Order.Where(ios => ios.UserId == selUser.Id && ios.UserId1 == selUser.Id);
                DBConnect.db.Order.RemoveRange(userToRemove);

                DBConnect.db.User.Remove(selUser);
                DBConnect.db.SaveChanges();
                MessageBox.Show("Данные удалены");
                EmployeeLW.ItemsSource = DBConnect.db.User.ToList();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selEmployee = (sender as Button).DataContext as User;
            Navigation.NextPage(new Nav("", new AddEditEmployeePage(selEmployee)));
        }

        private void AddDtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new AddEditEmployeePage(new User())));
        }

        private void RoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void FindTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
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

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBConnect.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                EmployeeLW.ItemsSource = DBConnect.db.User.Where(x => x.Id != 2).ToList();
            }
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
