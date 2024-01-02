using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerDemo.Terrain.Blocks;
using System.Collections.Generic;

namespace PlatformerDemo
{
    public class Player
    {
        private Animation idleAnimation;
        private Animation moveAnimation;
        private Animation jumpAnimation;

        private Vector2 position;
        private Vector2 velocity;
        private bool isOnGround;
        private bool isMoving;
        public int Lives { get; set; }

        // Flickering effect variables
        private bool isFlickering;
        private float flickerDuration = 1f; // Duration in seconds
        private float flickerTimer;
        private float flickerInterval = 0.1f; // Interval of flicker effect


        private float jumpVelocity = -6.5f; // Adjust the initial jump velocity
        private float gravity = 0.35f; // Adjust gravity as needed

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsFacingLeft { get; set; }
        public bool IsJumping { get; private set; }
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, CurrentFrameTexture.Width, CurrentFrameTexture.Height);

        public Player(Vector2 initialPosition, Texture2D[] idleFrames, Texture2D[] moveFrames, Texture2D[] jumpFrames)
        {
            position = initialPosition;
            idleAnimation = new Animation(idleFrames, 0.1f);
            moveAnimation = new Animation(moveFrames, 0.1f);
            jumpAnimation = new Animation(jumpFrames, 0.1f);
            isOnGround = false;
            isMoving = false;
            Lives = 3; // Player starts with 3 lives
        }

        public void Update(GameTime gameTime, List<Block> blocks, List<Enemy> enemies)
        {
            Vector2 movement = InputManager.UpdatePlayerMovement();
            isMoving = movement.X != 0;

            Vector2 newPosition = position + new Vector2(movement.X * 3f, 0);
            Rectangle newHorizontalBounds = new Rectangle((int)newPosition.X, (int)position.Y, BoundingBox.Width, BoundingBox.Height);
            bool horizontalCollision = CheckForBlockCollision(newHorizontalBounds, blocks);

            if (isMoving && !horizontalCollision)
            {
                HandleHorizontalMovement(movement, blocks, gameTime);
            }

            if (!isOnGround)
            {
                velocity.Y += gravity;
                position.Y += velocity.Y;
                isOnGround = false; // Reset to false before collision check
            }

            isOnGround = false;
            foreach (var block in blocks)
            {
                if (BoundingBox.Intersects(block.BoundingBox))
                {
                    HandleCollisionWithBlock(block);
                }
            }

            if (IsJumping)
            {
                jumpAnimation.Update(gameTime);
            }
            else if (isOnGround)
            {
                if (isMoving)
                {
                    moveAnimation.Update(gameTime);
                }
                else
                {
                    idleAnimation.Update(gameTime);
                }
            }

            if (InputManager.IsJumpKeyPressed() && isOnGround)
            {
                Jump();
            }

            foreach (var enemy in enemies)
            {
                if (IsJumpingOnEnemy(enemy))
                {
                    enemy.IsActive = false; // Deactivate the enemy
                }
                else if (enemy.IsActive && BoundingBox.Intersects(enemy.BoundingBox))
                {
                    LoseLife();
                }
            }

            // Flickering logic
            if (isFlickering)
            {
                flickerTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (flickerTimer >= flickerDuration)
                {
                    isFlickering = false;
                    flickerTimer = 0;
                }
            }
        }

        private void LoseLife()
        {
            Lives--;
            isFlickering = true;
            flickerTimer = 0;

            if (Lives <= 0)
            {
                // TODO: Handle game over logic
            }
            else
            {
                // TODO: Reset player to a safe position
                Respawn();
                
            }
        }

        private void Respawn()
        {
            Vector2 respawnPosition = new Vector2(100, 100);

            this.ResetPlayer(respawnPosition, Lives);
            isFlickering = true;
            flickerTimer = 0;
        }
        public bool IsOffScreen(int screenHeight)
        {
            return Position.Y > screenHeight; // Assuming Y increases downwards
        }

        private bool IsJumpingOnEnemy(Enemy enemy)
        {
            if (!enemy.IsActive) return false;

            // Check if the player's bottom is colliding with the top of the enemy
            return BoundingBox.Top < enemy.BoundingBox.Bottom &&
                   BoundingBox.Bottom > enemy.BoundingBox.Top &&
                   BoundingBox.Right > enemy.BoundingBox.Left &&
                   BoundingBox.Left < enemy.BoundingBox.Right &&
                   velocity.Y > 0; // Ensure the player is falling down
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Skip drawing the player on every other flicker interval if flickering
            if (!isFlickering || (isFlickering && flickerTimer % (flickerInterval * 2) < flickerInterval))
            {
                spriteBatch.Draw(CurrentFrameTexture, Position, null, Color.White, 0f, Vector2.Zero, 1.0f, IsFacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
        }

        private void HandleCollisionWithBlock(Block block)
        {
            Rectangle blockBox = block.BoundingBox;
            if (velocity.Y >= 0 && Position.Y + CurrentFrameTexture.Height <= blockBox.Top + velocity.Y + 1)
            {
                Position = new Vector2(Position.X, blockBox.Top - CurrentFrameTexture.Height);
                velocity.Y = 0;
                isOnGround = true;
                IsJumping = false;
            }
        }

        private void HandleHorizontalMovement(Vector2 movement, List<Block> blocks, GameTime gameTime)
        {
            float moveAmount = movement.X * 3f;
            Vector2 newPosition = position + new Vector2(moveAmount, 0);
            Rectangle newHorizontalBounds = new Rectangle((int)newPosition.X, (int)position.Y, BoundingBox.Width, BoundingBox.Height);

            if (!CheckForBlockCollision(newHorizontalBounds, blocks))
            {
                position.X += moveAmount; // Adjust movement speed as needed
                IsFacingLeft = movement.X < 0;
                moveAnimation.Update(gameTime);
            }
        }

        private bool CheckForBlockCollision(Rectangle boundingBox, List<Block> blocks)
        {
            foreach (var block in blocks)
            {
                if (boundingBox.Intersects(block.BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }

        public void ResetPlayer(Vector2 newPosition, int newLives)
        {
            Position = newPosition;
            Lives = newLives;
            // Any other reset logic, like resetting animation states
        }

        public Texture2D CurrentFrameTexture
        {
            get
            {
                if (IsJumping)
                {
                    return jumpAnimation.CurrentFrameTexture;
                }
                else if (isMoving)
                {
                    return moveAnimation.CurrentFrameTexture;
                }
                else
                {
                    return idleAnimation.CurrentFrameTexture;
                }
            }
        }

        private void Jump()
        {
            if (isOnGround)
            {
                IsJumping = true;
                isOnGround = false;
                velocity.Y = jumpVelocity;
            }
        }
    }
}
