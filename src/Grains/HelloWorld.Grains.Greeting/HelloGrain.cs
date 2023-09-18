using HelloWorld.Interfaces.Hello;
using Orleans;

namespace HelloWorld.Grains.Greeting;

public class HelloGrain : Grain, IHelloGrain
{
    public Task<string> SayHello(string greeting)
    {
        return Task.FromResult($"You said: '{greeting}', I say: Hello!");
    }
}