using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerDemo
{
    public class GameOverScreen
    {
        private SpriteFont font;
        private Vector2 position;
        private string gameOverMessage;
        private string returnMessage;

        public GameOverScreen(ContentManager content, GraphicsDevice graphicsDevice)
        {
            font = content.Load<SpriteFont>("MenuFont"); // Load your font
            gameOverMessage = "Game Over!";
            returnMessage = "Press 'Enter' to restart";

            // Calculate positions to center the text
            Vector2 gameOverSize = font.MeasureString(gameOverMessage);
            position = new Vector2(graphicsDevice.Viewport.Width / 2 - gameOverSize.X / 2, graphicsDevice.Viewport.Height / 2 - gameOverSize.Y / 2);
        }

        public bool Update(GameTime gameTime)
        {
            // Check for Enter key to return to the menu
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                return true; // Indicates that we should return to the menu
            }
            return false; // Stay on the game over screen
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameOverMessage, position, Color.White);

            // Draw the return message below the game over message
            Vector2 returnMessagePosition = new Vector2(position.X, position.Y + 50); // Adjust Y position as needed
            spriteBatch.DrawString(font, returnMessage, returnMessagePosition, Color.White);
        }
    }
}
