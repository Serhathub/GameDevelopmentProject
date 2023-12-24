using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerDemo
{
    public class Enemy
    {
        public Vector2 Position { get; private set; }
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, EnemyTexture.Width, EnemyTexture.Height);
        public bool IsActive { get; set; }
        public Texture2D EnemyTexture { get; private set; }

        private float movementSpeed;
        private Action<Enemy, GameTime> aiBehavior;

        public Enemy(Texture2D texture, Vector2 initialPosition, float speed, Action<Enemy, GameTime> behavior)
        {
            EnemyTexture = texture;
            Position = initialPosition;
            movementSpeed = speed;
            aiBehavior = behavior;
            IsActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                aiBehavior(this, gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(EnemyTexture, Position, Color.White);
            }
        }

        // AI that moves the enemy left and right
        void MoveLeftRight(Enemy enemy, GameTime gameTime)
        {
            // Logic to move enemy left and right
        }

        // Random movement AI
        void MoveRandomly(Enemy enemy, GameTime gameTime)
        {
            // Logic for random movement
        }

    }

}
