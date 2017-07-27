namespace CDIPAlumniAssociation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobInfoes", "JobRequirement", c => c.String(nullable: false));
            DropColumn("dbo.JobInfoes", "AdditionalRequirement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobInfoes", "AdditionalRequirement", c => c.String(nullable: false));
            DropColumn("dbo.JobInfoes", "JobRequirement");
        }
    }
}
