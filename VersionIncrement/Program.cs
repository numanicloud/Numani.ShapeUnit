using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VersionIncrement;

await Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
        configurationBuilder.AddIniFile("version.ini", optional: false, reloadOnChange: false);
        configurationBuilder.AddCommandLine(args);
    })
    .ConfigureServices((context, collection) =>
    {
        collection.AddHostedService<MainService>();
        collection.Configure<VersionSettings>(context.Configuration);
        collection.Configure<AppOptions>(context.Configuration);
    })
    .RunConsoleAsync(options =>
    {
        options.SuppressStatusMessages = true;
    });