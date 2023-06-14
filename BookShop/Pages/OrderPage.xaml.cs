using BookShop.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {

        public ObservableCollection<Order> Orders
        {
            get { return (ObservableCollection<Order>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<Order>), typeof(OrderPage));

        public OrderPage()
        {
            DBConnect.db.Order.Load();
            Orders = DBConnect.db.Order.Local;
            InitializeComponent();
            
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

        private void BtnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Order;
            var stage = selBook.ExecutionStage;
            if (MessageBox.Show("Вы точно хотите отменить заказ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(stage.Id == 1)
                {
                    var bookorderToRemove = DBConnect.db.BookOrder.Where(ios => ios.OrderId == selBook.Id);
                    DBConnect.db.BookOrder.RemoveRange(bookorderToRemove);

                    DBConnect.db.Order.Remove(selBook);
                    DBConnect.db.SaveChanges();
                    MessageBox.Show("Заказ отменен");
                }
                else
                {
                    MessageBox.Show("К сожалению, заказ не может быть отменен!");
                }
                ListOrder.ItemsSource = DBConnect.db.Order.ToList();
            }
        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new EmployeePage()));
        }

        private void ShipBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new ShipmentPage()));
        }

        private void BtnFixOrder_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Order;
            OrderEditWindow orderEditWindow = new OrderEditWindow(selBook);
            orderEditWindow.ShowDialog();
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new HistoryPage()));
        }
    }
}
