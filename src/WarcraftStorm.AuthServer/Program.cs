using Microsoft.EntityFrameworkCore;
using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Data.Realms;
using WarcraftStorm.Network;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<RealmsDbContext>(options => options.UseSqlServer("Server=192.168.32.15,9433;Database=WarcraftStorm;User Id=sa;Password=Pa11§w0rd;Trust Server Certificate=true;App=WarcraftStorm AuthServer;"));
builder.Services.AddWarcraftStormServer<AuthConnectionFactory>(3724);

var host = builder.Build();
host.Run();
