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
        }

        public void Update(GameTime gameTime, List<Block> blocks)
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

        public void DrawBoundingBox(SpriteBatch spriteBatch)
        {
            Rectangle box = BoundingBox;
            Texture2D rectTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            rectTexture.SetData(new[] { Color.Red });

            spriteBatch.Draw(rectTexture, box, Color.White * 0.5f); // Semi-transparent red box
        }
    }
}
