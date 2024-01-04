using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using PlatformerDemo.Entities;
using PlatformerDemo.Interfaces;
using PlatformerDemo.Levels;
using PlatformerDemo.States;
namespace PlatformerDemo
{
    //LSP - Liskov Substitution Principle (LSP):
    //Level en Level2 kunnen door elkaar worden gebruikt waar ILevel wordt verwacht, in overeenstemming met LSP.
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private GameState gameState;
        private Menu menu;
        private ILevel currentLevel; //LSP
        private Texture2D backgroundTextureMenu;
        private Texture2D backgroundTextureGame;
        private Texture2D heartTexture;
        private HealthBar healthBar;
        private GameOverScreen gameOverScreen;
        private VictoryScreen victoryScreen;
        private bool isSecondLevel;
        Song backgroundMusic;
        SoundEffect jumpSound;
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

            backgroundTextureMenu = Content.Load<Texture2D>("Sample");
            backgroundTextureGame = Content.Load<Texture2D>("Background/background");
            backgroundMusic = Content.Load<Song>("Music/theme");
            jumpSound = Content.Load<SoundEffect>("Music/jump");

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;

            Texture2D startButtonTexture = Content.Load<Texture2D>("Menu/playbutton");
            Texture2D exitButtonTexture = Content.Load<Texture2D>("Menu/backbutton");

            Texture2D[] idleFrames = { Content.Load<Texture2D>("Player/tile_0040") };
            Texture2D[] moveFrames = { Content.Load<Texture2D>("Player/tile_0040"), Content.Load<Texture2D>("Player/tile_0041") };
            Texture2D[] jumpFrames = { Content.Load<Texture2D>("Player/tile_0045"), Content.Load<Texture2D>("Player/tile_0046") };

            player = new Player(new Vector2(100, 100), idleFrames, moveFrames, jumpFrames);

            gameState = GameState.Menu;

            SpriteFont font = Content.Load<SpriteFont>("MenuFont");

            menu = new Menu(font, GraphicsDevice, backgroundTextureMenu, startButtonTexture, exitButtonTexture);
            gameOverScreen = new GameOverScreen(Content, GraphicsDevice);
            victoryScreen = new VictoryScreen(Content, GraphicsDevice);

            heartTexture = Content.Load<Texture2D>("Menu/heart");
            healthBar = new HealthBar(heartTexture, new Vector2(10, 10));

            currentLevel = new Level(GraphicsDevice, Content); //LSP
            isSecondLevel = false;
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
                    player.Update(gameTime, currentLevel.TerrainBlocks, currentLevel.Enemies, jumpSound);
                    currentLevel.Update(gameTime);

                    if (player.Position.X >= _graphics.PreferredBackBufferWidth - player.BoundingBox.Width)
                    {
                        if (isSecondLevel)
                        {
                            gameState = GameState.VictoryScreen;
                        }
                        else
                        {
                            TransitionToNextLevel();
                        }
                    }

                    if ((player.IsOffScreen(_graphics.PreferredBackBufferHeight) || player.Lives <= 0) && gameState != GameState.GameOver)
                    {
                        player.ResetPlayer(new Vector2(100, 100), player.Lives - 1);
                    }

                    if (player.Lives <= 0)
                    {
                        gameState = GameState.GameOver;
                    }
                    break;

                case GameState.VictoryScreen:
                    if (gameOverScreen.Update(gameTime))
                    {
                        ResetGame();
                        gameState = GameState.MenuTransition;
                    }
                    break;

                case GameState.GameOver:
                    if (gameOverScreen.Update(gameTime))
                    {
                        ResetGame();
                        gameState = GameState.MenuTransition;
                    }
                    break;

                case GameState.MenuTransition:
                    if (!Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.Menu;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        private void TransitionToNextLevel()
        {
            currentLevel = new Level2(GraphicsDevice, Content);
            player.ResetPlayer(new Vector2(0, 100), 3);
            isSecondLevel = true;
        }

        private void ResetGame()
        {
            player.ResetPlayer(new Vector2(100, 100), 3);
            foreach (var enemy in currentLevel.Enemies)
            {
                enemy.Reset();
                enemy.IsActive = true;
            }
            menu.ResetSelectedOption();
            currentLevel = new Level(GraphicsDevice, Content);
            isSecondLevel = false;
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

                case GameState.VictoryScreen:
                    victoryScreen.Draw(_spriteBatch);
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
