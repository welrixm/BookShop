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

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        Components.Order bookOrder;
        public InvoiceWindow(Components.Order _bookOrder)
        {
            InitializeComponent();
            bookOrder = _bookOrder;
            DataContext = bookOrder;
        }

        private void PrintBnt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if(printDialog.ShowDialog() ==  true )
                {
                    printDialog.PrintVisual(print, "Invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
