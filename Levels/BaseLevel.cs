using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerDemo.Interfaces;
using PlatformerDemo.Terrain;
using PlatformerDemo.Terrain.Blocks;
using PlatformerDemo.Entities;
using System.Collections.Generic;

namespace PlatformerDemo.Levels
{
    public abstract class BaseLevel : ILevel
    {
        protected TerrainBuilder terrainBuilder;
        protected Texture2D terrainTexture;
        protected GraphicsDevice graphicsDevice;

        public List<Enemy> Enemies { get; protected set; }
        public List<Block> TerrainBlocks => terrainBuilder.Blocks;
        public bool IsLevelComplete { get; protected set; }
        protected Player player;

        protected BaseLevel(GraphicsDevice graphicsDevice, ContentManager content, IBlueprint blueprint)
        {
            this.graphicsDevice = graphicsDevice;
            terrainBuilder = new TerrainBuilder(blueprint);
            terrainTexture = content.Load<Texture2D>("Tiles/tilemap");
            terrainBuilder.LoadTerrain(terrainTexture);
            Enemies = new List<Enemy>();
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            terrainBuilder.DrawTerrain(spriteBatch);
            foreach (var enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}