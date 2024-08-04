using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IMS.Models
{
    public class InventoryContext : DbContext
    {
        
        public InventoryContext() : base("name=InventoryContext")
        {
        }

        
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            // Product - Category relationship
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Product - Supplier relationship
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId);

            // Order - Customer relationship
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            // OrderDetail - Order relationship
            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            // OrderDetail - Product relationship
            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);

            // InventoryTransaction - Product relationship
            modelBuilder.Entity<InventoryTransaction>()
                .HasRequired(it => it.Product)
                .WithMany()
                .HasForeignKey(it => it.ProductId);

            // UserRole - User relationship
            modelBuilder.Entity<UserRole>()
                .HasRequired(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithRequired(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}