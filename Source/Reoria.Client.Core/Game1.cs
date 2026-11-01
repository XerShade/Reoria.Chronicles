using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Reoria.Client.Core.Services.Interfaces;
using Syntaxis.Core.Hooks.Extensions;
using Syntaxis.Core.Hooks.Interfaces;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using IGraphicsDeviceService = Reoria.Client.Core.Services.Interfaces.IGraphicsDeviceService;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Screen = MonoGame.Extended.Screens.Screen;

namespace Reoria.Client.Core;

public class Game1 : Game
{
    protected virtual ILifetimeScope Scope { get; private set; } = default!;
    protected virtual IGraphicsDeviceService GraphicsService { get; private set; } = default!;
    protected virtual IContentManagerService ContentManager { get; private set; } = default!;
    protected virtual ISpriteBatchService SpriteBatch { get; private set; } = default!;
    protected virtual ICameraService Camera { get; private set; } = default!;
    protected virtual IScreenService ScreenService { get; private set; } = default!;

    private GraphicsDeviceManager GraphicsDeviceManager { get; } = default!;

    public Game1(ILifetimeScope scope)
    {
        // Store the scope.
        this.Scope = scope;

        IHookEngine hookEngine = this.Scope.Resolve<IHookEngine>();
        hookEngine.Execute<IHookSubscriber>(hook => Console.Write(hook.GetHookName()));

        // Resolve services, we won't need most of these once we have proper lifecycle code.
        this.GraphicsService = this.Scope.Resolve<IGraphicsDeviceService>();
        this.ContentManager = this.Scope.Resolve<IContentManagerService>();
        this.SpriteBatch = this.Scope.Resolve<ISpriteBatchService>();
        this.Camera = this.Scope.Resolve<ICameraService>();
        this.ScreenService = this.Scope.Resolve<IScreenService>();

        // TODO: Handle MonoGame stuff properly later.
        this.GraphicsDeviceManager = new GraphicsDeviceManager(this);
        this.ContentManager.Initialize(this.Content);
        this.IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        if(!this.GraphicsService.IsInitialized)
        {
            this.GraphicsService.Initialize(this.GraphicsDevice, this.GraphicsDeviceManager);
            this.Camera.Initalize();
            this.ScreenService.Initialize(this.Components, this.Scope.Resolve<Screen[]>());
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        if (!this.GraphicsService.IsInitialized)
        {
            this.GraphicsService.Initialize(this.GraphicsDevice, this.GraphicsDeviceManager);
            this.Camera.Initalize();
            this.ScreenService.Initialize(this.Components, this.Scope.Resolve<Screen[]>());
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            this.Exit();
        }

        // TODO: Figure out a better place for this later, for now it works.
        //        The problem is that we need to wait until the GraphicsDevice is initialized before we can use it.
        //        This isn't a problem on desktop, but android likes to load things wierdly, so do it here for now.
        if (!this.SpriteBatch.IsInitialized)
        {
            this.SpriteBatch.Initialize();
        }

        this.ScreenService.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
        => base.Draw(gameTime);
}
