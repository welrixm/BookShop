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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        Book_shopEntities _context = new Book_shopEntities();
        public HistoryPage()
        {
            InitializeComponent();
            List<StateMessage> listGenres = _context.StateMessage.ToList();
            listGenres.Insert(0, new StateMessage { Name = "Все статусы" });
            CbFiltration.ItemsSource = listGenres;
            CbFiltration.SelectedIndex = 0;
            RefreshData();
        }
        private void RefreshData()
        {
            List<Message> listBooks = _context.Message.ToList();
            if (CbFiltration.SelectedIndex != 0)
            {
                StateMessage selectedGenre = (StateMessage)CbFiltration.SelectedItem;
                listBooks = listBooks.Where(x => x.StateMessageId == selectedGenre.Id).ToList();
            }

            if (CbSort.SelectedItem != null)
            {
                switch ((CbSort.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":

                        listBooks = _context.Message.ToList();
                        break;
                    case "2":

                        listBooks = listBooks.OrderBy(x => x.Name).ToList();
                        break;
                    case "3":

                        listBooks = listBooks.OrderByDescending(x => x.Name).ToList();

                        break;
                    case "4":

                        listBooks = listBooks.OrderBy(x => x.DateMessage).ToList();
                        break;
                    case "5":

                        listBooks = listBooks.OrderByDescending(x => x.DateMessage).ToList();
                        break;

                }

            }
            var searchString = SearchTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listBooks = listBooks.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
           
            HistoryList.ItemsSource = listBooks;
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new BookPage()));
        }

        private void StockBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new StockPage()));
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new HistoryPage()));
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.isAuth = false;
            Navigation.navs.Clear();
            Navigation.NextPage(new Nav("Авторизация", new AuthPage()));
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }

        private void CbFiltration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBConnect.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                HistoryList.ItemsSource = DBConnect.db.Message.ToList();
            }
        }

        private void MessageBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow message = new MessageWindow();
            message.Show();
        }
    }
}
