namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContactId = c.Guid(),
                        CreatedByUserId = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedByUserId = c.Guid(),
                        UpdatedDateTime = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        I = c.String(maxLength: 255),
                        O = c.String(maxLength: 255),
                        F = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        CreatedByUserId = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedByUserId = c.Guid(),
                        UpdatedDateTime = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.String(maxLength: 50),
                        CreatedByUserId = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedByUserId = c.Guid(),
                        UpdatedDateTime = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(),
                        ClientId = c.Guid(),
                        ProductId = c.Guid(),
                        Sum = c.String(maxLength: 50),
                        CreatedByUserId = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedByUserId = c.Guid(),
                        UpdatedDateTime = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContactId = c.Guid(),
                        CreatedByUserId = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedByUserId = c.Guid(),
                        UpdatedDateTime = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Managers");
            DropTable("dbo.Sales");
            DropTable("dbo.Products");
            DropTable("dbo.Contacts");
            DropTable("dbo.Clients");
        }
    }
}
