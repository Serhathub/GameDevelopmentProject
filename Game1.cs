using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerDemo.Interfaces;
using PlatformerDemo.Terrain;
using PlatformerDemo.Terrain.PlatformerDemo.Terrain;

namespace PlatformerDemo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private GameState gameState;
        private Menu menu;
        private TerrainBuilder terrainBuilder;
        private Texture2D backgroundTextureGame;


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
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load background image
            Texture2D backgroundTexture = Content.Load<Texture2D>("Sample");

            backgroundTextureGame = Content.Load<Texture2D>("Background/background");

            // Load button images
            Texture2D startButtonTexture = Content.Load<Texture2D>("Menu/playbutton");
            Texture2D exitButtonTexture = Content.Load<Texture2D>("Menu/backbutton");

            // Load your texture arrays for idle, move, and jump frames
            Texture2D[] idleFrames = { Content.Load<Texture2D>("Player/tile_0040") };
            Texture2D[] moveFrames = { Content.Load<Texture2D>("Player/tile_0040"), Content.Load<Texture2D>("Player/tile_0041") };
            Texture2D[] jumpFrames = { Content.Load<Texture2D>("Player/tile_0045"), Content.Load<Texture2D>("Player/tile_0046") };

            // Create the player instance
            player = new Player(new Vector2(100, 100), idleFrames, moveFrames, jumpFrames);

            // Set the initial game state
            gameState = GameState.Menu;

            // Load font for the menu
            SpriteFont font = Content.Load<SpriteFont>("MenuFont");

            // Create the menu instance
            menu = new Menu(font, GraphicsDevice, backgroundTexture, startButtonTexture, exitButtonTexture);

            // Initialize Terrain
            IBlueprint blueprint = new Blueprint();
            terrainBuilder = new TerrainBuilder(blueprint);

            // Load terrain texture (assuming a single texture for all tiles)
            Texture2D terrainTexture = Content.Load<Texture2D>("Tiles/tilemap");
            terrainBuilder.LoadTerrain(terrainTexture);
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
                    player.Update(gameTime, terrainBuilder.Blocks); // Pass in the blocks for collision
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTextureGame, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(_spriteBatch);
                    break;

                case GameState.Playing:
                    terrainBuilder.DrawTerrain(_spriteBatch);
                    _spriteBatch.Draw(player.CurrentFrameTexture, player.Position, null, Color.White, 0f, Vector2.Zero, 1.0f, player.IsFacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                    //player.DrawBoundingBox(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}