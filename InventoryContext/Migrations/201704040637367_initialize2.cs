namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplier", "Vatable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplier", "Vatable");
        }
    }
}
