using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerDemo
{
    public class Player
    {
        private Animation idleAnimation;
        private Animation moveAnimation;
        private Animation jumpAnimation;

        private Vector2 position;
        private Vector2 velocity;
        private float jumpVelocity = -8f; // Adjust the initial jump velocity
        private float gravity = 0.4f; // Adjust gravity as needed
        private bool isOnGround = true; // Flag to track whether the player is on the ground

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private bool isFacingLeft = false;

        public bool IsFacingLeft
        {
            get { return isFacingLeft; }
            set { isFacingLeft = value; }
        }

        public bool IsJumping { get; set; }

        public Player(Vector2 initialPosition, Texture2D[] idleFrames, Texture2D[] moveFrames, Texture2D[] jumpFrames)
        {
            Position = initialPosition;
            idleAnimation = new Animation(idleFrames, 0.1f);
            moveAnimation = new Animation(moveFrames, 0.1f);
            jumpAnimation = new Animation(jumpFrames, 0.1f);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 movement = InputManager.UpdatePlayerMovement();

            if (movement.X != 0)
            {
                // Player is moving
                position.X += movement.X * 3f; // Adjust movement speed as needed

                // Determine the animation based on movement direction
                if (movement.X > 0)
                {
                    IsFacingLeft = false;
                }
                else
                {
                    IsFacingLeft = true;
                }

                moveAnimation.Update(gameTime);
            }
            else if (IsJumping)
            {
                // Player is jumping
                jumpAnimation.Update(gameTime);

                // Allow small adjustments to the player's position while jumping
                position.X += movement.X * 1.5f; // Adjust the factor as needed

                // Update player position based on vertical velocity
                position.Y += velocity.Y;

                // Apply gravity to simulate falling
                velocity.Y += gravity; // Adjust gravity as needed

                // Check if the player has landed (reached the ground)
                if (position.Y >= 100) // Adjust the ground level as needed
                {
                    // Reset jumping state
                    IsJumping = false;
                    isOnGround = true; // Set the flag to true when the player lands
                    position.Y = 100; // Adjust the ground level as needed
                    velocity.Y = 0;
                    jumpAnimation.Reset(); // Reset the jump animation to the beginning
                }
            }
            else
            {
                // Player is idle
                idleAnimation.Update(gameTime);
            }

            if (InputManager.IsJumpKeyPressed() && isOnGround)
            {
                Jump();
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
            IsJumping = true;
            isOnGround = false; // Set the flag to false when the player jumps
            // Set an initial vertical velocity for the jump
            velocity.Y = jumpVelocity;
        }
    }
}










