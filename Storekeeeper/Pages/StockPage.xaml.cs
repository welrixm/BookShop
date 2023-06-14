using Storekeeeper.Components;
using System;
using System.Collections.Generic;
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

namespace Storekeeeper.Pages
{
    /// <summary>
    /// Логика взаимодействия для StockPage.xaml
    /// </summary>
    public partial class StockPage : Page
    {
        private int CountEntriestOnPage = 4;

        public int CountBooks
        {
            get
            {
                return (int)GetValue(CountBooksProperty);
            }
            set
            {
                SetValue(CountBooksProperty, value);
            }
        }
        public static readonly DependencyProperty CountBooksProperty = DependencyProperty.Register("CountBooks", typeof(int), typeof(StockPage));

        public decimal AllBooks
        {
            get
            {
                return (decimal)GetValue(AllBooksProperty);

            }
            set
            {
                SetValue(AllBooksProperty, value);
            }
        }
        public static readonly DependencyProperty AllBooksProperty = DependencyProperty.Register("AllBooks", typeof(decimal), typeof(StockPage));


        public static StockPage Instance { get; private set; }
        private IEnumerable<Stock> TestingIEnumerableStocks;
        public StockPage()
        {
            DBConnect.db.Stock.Load();
            Stock = DBConnect.db.Stock.Local;
            TestingIEnumerableStocks = Stock;
            NumberPage = 1;
            TotalNumberPage = 0;
            NumberEntriestOnOnePage = new List<int>();
            CallingMethodBeforeInitialization();
            InitializeComponent();
            Instance = this;
        }

        public void UpdateAllIngredient()
            => AllBooks = (decimal)DBConnect.db.Stock.Local.Sum(x => x.PriceOfPurchase);

        public void UpdateCountIngredient()
            => CountBooks = TestingIEnumerableStocks.Count();

        public void RefreshCounters()
        {
            UpdateAllIngredient();
            UpdateCountIngredient();
        }
        private void CallingMethodBeforeInitialization()
        {
            ValidateCountBooks();
            ValidateTotalCountPage();
            PageProcessing();


        }

        private void ValidateCountBooks()
        {
            CountBooks = TestingIEnumerableStocks.Count();

        }

        private void ValidateTotalCountPage()
        {
            TotalNumberPage = (int)Math.Ceiling(Convert.ToDouble(TestingIEnumerableStocks.Cast<Stock>().Count()) / Convert.ToDouble(CountEntriestOnPage));
            ValidateNumberEntriestOnOnePage();
        }

        private void PageProcessing()
        {
            Stock = TestingIEnumerableStocks;

            Stock = Stock.Cast<Stock>()
                                   .Skip((NumberPage - 1) * CountEntriestOnPage)
                                   .Take(CountEntriestOnPage);
        }

        private void ValidateNumberEntriestOnOnePage()
        {
            int n = 4, sum = 0;

            if (NumberEntriestOnOnePage.Count() == 0)
                for (int i = 2; i <= n; i++)
                {
                    sum += i;
                    NumberEntriestOnOnePage.Add(sum);
                }
        }

        private void ValidateCountEntriestOnPage()
        {
            if (CbCount.SelectedItem == null)
                return;

            CountEntriestOnPage = (int)CbCount.SelectedItem;
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

        private void CbCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateCountEntriestOnPage();
            ValidateTotalCountPage();

            if (NumberPage >= TotalNumberPage)
                NumberPage = TotalNumberPage;

            PageProcessing();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selStock = (sender as Button).DataContext as Stock;
            Navigation.NextPage(new Nav("", new AddEditStockPage(selStock)));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selStock = (sender as Button).DataContext as Stock;
            if (MessageBox.Show("Вы точно хотите удалить эту запись ", "Уведомление ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
               
                DBConnect.db.Stock.Remove(selStock);
                MessageBox.Show("Запись удалена");
                DBConnect.db.SaveChanges();
                
            }
            NumberPage = 1;
            ValidateTotalCountPage();
            PageProcessing();
        }

        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 1;

            PageProcessing();
        }

        private void BtnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if ((NumberPage - 1) <= 0)
                return;

            NumberPage--;

            PageProcessing();
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (TotalNumberPage <= NumberPage)
                return;

            NumberPage++;

            PageProcessing();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = TotalNumberPage;

            PageProcessing();
        }

        private void BorderPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Navigation.NextPage(new Nav("", new AddEditStockPage(new Stock())));
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
        }

        private void MessageBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow message = new MessageWindow();
            message.Show();
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new HistoryPage()));
        }
    }
}
