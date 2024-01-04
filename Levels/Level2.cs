using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerDemo.Levels;
using PlatformerDemo.Entities;
using System.Collections.Generic;
using PlatformerDemo.Terrain;

namespace PlatformerDemo.Levels
{
    public class Level2 : BaseLevel
    {
        private Texture2D enemyTexture;
        private Texture2D enemyTexture2;
        private Texture2D enemyTexture3;

        public Level2(GraphicsDevice graphicsDevice, ContentManager content)
            : base(graphicsDevice, content, new Blueprint2())
        {
            enemyTexture = content.Load<Texture2D>("Enemies/tile_0053");
            enemyTexture2 = content.Load<Texture2D>("Enemies/tile_0055");
            enemyTexture3 = content.Load<Texture2D>("Enemies/tile_0051");

            float movementRange = 2 * 16;
            Enemies.Add(new Enemy(enemyTexture, new Vector2(350, 175), 2f, movementRange, Enemy.MoveInCircle));
            Enemies.Add(new Enemy(enemyTexture2, new Vector2(370, 35), 1f, movementRange, Enemy.MoveLeftRight));
            Enemies.Add(new Enemy(enemyTexture3, new Vector2(200, 70), 2f, movementRange, Enemy.JumpAndFall));
        }

        public override void Update(GameTime gameTime)
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
    }
}