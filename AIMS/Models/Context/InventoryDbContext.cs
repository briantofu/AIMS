using AIMS.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AIMS.Models.Context
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext() : base("InventoryConnectionString")
        {
            Database.SetInitializer(new DBInitializer());

            if (Database.Exists())
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<InventoryDbContext, Migrations.Configuration>()); //mag eeror ang part na ito kung wala pa kyung migration
            }
            else
            {
                Database.SetInitializer(new DBInitializer());
            }
        }

        public class DBInitializer : CreateDatabaseIfNotExists<InventoryDbContext>
        {
            public DBInitializer()
            {

            }
        }
        public DbSet<TableInventoryItem> TblInventoryItem { get; set; } //CREATE TBL INVENTORYITEM
        public DbSet<TableLocation> TblLocation { get; set; } //CREATE TBL LOCATION
        public DbSet<TableRequest> TblRequest { get; set; } //CREATE TBL REQUEST
        public DbSet<TableRequestItem> TblRequestItem { get; set; } //CREATE TBL REQUESTITEM
        public DbSet<TableRequisition> TblRequisition { get; set; } //CREATE TBL REQUISITION
        public DbSet<TableRequisitionItem> TblRequisitionItem { get; set; }//CREATE TBL REQUISITIONITEM
        public DbSet<TableSupplier> TblSupplier { get; set; } //CREATE TBL REQUESTITEM
        public DbSet<TableSupplierInventoryItem> TblSupplierInventoryItem { get; set; } //CREATE TBL REQUESTITEM
        public DbSet<TableUnitOfMeasurement> TblUnitOfMeasurement { get; set; } //CREATE TBL REQUESTITEM
        public DbSet<TableClient> TblClient { get; set; } //CREATE TBL CLIENT
        public DbSet<TableClientItem> TblClientItem { get; set; } //CREATE TBL CLIENTITEM
        public DbSet<TableInvoice> TblInvoice { get; set; } //CREATE TBL CLIENTITEM

    }
}