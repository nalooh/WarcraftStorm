using Microsoft.EntityFrameworkCore;
using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Data.Realms;
using WarcraftStorm.Network;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<RealmsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RealmsConnection")));
builder.Services.AddWarcraftStormServer<AuthConnectionFactory>(3724);

var host = builder.Build();
host.Run();
