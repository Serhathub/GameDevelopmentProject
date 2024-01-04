using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerDemo.States
{
    public class VictoryScreen : BaseScreen
    {
        public VictoryScreen(ContentManager content, GraphicsDevice graphicsDevice)
            : base(content, graphicsDevice, "Victory!", "Press 'Enter' to return to menu!", new Color(0, 255, 0))
        {
        }
    }
}