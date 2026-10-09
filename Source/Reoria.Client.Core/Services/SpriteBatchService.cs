using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reoria.Client.Core.Services.Interfaces;
using IGraphicsDeviceService = Reoria.Client.Core.Services.Interfaces.IGraphicsDeviceService;

namespace Reoria.Client.Core.Services;

public class SpriteBatchService(IGraphicsDeviceService graphicsDeviceService, ICameraService cameraService) : ISpriteBatchService
{
    protected virtual IGraphicsDeviceService GraphicsDevice { get; } = graphicsDeviceService;
    protected virtual ICameraService Camera { get; } = cameraService;
    public virtual SpriteBatch Batch { get; protected set; } = default!;

    public virtual bool IsInitialized
        => this.Batch is not null;

    public void Initialize()
    {
        if(!this.GraphicsDevice.IsInitialized)
        {
            throw new InvalidOperationException("Unable to initialize SpriteBatch: GraphicsDevice is not initialized.");
        }
        
        this.Batch = new SpriteBatch(this.GraphicsDevice.Device);
    }

    public virtual void OnPostRender(GameTime gameTime)
        => this.Batch.End();
    public virtual void OnPreRender(GameTime gameTime)
        => this.Batch.Begin(
            transformMatrix: this.Camera.Camera.GetViewMatrix(),
            samplerState: SamplerState.PointWrap,
            sortMode: SpriteSortMode.Deferred
            );
}