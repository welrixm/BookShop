using Client.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Pages
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
            DependencyProperty.Register("Books", typeof(ObservableCollection<Book>), typeof(OrderPage));

        public ObservableCollection<Book> BooksAdd
        {
            get { return (ObservableCollection<Book>)GetValue(BooksAddOrderProperty); }
            set { SetValue(BooksAddOrderProperty, value); }
        }


        public static readonly DependencyProperty BooksAddOrderProperty =
            DependencyProperty.Register("BooksAdd", typeof(ObservableCollection<Book>), typeof(OrderPage));

        public CheckoutPage()
        {
            DBConnect.db.Book.Load();
            Books = new ObservableCollection<Book>(DBConnect.db.Book.Local);
            BooksAdd = new ObservableCollection<Book>();
            InitializeComponent();
            List<Delivery> listDels = DBConnect.db.Delivery.ToList();
            listDels.Insert(0, new Delivery { Name = "Доставка" });
            DelCmb.ItemsSource = listDels;
            DelCmb.SelectedIndex = 0;
            RefreshData();
        }

        private void RefreshData()
        {

            List<Delivery> listBooks = DBConnect.db.Delivery.ToList();
            if (DelCmb.SelectedIndex != 0)
            {
                Delivery selectedGenre = (Delivery)DelCmb.SelectedItem;
                listBooks = listBooks.Where(x => x.Id == selectedGenre.Id).ToList();
            }
        }

        private void ButtonAddBookOrder_Click(object sender, RoutedEventArgs e)
        {
            
         
            Book book = BookList.SelectedItem as Book;
            var stock = DBConnect.db.Stock.FirstOrDefault(x => x.BookId == book.Id);
            if(stock != null)
            {
                Books.Remove(book);
                BooksAdd.Add(book);
            }
           else
            {
                MessageBox.Show("К сожалению, данный товар отсутствует на складе!");
            }
            
            
            
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            decimal totalCost = 0;

            foreach (var item in BooksAdd)
                totalCost += (decimal)item.Price;

            Order order = new Order()
            {

                UserId = 1,
                UserId1 = Navigation.AuthUser.Id,
                ExecutionStageId = 1,
                DateOfOrder = DateTime.Now,
                Delivery = (Delivery)DelCmb.SelectedItem,
                TotalCost = (int?)totalCost
            };

            DBConnect.db.Order.Add(order);

            int count = 0;

            foreach (var item in BooksAdd)


            {

                count = Convert.ToInt32(CountTb.Text);
                

                BookOrder book = new BookOrder()
                {

                    OrderId = order.Id,
                    Book = item,
                    Quantity = (int?)count,

                };

                DBConnect.db.BookOrder.Add(book);


                DBConnect.db.SaveChanges();

                var selStock = DBConnect.db.Stock.FirstOrDefault(x => x.BookId == book.Book.Id);
                selStock.NumberOfBooks -= count;
            }

            

            DBConnect.db.SaveChanges();

            Navigation.NextPage(new Nav("", new OrderPage()));
        }

        private void DelCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OtmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }
    }
}
