using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Reoria.Client.Core.Services.Interfaces;

public interface ICameraService
{
    OrthographicCamera Camera { get; }
    bool IsInitialized { get; }

    void MoveCamera(Vector2 position);
    void Initalize();
    void Update(GameTime gameTime);
    void RotateCamera(float rotation);
    void ScaleCamera(float scale);
    void Reset();
}