using System.Threading.Tasks;
using Orleans;

namespace HelloWorld.Interfaces.Hello;

public interface IHelloGrain : IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting); 
}