using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerDemo.Animations
{
    // OCP - Open/Closed Principle
    // De Animation klasse is open voor uitbreiding (je kunt meer animaties toevoegen) maar gesloten voor wijziging (de bestaande logica blijft intact).
    public class Animation
    {
        private Texture2D[] frames;
        private float frameTime;
        private int currentFrame;
        private float timeSinceLastFrame;

        public Animation(Texture2D[] frames, float frameTime)
        {
            this.frames = frames;
            this.frameTime = frameTime;
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastFrame > frameTime)
            {
                currentFrame = (currentFrame + 1) % frames.Length;
                timeSinceLastFrame = 0;
            }
        }

        public Texture2D CurrentFrameTexture
        {
            get { return frames[currentFrame]; }
        }

        public void Reset()
        {
            currentFrame = 0;
            timeSinceLastFrame = 0;
        }
    }
}
