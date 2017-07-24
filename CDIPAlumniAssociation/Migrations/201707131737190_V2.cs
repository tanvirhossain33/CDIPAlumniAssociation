namespace CDIPAlumniAssociation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CurrentJobInfo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "CurrentJobInfo", c => c.String(nullable: false));
        }
    }
}
