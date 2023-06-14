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
using System.Windows.Shapes;

namespace BookShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderEditWindow.xaml
    /// </summary>
    public partial class OrderEditWindow : Window
    {
        Components.Order order;
        public OrderEditWindow(Components.Order _order)
        {
            InitializeComponent();
            order = _order;
            DataContext = order;
            StageCbx.ItemsSource = DBConnect.db.ExecutionStage.ToList();
            StageCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (order.Id == 0)
            {
                DBConnect.db.Order.Add(order);
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
