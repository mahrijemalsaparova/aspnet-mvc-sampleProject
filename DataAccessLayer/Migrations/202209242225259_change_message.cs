namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_message : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Messages", newName: "Mesajs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Mesajs", newName: "Messages");
        }
    }
}
