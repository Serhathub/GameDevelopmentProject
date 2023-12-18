using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerDemo
{
    public class Animation
    {
        private Texture2D[] frames;
        private float frameTime;
        private int currentFrame;
        private float timeSinceLastFrame;

        public bool IsFlipped { get; set; }

        public Animation(Texture2D[] frames, float frameTime)
        {
            this.frames = frames;
            this.frameTime = frameTime;
            IsFlipped = false; // Default to not flipped
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
            get
            {
                if (IsFlipped)
                {
                    // If flipped, create a flipped version of the texture
                    Texture2D originalTexture = frames[currentFrame];
                    int width = originalTexture.Width;
                    Color[] data = new Color[width * originalTexture.Height];
                    originalTexture.GetData(data);

                    for (int i = 0; i < data.Length / 2; i++)
                    {
                        Color temp = data[i];
                        data[i] = data[data.Length - i - 1];
                        data[data.Length - i - 1] = temp;
                    }

                    Texture2D flippedTexture = new Texture2D(originalTexture.GraphicsDevice, originalTexture.Width, originalTexture.Height);
                    flippedTexture.SetData(data);

                    return flippedTexture;
                }
                else
                {
                    return frames[currentFrame];
                }
            }
        }

        public void Reset()
        {
            currentFrame = 0;
            timeSinceLastFrame = 0;
        }
    }
}



