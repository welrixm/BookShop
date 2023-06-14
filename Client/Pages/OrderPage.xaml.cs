using Client.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
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
using System.Windows.Threading;
using Zen.Barcode;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Xml.Linq;

namespace Client.Pages
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

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

      
        public OrderPage()
        {
            DBConnect.db.Order.Load();
            Orders = DBConnect.db.Order.Local;
            InitializeComponent();
            ListOrder.ItemsSource = DBConnect.db.Order.Local.Where(x => x.UserId1 == Navigation.AuthUser.Id).ToList();
            
        }
        
        
        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
        }

        private void DepartamentBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new DepartmentPage()));
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

        private void BtnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Order;
            InvoiceWindow shipmentEditWindow = new InvoiceWindow(selBook);
            shipmentEditWindow.ShowDialog();
        }

        private void BtnSaveQR_Click(object sender, RoutedEventArgs e)
        {
            BarcodeWindow barcode = new BarcodeWindow();
            barcode.Show();
        }

       
    }
}
