namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initializa1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierInventoryItem", "UnitPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupplierInventoryItem", "UnitPrice");
        }
    }
}
