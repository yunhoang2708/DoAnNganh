namespace WebsiteBanGiayDep23.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Update01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "FullName");
        }
    }
}
