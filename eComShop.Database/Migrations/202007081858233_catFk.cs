namespace eComShop.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catFk : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Products", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AlterColumn("dbo.Products", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Products", "Category_Id");
        }
    }
}
