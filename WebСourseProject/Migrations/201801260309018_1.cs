namespace WebСourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Groups", "Group_Name");
            DropColumn("dbo.Groups", "Count_Of_Student");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "Count_Of_Student", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "Group_Name", c => c.String());
        }
    }
}
