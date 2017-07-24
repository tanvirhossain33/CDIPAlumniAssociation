namespace CDIPAlumniAssociation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppliedJobInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoverLetter = c.String(nullable: false),
                        ExpectedSalary = c.Int(nullable: false),
                        AppliedTime = c.DateTime(nullable: false),
                        JobInfoId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobInfoes", t => t.JobInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.JobInfoId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.JobInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false),
                        JobNature = c.String(nullable: false),
                        EducationalRequirement = c.String(nullable: false),
                        ExperienceRequirement = c.String(nullable: false),
                        AdditionalRequirement = c.String(nullable: false),
                        NumberOfVacancies = c.Int(nullable: false),
                        SalaryRange = c.String(nullable: false),
                        OtherBenefit = c.String(nullable: false),
                        PublishTime = c.DateTime(nullable: false),
                        ApplicationDeadline = c.DateTime(nullable: false),
                        Approval = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserJobPostedInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobInfoId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobInfoes", t => t.JobInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.JobInfoId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        MobileNo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        CurrentJobInfo = c.String(nullable: false),
                        Approval = c.Boolean(nullable: false),
                        BatchId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Batches", t => t.BatchId, cascadeDelete: true)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .Index(t => t.BatchId)
                .Index(t => t.GenderId);
            
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchNumber = c.String(nullable: false),
                        ProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .Index(t => t.ProgramId);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserJobPostedInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Users", "BatchId", "dbo.Batches");
            DropForeignKey("dbo.Batches", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.AppliedJobInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserJobPostedInfoes", "JobInfoId", "dbo.JobInfoes");
            DropForeignKey("dbo.AppliedJobInfoes", "JobInfoId", "dbo.JobInfoes");
            DropIndex("dbo.UserJobPostedInfoes", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "GenderId" });
            DropIndex("dbo.Users", new[] { "BatchId" });
            DropIndex("dbo.Batches", new[] { "ProgramId" });
            DropIndex("dbo.AppliedJobInfoes", new[] { "UserId" });
            DropIndex("dbo.UserJobPostedInfoes", new[] { "JobInfoId" });
            DropIndex("dbo.AppliedJobInfoes", new[] { "JobInfoId" });
            DropTable("dbo.Genders");
            DropTable("dbo.Programs");
            DropTable("dbo.Batches");
            DropTable("dbo.Users");
            DropTable("dbo.UserJobPostedInfoes");
            DropTable("dbo.JobInfoes");
            DropTable("dbo.AppliedJobInfoes");
        }
    }
}
