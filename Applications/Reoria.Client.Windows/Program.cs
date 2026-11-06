using Syntaxis.Core.Application;
using Syntaxis.Core.Enumerations;
using Syntaxis.MonoGame;

namespace Reoria.Client.Windows;

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
            using ApplicationBootstrapper bootstrapper = new(Platform.Windows, args);
            using SyntaxisMonoGame game = bootstrapper.Run<SyntaxisMonoGame>();
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