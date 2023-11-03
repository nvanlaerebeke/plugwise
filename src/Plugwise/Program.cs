using Plugwise.Api;

namespace Plugwise;
internal static class Program {
    private static void Main(string[] args) {
        new Startup().Start(WebApplication.CreateBuilder(args));
    }
}
