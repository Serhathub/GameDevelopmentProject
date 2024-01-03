using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerDemo.Entities;
using PlatformerDemo.Terrain.Blocks;
using System.Collections.Generic;

namespace PlatformerDemo.Interfaces
{
    public interface ILevel
    {
        List<Enemy> Enemies { get; }
        List<Block> TerrainBlocks { get; }
        bool IsLevelComplete { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }

}
