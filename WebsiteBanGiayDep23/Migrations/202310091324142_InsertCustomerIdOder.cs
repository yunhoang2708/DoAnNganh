﻿namespace WebsiteBanGiayDep23.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InsertCustomerIdOder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "CustomerId", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.tb_Order", "CustomerId");
        }
    }
}
