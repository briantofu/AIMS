namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartialDelivery", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PartialDelivery", "Status");
        }
    }
}
