using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerDemo.States
{
    public class GameOverScreen
    {
        private SpriteFont font;
        private Vector2 position;
        private Vector2 returnMessagePosition;
        private string gameOverMessage;
        private string returnMessage;
        private Texture2D backgroundTexture;
        private GraphicsDevice graphicsDevice;

        public GameOverScreen(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            font = content.Load<SpriteFont>("MenuFont");
            gameOverMessage = "Game Over!";
            returnMessage = "Press 'Enter' to restart";

            Vector2 gameOverSize = font.MeasureString(gameOverMessage);
            position = new Vector2(graphicsDevice.Viewport.Width / 2 - gameOverSize.X / 2, graphicsDevice.Viewport.Height / 2 - gameOverSize.Y / 2);


            Vector2 gameOverMessageSize = font.MeasureString(gameOverMessage);
            Vector2 returnMessageSize = font.MeasureString(returnMessage);

            position = new Vector2((graphicsDevice.Viewport.Width - gameOverMessageSize.X) / 2, graphicsDevice.Viewport.Height / 2 - gameOverMessageSize.Y / 2);
            returnMessagePosition = new Vector2((graphicsDevice.Viewport.Width - returnMessageSize.X) / 2, position.Y + gameOverMessageSize.Y + 20);

            backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
            backgroundTexture.SetData(new Color[] { new Color(255, 0, 0) });

        }

        public bool Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);

            spriteBatch.DrawString(font, gameOverMessage, position, Color.Black);

            spriteBatch.DrawString(font, returnMessage, returnMessagePosition, Color.Black);
        }
    }
}
