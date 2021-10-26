namespace AlterSolutions.Challenge.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_CATEGORY",
                c => new
                    {
                        ID_IN_CATEGORY = c.Int(nullable: false, identity: true),
                        DESCRIPTION_VC_CATEGORY = c.String(nullable: false, maxLength: 300, unicode: false),
                        ACTIVE_SI_CATEGORY = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID_IN_CATEGORY);
            
            CreateTable(
                "dbo.TB_PRODUCT",
                c => new
                    {
                        ID_IN_PRODUCT = c.Int(nullable: false, identity: true),
                        DESCRIPTION_VC_PRODUCT = c.String(nullable: false, maxLength: 300, unicode: false),
                        DIMENSIONS_VC_PRODUCT = c.String(maxLength: 160, unicode: false),
                        CODE_VC_PRODUCT = c.String(maxLength: 160, unicode: false),
                        REFERENCE_VC_PRODUCT = c.String(maxLength: 160, unicode: false),
                        INVENTORY_BALANCE_IN_PRODUCT = c.Int(nullable: false),
                        PRICE_VC_PRODUCT = c.Single(nullable: false),
                        ACTIVE_SI_PRODUCT = c.Byte(nullable: false),
                        ID_IN_CATEGORY = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_IN_PRODUCT)
                .ForeignKey("dbo.TB_CATEGORY", t => t.ID_IN_CATEGORY)
                .Index(t => t.ID_IN_CATEGORY);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_PRODUCT", "ID_IN_CATEGORY", "dbo.TB_CATEGORY");
            DropIndex("dbo.TB_PRODUCT", new[] { "ID_IN_CATEGORY" });
            DropTable("dbo.TB_PRODUCT");
            DropTable("dbo.TB_CATEGORY");
        }
    }
}
