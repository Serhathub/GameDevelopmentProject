using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerDemo.Interfaces;
using PlatformerDemo.Terrain;
using System.Collections.Generic;

namespace PlatformerDemo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private GameState gameState;
        private Menu menu;
        private Level currentLevel;
        private Texture2D backgroundTextureMenu;
        private Texture2D backgroundTextureGame;
        private Texture2D heartTexture;
        private HealthBar healthBar;
        private GameOverScreen gameOverScreen;

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

            // Load background images
            backgroundTextureMenu = Content.Load<Texture2D>("Sample");
            backgroundTextureGame = Content.Load<Texture2D>("Background/background");

            // Load button images
            Texture2D startButtonTexture = Content.Load<Texture2D>("Menu/playbutton");
            Texture2D exitButtonTexture = Content.Load<Texture2D>("Menu/backbutton");

            // Load player textures
            Texture2D[] idleFrames = { Content.Load<Texture2D>("Player/tile_0040") };
            Texture2D[] moveFrames = { Content.Load<Texture2D>("Player/tile_0040"), Content.Load<Texture2D>("Player/tile_0041") };
            Texture2D[] jumpFrames = { Content.Load<Texture2D>("Player/tile_0045"), Content.Load<Texture2D>("Player/tile_0046") };

            // Create the player instance
            player = new Player(new Vector2(100, 100), idleFrames, moveFrames, jumpFrames);

            // Set the initial game state
            gameState = GameState.Menu;

            // Load font for the menu
            SpriteFont font = Content.Load<SpriteFont>("MenuFont");

            // Create the menu and game over instances
            menu = new Menu(font, GraphicsDevice, backgroundTextureMenu, startButtonTexture, exitButtonTexture);
            gameOverScreen = new GameOverScreen(Content, GraphicsDevice);

            // Load heart texture and create health bar
            heartTexture = Content.Load<Texture2D>("Menu/heart");
            healthBar = new HealthBar(heartTexture, new Vector2(10, 10));

            // Initialize the first level
            currentLevel = new Level(GraphicsDevice, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    if (menu.SelectedOption == "Start")
                    {
                        gameState = GameState.Playing;
                    }
                    else if (menu.SelectedOption == "Exit")
                    {
                        Exit();
                    }
                    break;

                case GameState.Playing:
                    player.Update(gameTime, currentLevel.TerrainBlocks, currentLevel.Enemies);
                    currentLevel.Update(gameTime);

                    if ((player.IsOffScreen(_graphics.PreferredBackBufferHeight) || player.Lives <= 0) && gameState != GameState.GameOver)
                    {
                        gameState = GameState.GameOver;
                    }
                    break;

                case GameState.GameOver:
                    if (gameOverScreen.Update(gameTime))
                    {
                        ResetGame();
                        gameState = GameState.MenuTransition; // Add an intermediate state for transition
                    }
                    break;

                case GameState.MenuTransition:
                    // Check for key release before showing the menu
                    if (!Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.Menu;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        private void ResetGame()
        {
            // Reset player state
            player.ResetPlayer(new Vector2(100, 100), 3); // Assuming this method resets the player's position and lives

            // Reset the menu's selected option
            menu.ResetSelectedOption(); // You'll need to add this method in the Menu class

            // Any other game elements that need to be reset
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:
                    _spriteBatch.Draw(backgroundTextureMenu, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    menu.Draw(_spriteBatch);
                    break;

                case GameState.Playing:
                    _spriteBatch.Draw(backgroundTextureGame, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    currentLevel.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    healthBar.Draw(_spriteBatch, player.Lives);
                    break;

                case GameState.GameOver:
                    gameOverScreen.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
