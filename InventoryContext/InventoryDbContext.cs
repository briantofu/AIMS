using InventoryEntity;
using System.Data.Entity;

namespace InventoryContext
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
        public DbSet<EInventoryItem> InventoryItem { get; set; } //CREATE TBL INVENTORYITEM
        public DbSet<ELocation> Location { get; set; } //CREATE TBL LOCATION
        public DbSet<ERequest> Request { get; set; } //CREATE TBL REQUEST
        public DbSet<ERequestItem> RequestItem { get; set; } //CREATE TBL REQUESTITEM
        public DbSet<ERequisition> Requisition { get; set; } //CREATE TBL REQUISITION
        public DbSet<ERequisitionItem> RequisitionItem { get; set; }//CREATE TBL REQUISITIONITEM
        public DbSet<ESupplier> Supplier { get; set; } //CREATE TBL REQUESTITEM
        public DbSet<ESupplierInventoryItem> SupplierInventoryItem { get; set; } //CREATE TBL REQUESTITEM

        public DbSet<EUnitOfMeasurement> UnitOfMeasurement { get; set; } //CREATE TBL REQUESTITEM
        public DbSet<EPartialDeliveryItem> PartialDeliveryItem { get; set; } //CREATE TBL PARTIAL DELIVERY ITEM
        public DbSet<EPartialDelivery> PartialDelivery { get; set; } //CREATE TBL PARTIAL DELIVERY ITEM
        public DbSet<EPurchasingOrder> PurchasingOrder { get; set; } //CREATE TBL PURCHASING ORDER

        //public DbSet<EClient> TblClient { get; set; } //CREATE TBL CLIENT
        //public DbSet<EClientItem> TblClientItem { get; set; } //CREATE TBL CLIENTITEM
        //public DbSet<EInvoice> TblInvoice { get; set; } //CREATE TBL CLIENTITEM

    }
}
