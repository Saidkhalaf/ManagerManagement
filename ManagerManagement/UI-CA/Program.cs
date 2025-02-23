// See https://aka.ms/new-console-template for more information

using ManagerManagement.BL;
using ManagerManagement.DAL.EF;
using ManagerManagement.UI.CA;
using Microsoft.EntityFrameworkCore;

DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder<ManagerDbContext>();
dbContextOptionsBuilder.UseSqlite("Data Source=ManagerManagementDb.db");
ManagerDbContext dbContext = new ManagerDbContext(dbContextOptionsBuilder.Options);

var repository = new Repository(dbContext);
var manager = new Manager(repository);
ConsoleUi consoleUi = new ConsoleUi(manager);

if (dbContext.CreateDatabase(dropDatabase:true))
{
    DataSeeder.Seed(dbContext);
}
consoleUi.Run();