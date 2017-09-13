namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartialDelivery", "SupplierId", c => c.Int(nullable: false));
            AddColumn("dbo.PartialDeliveryItem", "PartialDeliveryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PartialDeliveryItem", "PartialDeliveryId");
            DropColumn("dbo.PartialDelivery", "SupplierId");
        }
    }
}
