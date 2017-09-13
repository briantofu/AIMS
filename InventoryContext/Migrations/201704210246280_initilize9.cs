namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initilize9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchasingOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        RequisitionId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseOrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PurchasingOrder");
        }
    }
}
