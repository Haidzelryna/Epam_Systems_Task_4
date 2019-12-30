using System.Data.Entity;

namespace Domain
{
    public class DataContext : DbContext
    {
        public static readonly string ContextKey = "context";

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        //protected readonly IUserService userService;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="Context"/> class.
        ///// </summary>
        //public DataContext() : base("EntityContext")
        //{
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("UserLists");

            modelBuilder.Entity<Contact>().ToTable("Contacts");

            modelBuilder.Entity<User>().ToTable("Users");
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
