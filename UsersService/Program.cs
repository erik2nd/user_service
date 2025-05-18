using UsersService;
using UsersService.DAL.Extensions;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
    .Build()
    .MigrateUp()
    .Run();