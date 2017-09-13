namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initilize11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchasingOrder", "DeliveryCharges", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchasingOrder", "DeliveryCharges");
        }
    }
}
