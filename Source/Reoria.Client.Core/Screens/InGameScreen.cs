using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reoria.Client.Core.Services.Interfaces;
using IGraphicsDeviceService = Reoria.Client.Core.Services.Interfaces.IGraphicsDeviceService;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Screen = MonoGame.Extended.Screens.Screen;
using Color = Microsoft.Xna.Framework.Color;

namespace Reoria.Client.Core.Screens;

public class InGameScreen(IScreenService screenService,
    IGraphicsDeviceService graphicsDeviceService,
    IContentManagerService contentManagerService,
    ISpriteBatchService spriteBatchService,
    ICameraService cameraService) : Screen
{
    protected IScreenService ScreenService { get; init; } = screenService;
    protected IGraphicsDeviceService GraphicsService { get; init; } = graphicsDeviceService;
    protected IContentManagerService ContentManager { get; init; } = contentManagerService;
    protected ISpriteBatchService SpriteBatch { get; init; } = spriteBatchService;
    protected ICameraService Camera { get; init; } = cameraService;

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            this.ScreenService.ChangeScreen<MainMenuScreen>();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            this.Camera.MoveCamera(new Vector2(0, -1));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            this.Camera.MoveCamera(new Vector2(0, 1));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            this.Camera.MoveCamera(new Vector2(-1, 0));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            this.Camera.MoveCamera(new Vector2(1, 0));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
        {
            this.Camera.RotateCamera(-1f);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.E))
        {
            this.Camera.RotateCamera(1f);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Z))
        {
            this.Camera.ScaleCamera(-1f);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.C))
        {
            this.Camera.ScaleCamera(1f);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.X))
        {
            this.Camera.Reset();
        }

        this.Camera.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        this.GraphicsService.Clear(Color.Orange);

        Texture2D texture = this.ContentManager.Content.Load<Texture2D>("Graphics/Characters/TimeFantasy/chara5");
        this.SpriteBatch.OnPreRender(gameTime);
        this.SpriteBatch.Batch.Draw(texture, Vector2.Zero, Color.White);
        this.SpriteBatch.OnPostRender(gameTime);
    }
}