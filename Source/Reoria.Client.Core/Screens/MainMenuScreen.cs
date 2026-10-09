using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Reoria.Client.Core.Screens.Attributes;
using Reoria.Client.Core.Services.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using IGraphicsDeviceService = Reoria.Client.Core.Services.Interfaces.IGraphicsDeviceService;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Screen = MonoGame.Extended.Screens.Screen;

namespace Reoria.Client.Core.Screens;

[DefaultScreen]
public class MainMenuScreen(IScreenService screenService,
    IGraphicsDeviceService graphicsDeviceService,
    ISpriteBatchService spriteBatchService) : Screen
{
    protected IScreenService ScreenService { get; init; } = screenService;
    protected IGraphicsDeviceService GraphicsService { get; init; } = graphicsDeviceService;
    protected ISpriteBatchService SpriteBatch { get; init; } = spriteBatchService;

    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Enter))
        {
            this.ScreenService.ChangeScreen<InGameScreen>();
        }
    }

    public override void Draw(GameTime gameTime)
    {
        this.GraphicsService.Clear(Color.CornflowerBlue);

        this.SpriteBatch.OnPreRender(gameTime);
        
        this.SpriteBatch.OnPostRender(gameTime);
    }
}