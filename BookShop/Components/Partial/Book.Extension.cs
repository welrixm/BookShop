using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Components
{
    partial class Book
    {
        public string ActiveText
        {
            get
            {
                if (IsActive == true)
                    return "Актуален";
                else
                    return "Нет в наличии";

            }
        }
        public string Color
        {
            get
            {
                if (IsActive == true)
                    return "#FDFDFD";
                else
                    return "#E7E3DF";

            }
        }
    }
}
