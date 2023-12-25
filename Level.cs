using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerDemo.Interfaces;
using PlatformerDemo.Terrain.PlatformerDemo.Terrain;
using PlatformerDemo.Terrain;
using System.Collections.Generic;
using PlatformerDemo.Terrain.Blocks;

namespace PlatformerDemo
{
    public class Level
    {
        public List<Enemy> Enemies { get; private set; }
        private TerrainBuilder terrainBuilder;
        private Texture2D enemyTexture;
        private Texture2D enemyTexture3;
        private Texture2D terrainTexture;

        public List<Block> TerrainBlocks => terrainBuilder.Blocks;

        public Level(GraphicsDevice graphicsDevice, ContentManager content)
        {
            // Initialize Terrain
            IBlueprint blueprint = new Blueprint();
            terrainBuilder = new TerrainBuilder(blueprint);
            terrainTexture = content.Load<Texture2D>("Tiles/tilemap");
            terrainBuilder.LoadTerrain(terrainTexture);

            // Load enemy texture
            enemyTexture = content.Load<Texture2D>("Enemies/tile_0053");
            enemyTexture3 = content.Load<Texture2D>("Enemies/tile_0055");

            // Initialize enemies
            Enemies = new List<Enemy>();
            float movementRange = 2 * 16; // Adjust as needed
            Enemies.Add(new Enemy(enemyTexture, new Vector2(350, 150), 2f, movementRange, Enemy.MoveLeftRight));
            Enemies.Add(new Enemy(enemyTexture3, new Vector2(530, 145), 2f, movementRange, Enemy.MoveLeftRight));
            // Add more enemies as needed
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

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
