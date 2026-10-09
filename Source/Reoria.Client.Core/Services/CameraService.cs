using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Reoria.Client.Core.Services.Interfaces;

namespace Reoria.Client.Core.Services;

public class CameraService(IGraphicsDeviceService graphicsDeviceService) : ICameraService
{
    /**
     * Temporary Camera Properties
     * These need to be relocated to a proper "Transform" class.
     */
    protected virtual Vector2 Position { get; set; } = Vector2.Zero;
    protected virtual float Scale { get; set; } = 1f;
    protected virtual float Rotation { get; set; } = 0f;

    /**
     * Temporary Camera Functions
     */
    public virtual void MoveCamera(Vector2 position) 
        => this.Position += position;
    public virtual void ScaleCamera(float scale) 
        => this.Scale += scale;
    public virtual void RotateCamera(float rotation) 
        => this.Rotation += rotation;
    public virtual void Reset()
    {
        this.Position = Vector2.Zero;
        this.Scale = 1f;
        this.Rotation = 0f;
    }

    /**
     * Camera Code Starts Here.
     */
    protected virtual IGraphicsDeviceService GraphicsDevice { get; } = graphicsDeviceService;
    public virtual OrthographicCamera Camera { get; protected set; } = default!;

    public virtual bool IsInitialized
        => this.Camera is not null;

    public virtual void Initalize()
    {
        if (this.GraphicsDevice.IsInitialized)
        {
            this.Camera = new OrthographicCamera(this.GraphicsDevice.Device);
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        this.Camera.LookAt(this.Position);
        this.Camera.Rotation = this.Rotation;
        this.Camera.Zoom = this.Scale;
    }
}