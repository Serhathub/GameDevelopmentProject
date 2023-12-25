using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerDemo
{
    public class Menu
    {
        private SpriteFont font;
        private Texture2D backgroundTexture;
        private Texture2D startButtonTexture;
        private Texture2D exitButtonTexture;

        private Vector2 startOptionPosition;
        private Vector2 exitOptionPosition;

        public string SelectedOption { get; private set; }

        public Menu(SpriteFont font, GraphicsDevice graphicsDevice, Texture2D backgroundTexture, Texture2D startButtonTexture, Texture2D exitButtonTexture)
        {
            this.font = font;
            this.backgroundTexture = backgroundTexture;
            this.startButtonTexture = startButtonTexture;
            this.exitButtonTexture = exitButtonTexture;

            // Set the position of menu options
            float buttonScale = 0.5f; // Adjust scale as needed
            startOptionPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - startButtonTexture.Width * buttonScale / 2, graphicsDevice.Viewport.Height / 2 - startButtonTexture.Height * buttonScale / 2);
            exitOptionPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - exitButtonTexture.Width * buttonScale / 2, graphicsDevice.Viewport.Height / 2 + startButtonTexture.Height * buttonScale);
        }

        public void Update(GameTime gameTime)
        {
            // Get the mouse state
            MouseState mouseState = Mouse.GetState();

            // Check if the mouse cursor is over the Start button
            Rectangle startButtonRectangle = new Rectangle((int)startOptionPosition.X, (int)startOptionPosition.Y, 100, 20);
            if (startButtonRectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                SelectedOption = "Start";
            }

            // Check if the mouse cursor is over the Exit button
            Rectangle exitButtonRectangle = new Rectangle((int)exitOptionPosition.X, (int)exitOptionPosition.Y, 100, 20);
            if (exitButtonRectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                SelectedOption = "Exit";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw background
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            // Draw menu options
            spriteBatch.Draw(startButtonTexture, startOptionPosition, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(exitButtonTexture, exitOptionPosition, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public void ResetSelectedOption()
        {
            SelectedOption = null; // Or whatever the default state should be
        }

    }
}
