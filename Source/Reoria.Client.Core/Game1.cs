using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Reoria.Client.Core.Screens;
using Reoria.Client.Core.Services;
using Reoria.Client.Core.Services.Interfaces;
using Reoria.Engine.Core.DependencyInjection;
using Reoria.Engine.Core.DependencyInjection.Interfaces;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using IGraphicsDeviceService = Reoria.Client.Core.Services.Interfaces.IGraphicsDeviceService;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Screen = MonoGame.Extended.Screens.Screen;

namespace Reoria.Client.Core;

public class Game1 : Game
{
    protected virtual IDependencyInjectionService DependencyInjection { get; private set; } = default!;
    protected virtual ILifetimeScope Scope { get; private set; } = default!;

    protected virtual IGraphicsDeviceService GraphicsService { get; private set; } = default!;
    protected virtual IContentManagerService ContentManager { get; private set; } = default!;
    protected virtual ISpriteBatchService SpriteBatch { get; private set; } = default!;
    protected virtual ICameraService Camera { get; private set; } = default!;
    protected virtual IScreenService ScreenService { get; private set; } = default!;

    private GraphicsDeviceManager GraphicsDeviceManager { get; } = default!;

    public Game1()
    {
        // TODO: Eventually this will be done with a Bootstrapper, for now just do it manually.
        this.DependencyInjection = new DependencyInjectionService();
        _ = this.DependencyInjection.Builder.RegisterType<GraphicsDeviceService>()
            .As<IGraphicsDeviceService>()
            .SingleInstance();
        _ = this.DependencyInjection.Builder.RegisterType<ContentManagerService>()
            .As<IContentManagerService>()
            .SingleInstance();
        _ = this.DependencyInjection.Builder.RegisterType<SpriteBatchService>()
            .As<ISpriteBatchService>()
            .SingleInstance();
        _ = this.DependencyInjection.Builder.RegisterType<CameraService>()
            .As<ICameraService>()
            .SingleInstance();
        _ = this.DependencyInjection.Builder.RegisterType<ScreenService>()
            .As<IScreenService>()
            .SingleInstance();
        _ = this.DependencyInjection.Builder.RegisterType<MainMenuScreen>()
            .As<Screen>()
            .InstancePerLifetimeScope();
        _ = this.DependencyInjection.Builder.RegisterType<InGameScreen>()
            .As<Screen>()
            .InstancePerLifetimeScope();
        this.Scope = this.DependencyInjection.CreateScope();

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
