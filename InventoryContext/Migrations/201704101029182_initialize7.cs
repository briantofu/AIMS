namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartialDelivery", "DeliveryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PartialDelivery", "DeliveryDate");
        }
    }
}
