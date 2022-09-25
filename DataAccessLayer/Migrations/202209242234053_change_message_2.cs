namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_message_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Mesaj", c => c.String());
            DropColumn("dbo.Contacts", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Message", c => c.String());
            DropColumn("dbo.Contacts", "Mesaj");
        }
    }
}
