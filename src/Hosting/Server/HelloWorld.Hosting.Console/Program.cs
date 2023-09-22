using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using static System.Console;

using HelloWorld.Grains.Greeting;
using System.Net;

var siloHost = new SiloHostBuilder()
    .UseLocalhostClustering()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "console-host-01";
        options.ServiceId = "Demo Greeting Service";
    })
    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
    .ConfigureApplicationParts(parts =>
    {
        parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences();
    })
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
        logging.AddDebug();
    })
    .Build();

//Tricks to manually wait for Ctrl+C key press
var waitForProcessShutdown = new ManualResetEvent(false);
CancelKeyPress += (sender, eventArgs) =>
{
    eventArgs.Cancel = true;
    waitForProcessShutdown.Set();
};

await siloHost.StartAsync();
WriteLine(
    "===\r\nOrleans Silo started and able to connect,\r\nPress Ctrl+C to shutdown when client finish demonstration...\r\n===");
waitForProcessShutdown.WaitOne();

WriteLine("Shutting down Silo...");
await siloHost.StopAsync().ConfigureAwait(false);
WriteLine("===\r\nSilo shutdown complete, exiting...\r\n===");
Environment.Exit(0);

