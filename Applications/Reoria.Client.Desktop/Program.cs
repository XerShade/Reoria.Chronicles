using Reoria.Client.Core;
using Syntaxis.Core.Application;
using Syntaxis.Core.Enumerations;

namespace Reoria.Client.Desktop;

/// <summary>
/// Entry point for the Reoria Desktop Client application.
/// </summary>
/// <remarks>
/// This program initializes and runs the desktop version of the Reoria client.
/// The desktop platform provides full keyboard and mouse input support along with windowed display capabilities.
/// </remarks>
public static class Program
{
    /// <summary>
    /// The main entry point for the desktop client application.
    /// </summary>
    /// <param name="args">Command line arguments passed to the application.</param>
    /// <returns>Exit code indicating application termination status.</returns>
    public static int Main(string[] args)
    {
        try
        {
            using ApplicationBootstrapper bootstrapper = new(Platform.Desktop, args);
            using Game1 game = bootstrapper.Run<Game1>();
            game.Run();

            return 0; // Successful exit
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Fatal error starting desktop client: {ex.Message}");
            return ex.HResult; // Error exit code
        }
    }
}