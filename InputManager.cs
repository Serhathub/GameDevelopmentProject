using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace PlatformerDemo
{
    public static class InputManager
    {
        public static Vector2 UpdatePlayerMovement()
        {
            Vector2 movement = Vector2.Zero;

            // Example: Check for player movement
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.X = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.X = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                // Crouch behavior
                // Adjust player's size or trigger crouch animation, etc.
                // You may want to set a flag indicating that the player is crouching
            }

            return movement;
        }

        public static bool IsJumpKeyPressed()
        {
            // Example: Check for jumping
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }

        public static bool IsMovingLeft()
        {
            // Example: Check if the player is moving left
            return Keyboard.GetState().IsKeyDown(Keys.Left);
        }
    }
}


