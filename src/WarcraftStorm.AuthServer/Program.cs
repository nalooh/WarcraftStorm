using WarcraftStorm.AuthServer.Network;
using WarcraftStorm.Network;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddLogging(configure => configure.AddConsole());
builder.Services.AddWarcraftStormServer<AuthConnectionFactory>(3724);

var host = builder.Build();
host.Run();
