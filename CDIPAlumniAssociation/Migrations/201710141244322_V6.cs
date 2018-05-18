namespace CDIPAlumniAssociation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserJobPostedInfoes", "UserId", "dbo.Users");
            DropIndex("dbo.UserJobPostedInfoes", new[] { "UserId" });
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        GenderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .Index(t => t.GenderId);
            
            AddColumn("dbo.UserJobPostedInfoes", "AdminId", c => c.Int());
            AlterColumn("dbo.UserJobPostedInfoes", "UserId", c => c.Int());
            CreateIndex("dbo.UserJobPostedInfoes", "AdminId");
            CreateIndex("dbo.UserJobPostedInfoes", "UserId");
            AddForeignKey("dbo.UserJobPostedInfoes", "AdminId", "dbo.Admins", "Id");
            AddForeignKey("dbo.UserJobPostedInfoes", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserJobPostedInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserJobPostedInfoes", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Admins", "GenderId", "dbo.Genders");
            DropIndex("dbo.UserJobPostedInfoes", new[] { "UserId" });
            DropIndex("dbo.UserJobPostedInfoes", new[] { "AdminId" });
            DropIndex("dbo.Admins", new[] { "GenderId" });
            AlterColumn("dbo.UserJobPostedInfoes", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.UserJobPostedInfoes", "AdminId");
            DropTable("dbo.Admins");
            CreateIndex("dbo.UserJobPostedInfoes", "UserId");
            AddForeignKey("dbo.UserJobPostedInfoes", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
