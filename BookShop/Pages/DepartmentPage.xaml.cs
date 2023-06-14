using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
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
    /// Логика взаимодействия для DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Page
    {
        private int CountEntriestOnPage = 4;

        public static DepartmentPage Instance { get; private set; }
        private IEnumerable<Department> TestingIEnumerableDepartments;
        public DepartmentPage()
        {
            DBConnect.db.Department.Load();
            Department = DBConnect.db.Department.Local;

            TestingIEnumerableDepartments = Department;
            NumberPage = 1;
            TotalNumberPage = 0;
            NumberEntriestOnOnePage = new List<int>();
            CallingMethodBeforeInitialization();
            InitializeComponent();
            Instance = this; 
        }

        private void CallingMethodBeforeInitialization()
        {
           
            ValidateTotalCountPage();
            PageProcessing();


        }
        private void ValidateTotalCountPage()
        {
            TotalNumberPage = (int)Math.Ceiling(Convert.ToDouble(TestingIEnumerableDepartments.Cast<Department>().Count()) / Convert.ToDouble(CountEntriestOnPage));
            ValidateNumberEntriestOnOnePage();
        }
        private void PageProcessing()
        {
            Department = TestingIEnumerableDepartments;

            Department = Department.Cast<Department>()
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
            var selDepartment = (sender as Button).DataContext as Department;
            Navigation.NextPage(new Nav("", new AddEditDepartmentPage(selDepartment)));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selDepartment = (sender as Button).DataContext as Department;
            if (MessageBox.Show("Вы точно хотите удалить эту запись ", "Уведомление ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                DBConnect.db.Department.Remove(selDepartment);
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
            Navigation.NextPage(new Nav("", new AddEditDepartmentPage(new Department())));
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
