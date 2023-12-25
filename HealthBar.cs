using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerDemo
{
    public class HealthBar
    {
        private Texture2D heartTexture;
        private Vector2 position;
        private float scale;
        private int heartSize;

        public HealthBar(Texture2D heartTexture, Vector2 position, float scale = 0.025f)
        {
            this.heartTexture = heartTexture;
            this.position = position;
            this.scale = scale;
            heartSize = (int)(heartTexture.Width * scale); // Scale the heart size
        }

        public void Draw(SpriteBatch spriteBatch, int lives)
        {
            for (int i = 0; i < lives; i++)
            {
                Vector2 heartPosition = new Vector2(position.X + i * heartSize, position.Y);
                spriteBatch.Draw(heartTexture, heartPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

    }

}
