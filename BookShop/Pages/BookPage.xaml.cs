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
using BookShop.Components;

namespace BookShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        Book_shopEntities3 _context = new Book_shopEntities3();

        public BookPage()
        {
            InitializeComponent();
            List<Genre> listGenres = _context.Genre.ToList();
            listGenres.Insert(0, new Genre { Name = "Все жанры" });
            CbFiltration.ItemsSource = listGenres;
            CbFiltration.SelectedIndex = 0;
            RefreshData();
        }
        private void RefreshData()
        {
            List<Book> listBooks = _context.Book.ToList();
            if (CbFiltration.SelectedIndex != 0)
            {
                Genre selectedGenre = (Genre)CbFiltration.SelectedItem;
                listBooks = listBooks.Where(x => x.GenreId == selectedGenre.Id).ToList();
            }
           
            if (CbSort.SelectedItem != null)
            {
                switch ((CbSort.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":

                        listBooks = _context.Book.ToList();
                        break;
                    case "2":

                        listBooks = listBooks.OrderBy(x => x.Name).ToList();
                        break;
                    case "3":

                        listBooks = listBooks.OrderByDescending(x => x.Name).ToList();

                        break;
                    case "4":

                        listBooks = listBooks.OrderBy(x => x.DateOfPublish).ToList();
                        break;
                    case "5":

                        listBooks = listBooks.OrderByDescending(x => x.DateOfPublish).ToList();
                        break;

                }

            }
            var searchString = SearchTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listBooks = listBooks.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
           
            BookList.ItemsSource = listBooks;
        }
        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
        }

        private void DepartamentBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new DepartmentPage()));
        }

        private void StockBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new StockPage()));
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

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Book;
            Navigation.NextPage(new Nav("", new AddEditPage(selBook)));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Book;
            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var inredientStagesToRemove = DBConnect.db.BookOrder.Where(ios => ios.BookId == selBook.Id);
                DBConnect.db.BookOrder.RemoveRange(inredientStagesToRemove);

                DBConnect.db.Book.Remove(selBook);
                DBConnect.db.SaveChanges();
                MessageBox.Show("Данные удалены");
                BookList.ItemsSource = DBConnect.db.Book.ToList();
            }
        }

        

        private void AddNewBookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new AddEditPage(new Book())));
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }

        private void CbFiltration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBConnect.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                BookList.ItemsSource = DBConnect.db.Book.ToList();
            }
        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new EmployeePage()));
        }

        private void ShipBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new ShipmentPage()));
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new HistoryPage()));
        }
    }
}
