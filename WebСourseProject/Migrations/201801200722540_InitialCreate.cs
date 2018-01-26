namespace WebСourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        letter = c.String(),
                        Group_Name = c.String(),
                        Count_Of_Student = c.Int(nullable: false),
                        TeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FathersName = c.String(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        imgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FathersName = c.String(nullable: false),
                        Information = c.String(nullable: false),
                        Qualification = c.String(nullable: false),
                        imgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Graduates",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FIO = c.String(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ScienceWorks", t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TeacherId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ScienceWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false, maxLength: 50),
                        ScienceDirection = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        PeriodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Periods", t => t.PeriodId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.PeriodId);
            
            CreateTable(
                "dbo.Periods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        begin = c.DateTime(nullable: false),
                        end = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        day = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        TimetableId = c.Int(nullable: false),
                        TeachersName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Timetables", t => t.TimetableId, cascadeDelete: true)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        published = c.DateTime(nullable: false),
                        Type = c.String(),
                        imgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.Timetables", "PeriodId", "dbo.Periods");
            DropForeignKey("dbo.Timetables", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Graduates", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Graduates", "Id", "dbo.ScienceWorks");
            DropForeignKey("dbo.Graduates", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Students", "GroupId", "dbo.Groups");
            DropIndex("dbo.Lessons", new[] { "TimetableId" });
            DropIndex("dbo.Timetables", new[] { "PeriodId" });
            DropIndex("dbo.Timetables", new[] { "GroupId" });
            DropIndex("dbo.Graduates", new[] { "GroupId" });
            DropIndex("dbo.Graduates", new[] { "TeacherId" });
            DropIndex("dbo.Graduates", new[] { "Id" });
            DropIndex("dbo.Students", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "TeacherId" });
            DropTable("dbo.Topics");
            DropTable("dbo.Lessons");
            DropTable("dbo.Periods");
            DropTable("dbo.Timetables");
            DropTable("dbo.ScienceWorks");
            DropTable("dbo.Graduates");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Groups");
        }
    }
}
