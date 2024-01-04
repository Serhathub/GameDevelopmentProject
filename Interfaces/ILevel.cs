using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerDemo.Entities;
using PlatformerDemo.Terrain.Blocks;
using System.Collections.Generic;

namespace PlatformerDemo.Interfaces
{
    // ISP - Interface Segregation Principle
    // De interface is specifiek, en zorgt ervoor dat implementerende klassen zoals Level niet wordt belast met onnodige methoden.
    public interface ILevel
    {
        List<Enemy> Enemies { get; }
        List<Block> TerrainBlocks { get; }
        bool IsLevelComplete { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }

}
