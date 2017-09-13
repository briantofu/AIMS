namespace AIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TableInvoices", "ClientBaseID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TableInvoices", "ClientBaseID");
        }
    }
}
