namespace WebsiteBanGiayDep23.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Update_Table_Review_1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.tb_Review", "ProductId");
            AddForeignKey("dbo.tb_Review", "ProductId", "dbo.tb_Product", "ID", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.tb_Review", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_Review", new[] { "ProductId" });
        }
    }
}
