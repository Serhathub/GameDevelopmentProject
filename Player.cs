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

        private float jumpVelocity = -8f; // Adjust the initial jump velocity
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
        }

        public void Update(GameTime gameTime, List<Block> blocks)
        {
            Vector2 movement = InputManager.UpdatePlayerMovement();

            if (movement.X != 0)
            {
                // Player is moving
                position.X += movement.X * 3f; // Adjust movement speed as needed

                // Determine the animation based on movement direction
                IsFacingLeft = movement.X < 0;
                moveAnimation.Update(gameTime);
            }
            else
            {
                // Player is idle
                idleAnimation.Update(gameTime);
            }

            // Handle jumping
            if (IsJumping)
            {
                // Allow small adjustments to the player's position while jumping
                position.X += movement.X * 1.5f; // Adjust the factor as needed
                jumpAnimation.Update(gameTime);
            }

            // Apply gravity
            if (!isOnGround)
            {
                velocity.Y += gravity;
                position.Y += velocity.Y;
            }

            // Check for collisions
            isOnGround = false; // Assume player is not on the ground until we find a collision
            foreach (var block in blocks)
            {
                if (BoundingBox.Intersects(block.BoundingBox))
                {
                    HandleCollisionWithBlock(block);
                }
            }

            // If no collision and player is not jumping, player should be falling
            if (!isOnGround && !IsJumping)
            {
                IsJumping = true;
            }

            // Jump logic
            if (InputManager.IsJumpKeyPressed() && isOnGround)
            {
                Jump();
            }
        }

        private void HandleCollisionWithBlock(Block block)
        {
            // Calculate how much we need to move the player up to prevent overlapping.
            // This is a very simplistic collision response for the top of the blocks only.
            Rectangle blockBox = block.BoundingBox;
            if (velocity.Y > 0 && Position.Y + CurrentFrameTexture.Height <= blockBox.Top + velocity.Y)
            {
                Position = new Vector2(Position.X, blockBox.Top - CurrentFrameTexture.Height);
                velocity.Y = 0;
                isOnGround = true;
                IsJumping = false;
            }
        }

        public Texture2D CurrentFrameTexture
        {
            get
            {
                if (IsJumping)
                {
                    return jumpAnimation.CurrentFrameTexture;
                }
                else if (InputManager.UpdatePlayerMovement().X != 0)
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
