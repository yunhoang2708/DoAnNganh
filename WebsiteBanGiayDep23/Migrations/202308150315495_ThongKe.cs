﻿namespace WebsiteBanGiayDep23.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ThongKe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongKes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ThoiGian = c.DateTime(nullable: false),
                    SoTruyCap = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.ThongKes");
        }
    }
}
