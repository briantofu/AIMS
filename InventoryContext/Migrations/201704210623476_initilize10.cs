namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initilize10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionItem", "PurchaseOrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionItem", "PurchaseOrderId");
        }
    }
}
