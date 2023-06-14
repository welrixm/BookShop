using Provider.Components;
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

namespace Provider.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShipmentEditWindow.xaml
    /// </summary>
    public partial class ShipmentEditWindow : Window
    {
        Components.Shipment shipment;
        public ShipmentEditWindow(Components.Shipment _shipment)
        {
            InitializeComponent();
            shipment = _shipment;
            DataContext = shipment;
            StageCbx.ItemsSource = DBConnect.db.State.ToList();
            StageCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (shipment.Id == 0)
            {
                DBConnect.db.Shipment.Add(shipment);
            }


            DBConnect.db.SaveChanges();
            MessageBox.Show("Успешно сохранено!");
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
