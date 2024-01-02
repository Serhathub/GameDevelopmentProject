using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerDemo
{
    public class Enemy
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public bool IsActive { get; set; }
        public bool IsOnTheGround { get; set; }
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        private float movementSpeed;
        private Action<Enemy, GameTime> behavior;
        private Vector2 direction = Vector2.UnitX;
        private Random random = new Random();

        private Vector2 originalPosition;
        private float movementRange; // The range in which the enemy moves

        public Enemy(Texture2D texture, Vector2 initialPosition, float speed, float moveRange, Action<Enemy, GameTime> behaviorFunc)
        {
            Texture = texture;
            Position = originalPosition = initialPosition;
            movementSpeed = speed;
            movementRange = moveRange;
            behavior = behaviorFunc;
            IsActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                behavior(this, gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }

        // Modified behavior: move left and right within a range
        public static void MoveLeftRight(Enemy enemy, GameTime gameTime)
        {
            enemy.Position += enemy.direction * enemy.movementSpeed;

            // Check if the enemy is outside its movement range
            if (Math.Abs(enemy.Position.X - enemy.originalPosition.X) > enemy.movementRange)
            {
                enemy.direction *= -1; // Change direction
                enemy.Position += enemy.direction * enemy.movementSpeed; // Adjust position to stay within range
            }
        }
       



        public void Reset()
        {
            Position = originalPosition;
        }

        // Other behaviors...
    }
}
