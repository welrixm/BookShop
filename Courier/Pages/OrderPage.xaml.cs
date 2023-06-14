using Courier.Components;
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

namespace Courier.Pages
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
                ListOrder.ItemsSource = DBConnect.db.Order.Where(x => x.DeliveryId == 3).ToList();
            }
        }

        private void BtnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Order;
            OrderEditWindow orderEditWindow = new OrderEditWindow(selBook);
            orderEditWindow.ShowDialog();
           
        }
    }
}
