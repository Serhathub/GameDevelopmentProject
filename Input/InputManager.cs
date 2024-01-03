using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace PlatformerDemo.Input
{
    public static class InputManager
    {
        public static Vector2 UpdatePlayerMovement(bool allowPassThrough = false)
        {
            Vector2 movement = Vector2.Zero;


            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.X = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.X = -1;
            }
            if (allowPassThrough && Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                // Crouch 
            }

            return movement;
        }

        public static bool IsJumpKeyPressed()
        {
            var keyboardState = Keyboard.GetState();
            return keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Up);
        }

        public static bool IsMovingLeft()
        {
            var keyboardState = Keyboard.GetState();
            return keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A);
        }
    }
}


