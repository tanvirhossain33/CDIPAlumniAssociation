namespace CDIPAlumniAssociation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.JobInfoes", "CompanyAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobInfoes", "CompanyAddress", c => c.String(nullable: false));
        }
    }
}
