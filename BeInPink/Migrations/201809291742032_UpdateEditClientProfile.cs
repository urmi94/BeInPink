namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEditClientProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "WaistToHipRatio", c => c.Double(nullable: true));
            AddColumn("dbo.AspNetUsers", "BMI", c => c.Double(nullable: true));
            AddColumn("dbo.AspNetUsers", "BmiResult", c => c.String(nullable: true));
            AddColumn("dbo.EditClientProfileViewModels", "WaistToHipRatio", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BmiResult");
            DropColumn("dbo.AspNetUsers", "BMI");
            DropColumn("dbo.AspNetUsers", "WaistToHipRatio");
            DropColumn("dbo.EditClientProfileViewModels", "WaistToHipRatio");
        }
    }
}
