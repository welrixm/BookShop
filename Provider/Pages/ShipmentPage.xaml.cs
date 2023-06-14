using Provider.Components;
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

namespace Provider.Pages
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
            var user = Navigation.AuthUser.Id;
            if (user == 5)
            {
                ListShipment.ItemsSource = DBConnect.db.Shipment.Where(x => x.ProviderId == 1).ToList();
            }
            else ListShipment.ItemsSource = DBConnect.db.Shipment.Where(x => x.ProviderId == 2).ToList();

        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
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

      

        private void BtnEditShip_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Shipment;
            ShipmentEditWindow shipmentEditWindow = new ShipmentEditWindow(selBook);
            shipmentEditWindow.ShowDialog();
        }

        
    }
}
