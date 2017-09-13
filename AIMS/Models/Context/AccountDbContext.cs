using AIMS.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AIMS.Models.Context
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext() : base("AccountConnectionString")
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

        public class DBInitializer : CreateDatabaseIfNotExists<AccountDbContext>
        {
            public DBInitializer()
            {
            }
        }
        public DbSet<TableUser> TblUser { get; set; } //CREATE TBL USER
        public DbSet<TableRole> TblRole { get; set; } //CREATE TBL ROLE
        public DbSet<TableUserRole> TblUserRole { get; set; } //CREATE TBL USERROLE
    }
}