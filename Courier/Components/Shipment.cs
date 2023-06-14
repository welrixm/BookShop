//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Courier.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipment()
        {
            this.BookShipment = new HashSet<BookShipment>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<int> ProviderId { get; set; }
        public Nullable<int> StateId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookShipment> BookShipment { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual State State { get; set; }
    }
}
