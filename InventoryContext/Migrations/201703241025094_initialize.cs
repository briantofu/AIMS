namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryItem",
                c => new
                    {
                        InventoryItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(maxLength: 150),
                        UnitOfMeasurementId = c.Int(),
                        Location = c.Int(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryItemId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RequiredDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SpecialInstruction = c.String(),
                        RequisitionType = c.String(),
                        ReasonForDeclined = c.String(),
                        Status = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId);
            
            CreateTable(
                "dbo.RequestItem",
                c => new
                    {
                        RequestItemId = c.Int(nullable: false, identity: true),
                        RequestId = c.Int(nullable: false),
                        InventoryItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestItemId);
            
            CreateTable(
                "dbo.Requisition",
                c => new
                    {
                        RequisitionId = c.Int(nullable: false, identity: true),
                        RequisitionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RequiredDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SpecialInstruction = c.String(),
                        RequisitionType = c.String(),
                        ReasonForDeclined = c.String(),
                        LocationId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        UserId = c.Int(),
                        Status = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequisitionId);
            
            CreateTable(
                "dbo.RequisitionItem",
                c => new
                    {
                        RequisitionItemId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        InventoryItemId = c.Int(nullable: false),
                        RequisitionId = c.Int(nullable: false),
                        Description = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequisitionItemId);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(maxLength: 150),
                        Address = c.String(),
                        ContactPerson = c.String(maxLength: 150),
                        ContactNo = c.String(maxLength: 150),
                        Email = c.String(maxLength: 150),
                        TinNumber = c.String(maxLength: 150),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.SupplierInventoryItem",
                c => new
                    {
                        SupplierItemId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierItemId);
            
            CreateTable(
                "dbo.UnitOfMeasurement",
                c => new
                    {
                        UnitOfMeasurementId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UnitOfMeasurementId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UnitOfMeasurement");
            DropTable("dbo.SupplierInventoryItem");
            DropTable("dbo.Supplier");
            DropTable("dbo.RequisitionItem");
            DropTable("dbo.Requisition");
            DropTable("dbo.RequestItem");
            DropTable("dbo.Request");
            DropTable("dbo.Location");
            DropTable("dbo.InventoryItem");
        }
    }
}
