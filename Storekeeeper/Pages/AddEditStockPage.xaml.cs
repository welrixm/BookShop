using Storekeeeper.Components;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Storekeeeper.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditStockPage.xaml
    /// </summary>
    public partial class AddEditStockPage : Page
    {
        Components.Stock stock;
        public AddEditStockPage(Components.Stock _stock)
        {
            InitializeComponent();
            stock = _stock;
            DataContext = stock;
            NameBookCbx.ItemsSource = DBConnect.db.Book.Where(x => x.IsActive == true).ToList();
            NameBookCbx.DisplayMemberPath = "Name";
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
        }

        private void StockBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new StockPage()));
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.isAuth = false;
            Navigation.navs.Clear();
            Navigation.NextPage(new Nav("Авторизация", new AuthPage()));
        }

        private void PriceTotalTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (QuanTbx.Text.Length > 0)
            {
                PriceTotalTbx.Text = (Convert.ToInt32(PriceOneTbx.Text) * Convert.ToInt32(QuanTbx.Text)).ToString();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (stock.Id == 0)
            {

                DBConnect.db.Stock.Add(stock);

            }

            DBConnect.db.SaveChanges();
            MessageBox.Show("Успешно сохранено!");
            Navigation.BackPage();
            StockPage.Instance.RefreshCounters();
        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
            StockPage.Instance.RefreshCounters();
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new HistoryPage()));
        }
    }
}
