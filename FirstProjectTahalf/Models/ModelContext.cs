using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }
        public virtual DbSet<ProjectCategoryStore> ProjectCategoryStores { get; set; }
        public virtual DbSet<ProjectCreditcard> ProjectCreditcards { get; set; }
        public virtual DbSet<ProjectHome> ProjectHomes { get; set; }
        public virtual DbSet<ProjectOrder> ProjectOrders { get; set; }
        public virtual DbSet<ProjectPayment> ProjectPayments { get; set; }
        public virtual DbSet<ProjectProduct> ProjectProducts { get; set; }
        public virtual DbSet<ProjectProductOrder> ProjectProductOrders { get; set; }
        public virtual DbSet<ProjectRole> ProjectRoles { get; set; }
        public virtual DbSet<ProjectStore> ProjectStores { get; set; }
        public virtual DbSet<ProjectTestimonial> ProjectTestimonials { get; set; }
        public virtual DbSet<ProjectUser> ProjectUsers { get; set; }
        public virtual DbSet<Proudct> Proudcts { get; set; }
        public virtual DbSet<ProudctCustomer> ProudctCustomers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_User135;PASSWORD=Deiaa@Tahalf;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER135")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMER");

                entity.Property(e => e.Customerid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CUSTOMERID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");
            });

            modelBuilder.Entity<ProjectCategory>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("SYS_C00221236");

                entity.ToTable("PROJECT_CATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Name)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<ProjectCategoryStore>(entity =>
            {
                entity.HasKey(e => e.CategoryStore)
                    .HasName("SYS_C00221281");

                entity.ToTable("PROJECT_CATEGORY_STORE");

                entity.Property(e => e.CategoryStore)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORY_STORE");

                entity.Property(e => e.Catid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATID");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STOREID");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.ProjectCategoryStores)
                    .HasForeignKey(d => d.Catid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00221283");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProjectCategoryStores)
                    .HasForeignKey(d => d.Storeid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00221282");
            });

            modelBuilder.Entity<ProjectCreditcard>(entity =>
            {
                entity.HasKey(e => e.Creditcardid)
                    .HasName("SYS_C00224957");

                entity.ToTable("PROJECT_CREDITCARD");

                entity.Property(e => e.Creditcardid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CREDITCARDID");

                entity.Property(e => e.Balance)
                    .HasColumnType("FLOAT")
                    .HasColumnName("BALANCE")
                    .HasDefaultValueSql("(0)\n  ");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectCreditcards)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("ACCOUNT_CREDITCARD_FK");
            });

            modelBuilder.Entity<ProjectHome>(entity =>
            {
                entity.HasKey(e => e.Homeid)
                    .HasName("SYS_C00221246");

                entity.ToTable("PROJECT_HOME");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOMEID");

                entity.Property(e => e.Address)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Freeshippinglimit)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("FREESHIPPINGLIMIT");

                entity.Property(e => e.Homeimg)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("HOMEIMG");

                entity.Property(e => e.Logoimg)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("LOGOIMG");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Supportworktime)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("SUPPORTWORKTIME");
            });

            modelBuilder.Entity<ProjectOrder>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("SYS_C00221258");

                entity.ToTable("PROJECT_ORDER");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Datee)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEE");

                entity.Property(e => e.Total)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("TOTAL");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectOrders)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00221259");
            });

            modelBuilder.Entity<ProjectPayment>(entity =>
            {
                entity.HasKey(e => e.Paymentid)
                    .HasName("SYS_C00221302");

                entity.ToTable("PROJECT_PAYMENT");

                entity.Property(e => e.Paymentid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYMENTID");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Status)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STATUS");

                entity.Property(e => e.Datee)
                   .HasColumnType("DATE")
                   .HasColumnName("DATEE");

                entity.Property(e => e.Total)
                   .HasColumnType("NUMBER(38)")
                   .HasColumnName("TOTAL");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProjectPayments)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("SYS_C00221303");
            });

            modelBuilder.Entity<ProjectProduct>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("SYS_C00221266");

                entity.ToTable("PROJECT_PRODUCT");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Discount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("DISCOUNT");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Productdescription)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTDESCRIPTION");

                entity.Property(e => e.Productname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Propricse)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PROPRICSE");

                entity.Property(e => e.Prosale)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PROSALE");

                entity.Property(e => e.Quntitiy)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("QUNTITIY");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STOREID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProjectProducts)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00224921");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProjectProducts)
                    .HasForeignKey(d => d.Storeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00224918");
            });

            modelBuilder.Entity<ProjectProductOrder>(entity =>
            {
                entity.HasKey(e => e.Productorderid)
                    .HasName("SYS_C00221287");

                entity.ToTable("PROJECT_PRODUCT_ORDER");

                entity.Property(e => e.Productorderid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTORDERID");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Proid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PROID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProjectProductOrders)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("SYS_C00221289");

                entity.HasOne(d => d.Pro)
                    .WithMany(p => p.ProjectProductOrders)
                    .HasForeignKey(d => d.Proid)
                    .HasConstraintName("SYS_C00221288");
            });

            modelBuilder.Entity<ProjectRole>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("SYS_C00221237");

                entity.ToTable("PROJECT_ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Roletype)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("ROLETYPE");
            });

            modelBuilder.Entity<ProjectStore>(entity =>
            {
                entity.HasKey(e => e.Storeid)
                    .HasName("SYS_C00221244");

                entity.ToTable("PROJECT_STORE");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STOREID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Namestore)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("NAMESTORE");

                entity.Property(e => e.Opentime)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("OPENTIME");

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
            });

            modelBuilder.Entity<ProjectTestimonial>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("SYS_C00221296");

                entity.ToTable("PROJECT_TESTIMONIAL");

                entity.Property(e => e.TestId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TEST_ID");

                entity.Property(e => e.Message)
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Publishdate)
                    .HasColumnType("DATE")
                    .HasColumnName("PUBLISHDATE");

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasMaxLength(222)
                    .IsUnicode(false)
                    .HasColumnName("RATING");

                entity.Property(e => e.Status)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProjectTestimonials)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("SYS_C00221298");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectTestimonials)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("SYS_C00221297");
            });

            modelBuilder.Entity<ProjectUser>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("SYS_C00221253");

                entity.ToTable("PROJECT_USER");

                entity.HasIndex(e => e.Email, "SYS_C00221254")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("HOMEID");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Home)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.Homeid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00221255");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00221256");
            });

            modelBuilder.Entity<Proudct>(entity =>
            {
                entity.ToTable("PROUDCT");

                entity.Property(e => e.Proudctid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PROUDCTID");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Sale)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SALE");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Proudcts)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK_CATEGORYID");
            });

            modelBuilder.Entity<ProudctCustomer>(entity =>
            {
                entity.HasKey(e => e.PcId)
                    .HasName("PK_PC_ID");

                entity.ToTable("PROUDCT_CUSTOMER");

                entity.Property(e => e.PcId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PC_ID");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.DateForm)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_FORM");

                entity.Property(e => e.DateTo)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_TO");

                entity.Property(e => e.ProudctId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROUDCT_ID");

                entity.Property(e => e.Quaninty)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANINTY");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ProudctCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CUSTOMER_ID");

                entity.HasOne(d => d.Proudct)
                    .WithMany(p => p.ProudctCustomers)
                    .HasForeignKey(d => d.ProudctId)
                    .HasConstraintName("FK_PROUDCT_ID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_USER_ID");

                entity.ToTable("USER_LOG");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Passowrd)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PASSOWRD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.UserLogs)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CUSTOMER_ID2");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserLogs)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_ROLEID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
