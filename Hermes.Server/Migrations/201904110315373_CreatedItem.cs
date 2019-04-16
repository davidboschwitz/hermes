namespace Hermes.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatabaseItems",
                c => new
                    {
                        MessageID = c.Guid(nullable: false),
                        SenderID = c.Guid(nullable: false),
                        RecipientID = c.Guid(nullable: false),
                        CreatedTimestamp = c.DateTime(nullable: false),
                        UpdatedTimestamp = c.DateTime(nullable: false),
                        MessageNamespace = c.String(),
                        MessageName = c.String(),
                    })
                .PrimaryKey(t => t.MessageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DatabaseItems");
        }
    }
}
