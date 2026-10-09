using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Reoria.Client.Core.Screens.Attributes;
using Reoria.Client.Core.Services.Interfaces;
using Screen = MonoGame.Extended.Screens.Screen;

namespace Reoria.Client.Core.Services;

public class ScreenService() : IScreenService
{
    protected virtual Screen[] Screens { get; set; } = default!;
    protected virtual ScreenManager ScreenManager { get; set; } = default!;
    protected float Lockout { get; set; } = 0f;

    public virtual void Initialize(GameComponentCollection components, Screen[] screens)
    {
        // Cache the screens.
        this.Screens = screens;

        // Initalize the screen manager.
        this.ScreenManager = new ScreenManager();
        components.Add(this.ScreenManager);

        // Now we want to get the default screen and set it as the active screen
        Screen defaultScreen = this.Screens.FirstOrDefault(screen =>
            screen is not null && Attribute.IsDefined(screen.GetType(), typeof(DefaultScreenAttribute)))
            ?? throw new InvalidOperationException("No default screen found.");

        // Set the default screen.
        this.ScreenManager.ShowScreen(defaultScreen);
    }

    public virtual void ChangeScreen<TScreen>() where TScreen : Screen
    {
        if (this.Lockout > 0f)
        { return; }

        Screen targetScreen = this.Screens.FirstOrDefault(screen =>
            screen is not null && screen.GetType() == typeof(TScreen))
            ?? throw new InvalidOperationException($"No screen of type {typeof(TScreen).Name} found.");

        if (this.ScreenManager.ActiveScreen != targetScreen)
        {
            this.ScreenManager.ShowScreen(targetScreen);
        }

        this.Lockout = 5f;
    }

    public virtual void Update(GameTime gameTime)
    {
        if(this.Lockout > 0f)
        {
            this.Lockout -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this.Lockout < 0f)
            {
                this.Lockout = 0f;
            }
        }
    }
}