﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.Components
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Book_shopEntities3 : DbContext
    {
        public Book_shopEntities3()
            : base("name=Book_shopEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookOrder> BookOrder { get; set; }
        public virtual DbSet<BookShipment> BookShipment { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<ExecutionStage> ExecutionStage { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Publish> Publish { get; set; }
        public virtual DbSet<PurchaseReceipt> PurchaseReceipt { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<StateMessage> StateMessage { get; set; }
    }
}
