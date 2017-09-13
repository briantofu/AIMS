namespace InventoryContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Location", "LocationAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Location", "LocationAddress");
        }
    }
}
