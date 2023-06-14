using Courier.Components;
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

namespace Courier.Pages
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
            StageCbx.ItemsSource = DBConnect.db.ExecutionStage.Where(x=> x.Id != 1 && x.Id != 2&& x.Id != 3&& x.Id != 4&& x.Id != 5).ToList();
            StageCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(order.Id == 0)
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
