using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformerDemo.Entities
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

        private Vector2 originalPosition;
        private float movementRange;
        // Strategy Pattern
        // Het gedrag van een vijand wordt tijdens runtime bepaald, waardoor het flexibel kan worden beheerd.
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

        public static void MoveLeftRight(Enemy enemy, GameTime gameTime)
        {
            enemy.Position += enemy.direction * enemy.movementSpeed;


            if (Math.Abs(enemy.Position.X - enemy.originalPosition.X) > enemy.movementRange)
            {
                enemy.direction *= -1;
                enemy.Position += enemy.direction * enemy.movementSpeed;
            }
        }

        public static void JumpAndFall(Enemy enemy, GameTime gameTime)
        {
            const float jumpSpeed = -300f;
            const float gravity = 800f;

            if (enemy.IsOnTheGround)
            {
                enemy.direction.Y = jumpSpeed;
                enemy.IsOnTheGround = false;
            }

            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            enemy.direction.Y += gravity * elapsedSeconds;

            enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y + enemy.direction.Y * elapsedSeconds);

            if (enemy.Position.Y >= enemy.originalPosition.Y)
            {
                enemy.Position = new Vector2(enemy.Position.X, enemy.originalPosition.Y);
                enemy.IsOnTheGround = true;
            }
        }


        public static void MoveInCircle(Enemy enemy, GameTime gameTime)
        {
            float circleRadius = enemy.movementRange;
            double time = gameTime.TotalGameTime.TotalSeconds;

            enemy.Position = new Vector2(
                enemy.originalPosition.X + circleRadius * (float)Math.Cos(time * enemy.movementSpeed),
                enemy.originalPosition.Y + circleRadius * (float)Math.Sin(time * enemy.movementSpeed)
            );
        }



        public void Reset()
        {
            Position = originalPosition;
        }


    }
}
