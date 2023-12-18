using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace PlatformerDemo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private GameState gameState;
        private Menu menu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 360;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load background image
            Texture2D backgroundTexture = Content.Load<Texture2D>("Sample");

            // Load button images
            Texture2D startButtonTexture = Content.Load<Texture2D>("Menu/playbutton");
            Texture2D exitButtonTexture = Content.Load<Texture2D>("Menu/backbutton");


            // Load your texture arrays for idle, move, and jump frames
            Texture2D[] idleFrames = new Texture2D[] { Content.Load<Texture2D>("Player/tile_0040") };
            Texture2D[] moveFrames = new Texture2D[] { Content.Load<Texture2D>("Player/tile_0040"), Content.Load<Texture2D>("Player/tile_0041"), Content.Load<Texture2D>("Player/tile_0042") };
            Texture2D[] jumpFrames = new Texture2D[] { Content.Load<Texture2D>("Player/tile_0045"), Content.Load<Texture2D>("Player/tile_0046") };

            // Create the player instance
            player = new Player(new Vector2(100, 100), idleFrames, moveFrames, jumpFrames);
            
            // Set the initial game state
            gameState = GameState.Menu;

            // Load font for the menu
            SpriteFont font = Content.Load<SpriteFont>("MenuFont");

            // Create the menu instance
            menu = new Menu(font, GraphicsDevice, backgroundTexture, startButtonTexture, exitButtonTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.Menu:
                    // Update menu
                    menu.Update(gameTime);

                    // Check for menu interactions (e.g., selecting "Start")
                    if (menuIsStartSelected())  // Add a method to check if "Start" is selected
                    {
                        gameState = GameState.Playing;
                    }
                    else if(IsExitSelected())
                    {
                        Exit();
                    }

                    break;

                case GameState.Playing:
                    // Update player
                    player.Update(gameTime);

                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            float scale = 2f;

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(_spriteBatch);

                    break;

                case GameState.Playing:
                    // Draw the player using the loaded sprite sheet and updated position
                    _spriteBatch.Draw(player.CurrentFrameTexture, player.Position, null, Color.White, 0f, Vector2.Zero, scale, player.IsFacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool menuIsStartSelected()
        {
            return menu.SelectedOption == "Start";
        }
        private bool IsExitSelected()
        {
            return menu.SelectedOption == "Exit";
        }


    }
}




