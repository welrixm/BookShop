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
    /// Логика взаимодействия для ShipmentPage.xaml
    /// </summary>
    public partial class ShipmentPage : Page
    {
        public ObservableCollection<Shipment> Shipments
        {
            get { return (ObservableCollection<Shipment>)GetValue(ShipmentsProperty); }
            set { SetValue(ShipmentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipmentsProperty =
            DependencyProperty.Register("Shipments", typeof(ObservableCollection<Shipment>), typeof(ShipmentPage));

        public ShipmentPage()
        {
            DBConnect.db.Shipment.Load();
            Shipments = DBConnect.db.Shipment.Local;
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

        private void ShipBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new ShipmentPage()));
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.isAuth = false;
            Navigation.navs.Clear();
            Navigation.NextPage(new Nav("Авторизация", new AuthPage()));
        }

        private void BtnDeleteShip_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Shipment;
            var state = selBook.State;
            if (MessageBox.Show("Вы точно хотите отменить поставку?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(state.Id == 1)
                {
                    var bookshipmentToRemove = DBConnect.db.BookShipment.Where(ios => ios.ShipmentId == selBook.Id);
                    DBConnect.db.BookShipment.RemoveRange(bookshipmentToRemove);

                    DBConnect.db.Shipment.Remove(selBook);
                    DBConnect.db.SaveChanges();
                    MessageBox.Show("Поставка отменена");
                }
                else
                {
                    MessageBox.Show("К сожалению, поставка не может быть отменена!");
                }
                ListShipment.ItemsSource = DBConnect.db.Shipment.ToList();
            }
        }

        private void BtnShip_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new CheckoutPage()));
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new HistoryPage()));
        }
    }
}
