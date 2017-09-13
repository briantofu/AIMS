namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PartialDeliveryItem",
                c => new
                    {
                        PartialDeliveryItemId = c.Int(nullable: false, identity: true),
                        DeliveredQuantity = c.Int(nullable: false),
                        RequisitionItemId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PartialDeliveryItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PartialDeliveryItem");
        }
    }
}
