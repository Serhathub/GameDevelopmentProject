using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerDemo.States
{
    public abstract class BaseScreen
    {
        protected SpriteFont font;
        protected Vector2 position;
        protected Vector2 returnMessagePosition;
        protected string mainMessage;
        protected string returnMessage;
        protected Texture2D backgroundTexture;
        protected GraphicsDevice graphicsDevice;
        protected Color backgroundColor;

        protected BaseScreen(ContentManager content, GraphicsDevice graphicsDevice, string mainMessage, string returnMessage, Color backgroundColor)
        {
            this.graphicsDevice = graphicsDevice;
            this.backgroundColor = backgroundColor;

            font = content.Load<SpriteFont>("MenuFont");
            this.mainMessage = mainMessage;
            this.returnMessage = returnMessage;

            Vector2 messageSize = font.MeasureString(mainMessage);
            position = new Vector2((graphicsDevice.Viewport.Width - messageSize.X) / 2, graphicsDevice.Viewport.Height / 2 - messageSize.Y / 2);

            Vector2 returnMessageSize = font.MeasureString(returnMessage);
            returnMessagePosition = new Vector2((graphicsDevice.Viewport.Width - returnMessageSize.X) / 2, position.Y + messageSize.Y + 20);

            backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
            backgroundTexture.SetData(new Color[] { backgroundColor });
        }

        public virtual bool Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                return true;
            }
            return false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), backgroundColor);
            spriteBatch.DrawString(font, mainMessage, position, Color.Black);
            spriteBatch.DrawString(font, returnMessage, returnMessagePosition, Color.Black);
        }
    }
}