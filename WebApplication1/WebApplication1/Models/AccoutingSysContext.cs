using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class AccoutingSysContext : DbContext
    {
        public AccoutingSysContext()
        {
        }

        public AccoutingSysContext(DbContextOptions<AccoutingSysContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceProduct> InvoiceProducts { get; set; } = null!;
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; } = null!;
        public virtual DbSet<PermissionUser> PermissionUsers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductStorage> ProductStorages { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Receipt> Receipts { get; set; } = null!;
        public virtual DbSet<ReceiptType> ReceiptTypes { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPermission> UserPermissions { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        public virtual DbSet<InvoiceProductView> InvoiceProductViews { get; set; } = null!;
        public virtual DbSet<InvoiceView> InvoiceViews { get; set; } = null!;

        public virtual DbSet<ProductStorageView> ProductStorageViews { get; set; } = null!;

        public virtual DbSet<ProductView> ProductViews { get; set; } = null!;

        public virtual DbSet<ReceiptView> ReceiptViews { get; set; } = null!;

        public virtual DbSet<UserView> UserViews { get; set; } = null!;
        public virtual DbSet<AccountView> AccountViews { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                

                optionsBuilder.UseSqlServer("Server=DESKTOP-1MAHDEJ; Database=AccoutingSys;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeId).HasColumnName("accountTypeID");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");

            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountFromId).HasColumnName("accountFromID");

                entity.Property(e => e.AccountToId).HasColumnName("accountToID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvoiceTypeId).HasColumnName("invoiceTypeID");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");


            });

            modelBuilder.Entity<InvoiceProduct>(entity =>
            {
                entity.ToTable("InvoiceProduct");

                entity.HasIndex(e => new { e.ProductId, e.InvoiceId }, "IX_InvoiceProduct")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.InvoiceId).HasColumnName("invoiceID");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.StoreId).HasColumnName("storeId");


            });

            modelBuilder.Entity<InvoiceType>(entity =>
            {
                entity.ToTable("InvoiceType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PermissionUser>(entity =>
            {
                entity.ToTable("PermissionUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermissionId).HasColumnName("permissionID");

                entity.Property(e => e.UserId).HasColumnName("userID");


            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductTypeId).HasColumnName("productTypeID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");


            });

            modelBuilder.Entity<ProductStorage>(entity =>
            {
                entity.ToTable("ProductStorage");

                entity.HasIndex(e => new { e.StoreId, e.ProductId }, "IX_ProductStorage")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");

            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountFromId).HasColumnName("accountFromID");

                entity.Property(e => e.AccountToId).HasColumnName("accountToID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReceiptTypeId).HasColumnName("receiptTypeID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");

            });

            modelBuilder.Entity<ReceiptType>(entity =>
            {
                entity.ToTable("ReceiptType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ename).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeID");

            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.ToTable("UserPermission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("userType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });


            modelBuilder.Entity<InvoiceProductView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("InvoiceProductView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InvoiceId).HasColumnName("invoiceID");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
            });
            modelBuilder.Entity<InvoiceView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("InvoiceView");

                entity.Property(e => e.AccountFromId).HasColumnName("accountFromID");

                entity.Property(e => e.AccountFromName)
                    .HasMaxLength(50)
                    .HasColumnName("accountFromName");

                entity.Property(e => e.AccountToId).HasColumnName("accountToID");

                entity.Property(e => e.AccountToName)
                    .HasMaxLength(50)
                    .HasColumnName("accountToName");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InvoiceTypeId).HasColumnName("invoiceTypeID");

                entity.Property(e => e.InvoiceTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("invoiceTypeName");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");
            });


            modelBuilder.Entity<InvoiceProductView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("InvoiceProductView");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InvoiceId).HasColumnName("invoiceID");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.ProductImage)
                    .HasColumnType("image")
                    .HasColumnName("productImage");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.ProductTypeId).HasColumnName("productTypeID");

                entity.Property(e => e.ProductTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("productTypeName");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .HasColumnName("storeName");
            });

            modelBuilder.Entity<InvoiceView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("InvoiceView");

                entity.Property(e => e.AccountFromId).HasColumnName("accountFromID");

                entity.Property(e => e.AccountFromName)
                    .HasMaxLength(50)
                    .HasColumnName("accountFromName");

                entity.Property(e => e.AccountToId).HasColumnName("accountToID");

                entity.Property(e => e.AccountToName)
                    .HasMaxLength(50)
                    .HasColumnName("accountToName");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InvoiceTypeId).HasColumnName("invoiceTypeID");

                entity.Property(e => e.InvoiceTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("invoiceTypeName");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.UserStatus).HasColumnName("userStatus");

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeID");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("userTypeName");
            });

            modelBuilder.Entity<ProductStorageView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProductStorageView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductImage)
                    .HasColumnType("image")
                    .HasColumnName("productImage");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.ProductTypeId).HasColumnName("productTypeID");

                entity.Property(e => e.ProductTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("productTypeName");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .HasColumnName("storeName");
            });

            modelBuilder.Entity<ProductView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("productView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductTypeId).HasColumnName("productTypeID");

                entity.Property(e => e.ProductTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("productTypeName");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");
            });

            modelBuilder.Entity<ReceiptView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReceiptView");

                entity.Property(e => e.AccountFromId).HasColumnName("accountFromID");

                entity.Property(e => e.AccountFromName)
                    .HasMaxLength(50)
                    .HasColumnName("accountFromName");

                entity.Property(e => e.AccountToId).HasColumnName("accountToID");

                entity.Property(e => e.AccountToName)
                    .HasMaxLength(50)
                    .HasColumnName("accountToName");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ReceiptTypeId).HasColumnName("receiptTypeID");

                entity.Property(e => e.ReceiptTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("receiptTypeName")
                    .IsFixedLength();

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.UserStatus).HasColumnName("userStatus");

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeID");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("userTypeName");
            });

            modelBuilder.Entity<UserView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("userView");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.UserStatus).HasColumnName("userStatus");

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeID");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("userTypeName");
            });
            modelBuilder.Entity<AccountView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccountView");

                entity.Property(e => e.AccountTypeId).HasColumnName("accountTypeID");

                entity.Property(e => e.AccountTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("accountTypeName");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
