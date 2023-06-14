using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Components
{
    partial class Shipment
    {
        public ObservableCollection<BookShipment> BookShipments
        {
            get
            {
                return new ObservableCollection<BookShipment>(BookShipment);
            }
        }
        public int? Quantity
        {
            get
            {
                return this.BookShipment.Sum(x => x.Quantity);
            }
        }
        public decimal? TotalPrice
        {
            get
            {
                return this.BookShipment.Sum(x => x.Quantity * x.Book.Price);
            }
        }
    }
}
