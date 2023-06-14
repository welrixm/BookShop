using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace BookShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        Components.Book book;
        public AddEditPage(Components.Book _book)
        {
            InitializeComponent();
            book = _book;
            DataContext = book;
            GenreCbx.ItemsSource = DBConnect.db.Genre.ToList();
            GenreCbx.DisplayMemberPath = "Name";
            PublishCbx.ItemsSource = DBConnect.db.Publish.ToList();
            PublishCbx.DisplayMemberPath = "Name";

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

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (book.Id == 0)
            {
                DBConnect.db.Book.Add(book);

            }

            DBConnect.db.SaveChanges();
            MessageBox.Show("Успешно сохранено!");
            Navigation.BackPage();
            
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                book.ImagePath = File.ReadAllBytes(openFileDialog.FileName);
                BookImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
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
