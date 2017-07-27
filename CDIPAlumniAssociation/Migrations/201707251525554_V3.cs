namespace CDIPAlumniAssociation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobInfoes", "CompanyName", c => c.String(nullable: false));
            AddColumn("dbo.JobInfoes", "CompanyAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobInfoes", "CompanyAddress");
            DropColumn("dbo.JobInfoes", "CompanyName");
        }
    }
}
