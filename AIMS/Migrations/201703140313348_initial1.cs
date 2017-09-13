namespace AIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableClients",
                c => new
                    {
                        ClientBaseID = c.Int(nullable: false, identity: true),
                        ClientID = c.String(),
                        ClientName = c.String(),
                        Address = c.String(),
                        ContactNo = c.String(),
                        TinNo = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientBaseID);
            
            CreateTable(
                "dbo.TableClientItems",
                c => new
                    {
                        ClientItemID = c.Int(nullable: false, identity: true),
                        ClientBaseID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ItemNo = c.Int(nullable: false),
                        Description = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientItemID);
            
            CreateTable(
                "dbo.TableInventoryItems",
                c => new
                    {
                        InventoryItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(maxLength: 150),
                        UnitOfMeasurementID = c.Int(),
                        Location = c.Int(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryItemID);
            
            CreateTable(
                "dbo.TableLocations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.TableRequests",
                c => new
                    {
                        RequestID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
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
                .PrimaryKey(t => t.RequestID);
            
            CreateTable(
                "dbo.TableRequestItems",
                c => new
                    {
                        RequestItemID = c.Int(nullable: false, identity: true),
                        RequestID = c.Int(nullable: false),
                        InventoryItemID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestItemID);
            
            CreateTable(
                "dbo.TableRequisitions",
                c => new
                    {
                        RequisitionID = c.Int(nullable: false, identity: true),
                        RequisitionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RequiredDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SpecialInstruction = c.String(),
                        RequisitionType = c.String(),
                        ReasonForDeclined = c.String(),
                        LocationID = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        UserID = c.Int(),
                        Status = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequisitionID);
            
            CreateTable(
                "dbo.TableRequisitionItems",
                c => new
                    {
                        RequisitionItemID = c.Int(nullable: false, identity: true),
                        SupplierID = c.Int(nullable: false),
                        InventoryItemID = c.Int(nullable: false),
                        RequisitionID = c.Int(nullable: false),
                        Description = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequisitionItemID);
            
            CreateTable(
                "dbo.TableSuppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.TableSupplierInventoryItems",
                c => new
                    {
                        SupplierItemID = c.Int(nullable: false, identity: true),
                        SupplierID = c.Int(nullable: false),
                        InventoryID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierItemID);
            
            CreateTable(
                "dbo.TableUnitOfMeasurements",
                c => new
                    {
                        UnitOfMeasurementID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UnitOfMeasurementID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TableUnitOfMeasurements");
            DropTable("dbo.TableSupplierInventoryItems");
            DropTable("dbo.TableSuppliers");
            DropTable("dbo.TableRequisitionItems");
            DropTable("dbo.TableRequisitions");
            DropTable("dbo.TableRequestItems");
            DropTable("dbo.TableRequests");
            DropTable("dbo.TableLocations");
            DropTable("dbo.TableInventoryItems");
            DropTable("dbo.TableClientItems");
            DropTable("dbo.TableClients");
        }
    }
}
