using Orleans;
using Orleans.Configuration;
using static System.Console;

using HelloWorld.Interfaces.Hello;

WriteLine("\r\n---Orleans RPCDemo Client---");
WriteLine("\r\n---\r\nInitializing Orleans Client...\r\n---");
var client = new ClientBuilder()
    .UseLocalhostClustering()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "console-client-01";
        options.ServiceId = "Demo Greeting Service";
    })
    .ConfigureApplicationParts(parts =>
    {
        parts.AddApplicationPart(typeof(IHelloGrain).Assembly).WithReferences();
    }).Build();

WriteLine( "Please wait until Orleans Server is started and ready for connections, then press any key to start connect...");
ReadKey();
await client.Connect();
WriteLine("\r\n---\r\nOrleans Client connected\r\n---");

var helloGrain = client.GetGrain<IHelloGrain>(0);
var helloResult = await helloGrain.SayHello("Orleans");
WriteLine($"\r\n---\r\nCall HelloGrain.SayHello(\"Orleans\") =\r\n{helloResult}\r\n---");

WriteLine("\r\n---\r\nDemonstration finished, press any key to exit...");
ReadKey();

await client.Close();
client.Dispose();
