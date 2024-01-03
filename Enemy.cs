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
        

        private Vector2 originalPosition;
        private float movementRange; 

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

            
            enemy.Position += enemy.direction * elapsedSeconds;

            
            if (enemy.Position.Y >= enemy.originalPosition.Y)
            {
                enemy.Position = new Vector2(enemy.Position.X, enemy.originalPosition.Y);
                enemy.IsOnTheGround = true;
            }
        }



        public void Reset()
        {
            Position = originalPosition;
        }

        
    }
}
