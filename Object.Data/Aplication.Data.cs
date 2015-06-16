using System.Data.Entity;
using Aplication.Model;
using Aplication.Data.Migrations;


namespace Aplication.Data.Data
{
    
    public class ObjectContext: DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonCommentLog> PersonCommentLogs { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachinesNotWorkLog> MachinesNotWorkLogs { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<CardForSelfControl> CardsForSelfControl { get; set; }
        public DbSet<SecurityAdministrator> AdministartorUssers { get; set; }
        public DbSet<SecurityOperator> OperatorsUssers { get; set; }
        public ObjectContext()
            : base("MalkiqKiro")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ObjectContext, Configuration>());
        }
    }
}
