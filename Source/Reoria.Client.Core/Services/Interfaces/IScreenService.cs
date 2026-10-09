using Microsoft.Xna.Framework;
using Screen = MonoGame.Extended.Screens.Screen;

namespace Reoria.Client.Core.Services.Interfaces;

public interface IScreenService
{
    void ChangeScreen<TScreen>() where TScreen : Screen;
    void Initialize(GameComponentCollection components, Screen[] screens);
    void Update(GameTime gameTime);
}