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
        private Texture2D enemyTexture2;
        private Texture2D enemyTexture3;
        private Texture2D terrainTexture;
        private GraphicsDevice graphicsDevice;
        private Player player;

        public List<Block> TerrainBlocks => terrainBuilder.Blocks;
        public bool IsLevelComplete { get; private set; }

        public Level(GraphicsDevice graphicsDevice, ContentManager content)
        {
            
            IBlueprint blueprint = new Blueprint();
            terrainBuilder = new TerrainBuilder(blueprint);
            terrainTexture = content.Load<Texture2D>("Tiles/tilemap");
            terrainBuilder.LoadTerrain(terrainTexture);

            enemyTexture = content.Load<Texture2D>("Enemies/tile_0053");
            enemyTexture2 = content.Load<Texture2D>("Enemies/tile_0055");
            enemyTexture3 = content.Load<Texture2D>("Enemies/tile_0051");

            
            Enemies = new List<Enemy>();
            float movementRange = 2 * 16; 
            Enemies.Add(new Enemy(enemyTexture, new Vector2(350, 150), 2f, movementRange, Enemy.MoveLeftRight));
            Enemies.Add(new Enemy(enemyTexture2, new Vector2(530, 145), 2f, movementRange, Enemy.MoveLeftRight));
            Enemies.Add(new Enemy(enemyTexture3, new Vector2(250, 70), 2f, movementRange, Enemy.JumpAndFall));
            

            IsLevelComplete = false;
        }

        public void Update(GameTime gameTime)
        {
            if (player !=null && player.Position.X > graphicsDevice.Viewport.Width)
            {
                IsLevelComplete = true;
            }
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
