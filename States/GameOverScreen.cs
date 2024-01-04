using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerDemo.States
{
    public class GameOverScreen : BaseScreen
    {
        public GameOverScreen(ContentManager content, GraphicsDevice graphicsDevice)
            : base(content, graphicsDevice, "Game Over!", "Press 'Enter' to restart", new Color(255, 0, 0))
        {
        }
    }
}