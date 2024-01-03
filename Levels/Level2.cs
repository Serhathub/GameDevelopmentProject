using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformerDemo.Interfaces;
using PlatformerDemo.Terrain.Blocks;
using PlatformerDemo.Terrain.PlatformerDemo.Terrain;
using PlatformerDemo.Terrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformerDemo.Entities;

namespace PlatformerDemo.Levels
{
    public class Level2 : ILevel
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

        public Level2(GraphicsDevice graphicsDevice, ContentManager content)
        {

            IBlueprint blueprint = new Blueprint2();
            terrainBuilder = new TerrainBuilder(blueprint);
            terrainTexture = content.Load<Texture2D>("Tiles/tilemap");
            terrainBuilder.LoadTerrain(terrainTexture);

            enemyTexture = content.Load<Texture2D>("Enemies/tile_0053");
            enemyTexture2 = content.Load<Texture2D>("Enemies/tile_0055");
            enemyTexture3 = content.Load<Texture2D>("Enemies/tile_0051");


            Enemies = new List<Enemy>();
            float movementRange = 2 * 16;
            Enemies.Add(new Enemy(enemyTexture, new Vector2(350, 175), 2f, movementRange, Enemy.MoveInCircle));
            Enemies.Add(new Enemy(enemyTexture2, new Vector2(370, 35), 1f, movementRange, Enemy.MoveLeftRight));
            Enemies.Add(new Enemy(enemyTexture3, new Vector2(200, 70), 2f, movementRange, Enemy.JumpAndFall));


            IsLevelComplete = false;
        }

        public void Update(GameTime gameTime)
        {
            if (player != null && player.Position.X > graphicsDevice.Viewport.Width)
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

        public bool IsPlayerAtEndOfLevel(Player player)
        {
            // Assuming the level width is the width of the graphics device viewport
            return player.Position.X > graphicsDevice.Viewport.Width - 2; // 'someOffset' is a value you determine
        }
    }
}
