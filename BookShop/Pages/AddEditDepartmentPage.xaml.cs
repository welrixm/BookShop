using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using BookShop.Components;

namespace BookShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditDepartmentPage.xaml
    /// </summary>
    public partial class AddEditDepartmentPage : Page
    {
        Components.Department department;
        public AddEditDepartmentPage(Components.Department _department)
        {
            InitializeComponent();
            department = _department;
            DataContext = department;
            NameBookCbx.ItemsSource = DBConnect.db.Book.Where(x => x.IsActive == true).ToList();
            NameBookCbx.DisplayMemberPath = "Name";
            CategoryCbx.ItemsSource = DBConnect.db.Genre.ToList();
            CategoryCbx.DisplayMemberPath = "Name";
           
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
        }

        private void DepartamentBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new DepartmentPage()));
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

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (department.Id == 0 )
            {
               
                    DBConnect.db.Department.Add(department);
                   
            }

            DBConnect.db.SaveChanges();
            MessageBox.Show("Успешно сохранено!");
            Navigation.BackPage();

        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new EmployeePage()));
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
