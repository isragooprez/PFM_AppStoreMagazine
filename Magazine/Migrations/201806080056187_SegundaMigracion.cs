namespace Magazine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegundaMigracion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MagazineModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MagazineModels", "UserId", c => c.Int(nullable: false));
        }
    }
}
