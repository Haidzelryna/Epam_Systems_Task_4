using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace Domain
{
    public class DataContext : DbContext
    {
        public static readonly string CONTEXTKEY = "context";

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        //protected readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public DataContext() : base("EntityContext")
        {
        //}

        //public DataContext() : base("DBConnection")
        //{
            //Database.Connection.ConnectionString = String.Concat("Data Source=DROGON\\SQLEXPRESS;Integrated Security=True;",
            //                                        "Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False;",
            //                                        "ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            
            Database.CreateIfNotExists();

            //string connectionString = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
            //Console.WriteLine(connectionString);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Manager>().ToTable("Managers");
            modelBuilder.Entity<Contact>().ToTable("Contacts");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Sale>().ToTable("Sales");
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
