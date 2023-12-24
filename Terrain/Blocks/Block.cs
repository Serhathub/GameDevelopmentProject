using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

namespace PlatformerDemo.Terrain.Blocks
{
    public abstract class Block
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle SpriteFrame { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Color Color { get; set; } = Color.White;


        protected Block(int x, int y, Texture2D texture, Rectangle spriteFrame)
        {
            Position = new Vector2(x * 16, y * 16);
            Texture = texture;
            SpriteFrame = spriteFrame;
            BoundingBox = new Rectangle(x * 32, y * 32, 32, 32); // 32 because the blocks are scaled by 2
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SpriteFrame, Color, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
        }
    }

    public class PlatformBlock1 : Block
    {
        public PlatformBlock1(int x, int y, Texture2D texture)
            : base(x, y, texture, new Rectangle(0, 32, 16, 16)) // Use the correct coordinates for this block type
        {
        }
    }

    public class PlatformBlock2 : Block
    {
        public PlatformBlock2(int x, int y, Texture2D texture)
            : base(x, y, texture, new Rectangle(16, 32, 16, 16)) // Adjust coordinates as needed
        {
        }
    }

    public class PlatformBlock3 : Block
    {
        public PlatformBlock3(int x, int y, Texture2D texture)
            : base(x, y, texture, new Rectangle(32, 32, 16, 16)) // Adjust coordinates as needed
        {
        }
    }

    public class ExitBlock : Block
    {
        public ExitBlock(int x, int y, Texture2D texture)
            : base(x, y, texture, new Rectangle(48, 32, 16, 16)) // Adjust coordinates as needed
        {
        }
    }

}
