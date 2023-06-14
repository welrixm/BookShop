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
    /// Логика взаимодействия для HistoryEditWindow.xaml
    /// </summary>
    public partial class HistoryEditWindow : Window
    {
        Components.Message message;
        public HistoryEditWindow(Components.Message _message)
        {
            InitializeComponent();
           message = _message;
            DataContext = message;
            StateMessageCbx.ItemsSource = DBConnect.db.StateMessage.ToList();
            StateMessageCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (message.Id == 0)
            {
                DBConnect.db.Message.Add(message);
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
