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
    /// Логика взаимодействия для CheckoutPage.xaml
    /// </summary>
    public partial class CheckoutPage : Page
    {
        public ObservableCollection<Book> Books
        {
            get { return (ObservableCollection<Book>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Books", typeof(ObservableCollection<Book>), typeof(ShipmentPage));

        public ObservableCollection<Book> BooksAdd
        {
            get { return (ObservableCollection<Book>)GetValue(BooksAddOrderProperty); }
            set { SetValue(BooksAddOrderProperty, value); }
        }


        public static readonly DependencyProperty BooksAddOrderProperty =
            DependencyProperty.Register("BooksAdd", typeof(ObservableCollection<Book>), typeof(ShipmentPage));
        public CheckoutPage()
        {
            DBConnect.db.Book.Load();
            Books = new ObservableCollection<Book>(DBConnect.db.Book.Local);
            BooksAdd = new ObservableCollection<Book>();
            InitializeComponent();
            List<Provider> listProvs = DBConnect.db.Provider.ToList();
            listProvs.Insert(0, new Provider { Name = "Поставщик" });
            ProvCmb.ItemsSource = listProvs;
            ProvCmb.SelectedIndex = 0;
            RefreshData();
        }

        private void RefreshData()
        {

            List<Provider> listBooks = DBConnect.db.Provider.ToList();
            if (ProvCmb.SelectedIndex != 0)
            {
                Provider selectedGenre = (Provider)ProvCmb.SelectedItem;
                listBooks = listBooks.Where(x => x.Id == selectedGenre.Id).ToList();
            }
        }

        private void ButtonAddBookShipment_Click(object sender, RoutedEventArgs e)
        {
            Book book = BookList.SelectedItem as Book;

            Books.Remove(book);
            BooksAdd.Add(book);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            decimal purchasePrice = 0;

            foreach (var item in BooksAdd)
                purchasePrice += (decimal)item.Price;
            Shipment shipment = new Shipment()
            {

                StateId = 1,
                Date = DateTime.Now,
                Provider = (Provider)ProvCmb.SelectedItem,
                PurchasePrice = (int?)purchasePrice
            };

            DBConnect.db.Shipment.Add(shipment);
            DBConnect.db.SaveChanges();

            int count = 0;
            int price = 0;
            foreach (var item in BooksAdd)


            {
                count = Convert.ToInt32(CountTb.Text);
                BookShipment book = new BookShipment()
                {

                    ShipmentId = shipment.Id,
                    Book = item,
                    Quantity = (int?)count
                };

                DBConnect.db.BookShipment.Add(book);
                DBConnect.db.SaveChanges();

                
                var selStock = DBConnect.db.Stock.FirstOrDefault(x => x.BookId == book.Book.Id);
                
                if (selStock == null)
                {
                    price = Convert.ToInt32(count) * Convert.ToInt32(book.Book.Price);
                    
                    Stock stock = new Stock()
                    {
                        BookId = book.Book.Id,
                        NumberOfBooks = count,
                        DateOfReceipt = DateTime.Now,
                        PriceOfPurchase = price,
                        CostForOne = book.Book.Price
                    };
                   


                    DBConnect.db.Stock.Add(stock);
                    DBConnect.db.SaveChanges();
                   
                }
                else
                {
                    selStock.NumberOfBooks += count;
                }

                


            

            }
            DBConnect.db.SaveChanges();
            
            Navigation.NextPage(new Nav("", new ShipmentPage()));
        }

        private void ProvCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void OtmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }
    }
}
