namespace AIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableInvoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        InvoicePeriod = c.String(),
                        AccountName = c.String(),
                        USDAccountNo = c.String(),
                        BankName = c.String(),
                        BankAddress = c.String(),
                        SwiftCode = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TableInvoices");
        }
    }
}
