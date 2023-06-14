using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Components
{
    partial class Order
    {
        public ObservableCollection<BookOrder> BookOrders
        {
            get
            {
                return new ObservableCollection<BookOrder>(BookOrder);
            }
        }
        public int? Quantity
        {
            get
            {
                return this.BookOrder.Sum(x => x.Quantity);
            }
        }
        public decimal? TotalPrice
        {
            get
            {
                return this.BookOrder.Sum(x => x.Quantity * x.Book.Price);
            }
        }
    }
}
