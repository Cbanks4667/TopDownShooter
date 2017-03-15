using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace TopDownShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Music
        private bool sceneChanged = true;
        private bool inMenu = true;
        private Song menuMusic;
        private Song gameMusic;

        //Scenes
        private StartScene startScene;
        private ActionScene actionScene;
        private ControlsScene controlsScene;
        private GameDescriptionScene gameDescriptionScene;
        private HighscoreScene highscoreScene;
        private CreditsScene creditsScene;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            Shared.CreatePositions(Shared.stage);
            Shared.CreatePatterns();
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //LOAD BACKGROUND MUSIC------------------------------
            menuMusic = Content.Load<Song>("music/Lost signal main theme (WIP)");
            gameMusic = Content.Load<Song>("music/Hero Immortal_Trevor Lentz");
            MediaPlayer.IsRepeating = true;


            //LOAD SCENES----------------------------------

            //startScene
            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            //actionScene
            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            //controlsScene
            controlsScene = new ControlsScene(this, spriteBatch);
            this.Components.Add(controlsScene);

            //gameDescriptionScene
            gameDescriptionScene = new GameDescriptionScene(this, spriteBatch);
            this.Components.Add(gameDescriptionScene);

            //highscoreScene
            highscoreScene = new HighscoreScene(this, spriteBatch);
            this.Components.Add(highscoreScene);

            //creditsScene
            creditsScene = new CreditsScene(this, spriteBatch);
            this.Components.Add(creditsScene);

            //Show only startScene on Load
            startScene.show();
            startScene.Menu.show();
            
        }

        /// <summary>
        /// Hides all game scenes
        /// </summary>
        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in this.Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            //SHOW and HIDE MENU here based on startScene selectedIndex
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    inMenu = false;
                    sceneChanged = true;
                    hideAllScenes();
                    actionScene.show();
                }
                    if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    controlsScene.show();
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    gameDescriptionScene.show();
                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    highscoreScene.show();
                }
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    creditsScene.show();
                }
                if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            //if action scene is enabled take care of navigation
            if (actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    inMenu = true;
                    sceneChanged = true;
                    hideAllScenes();
                    startScene.show();
                }
            }
            else if (controlsScene.Enabled || gameDescriptionScene.Enabled || highscoreScene.Enabled || creditsScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }

            //Plays Music based on current scene
            if (sceneChanged == true)
            {
                MediaPlayer.Stop();
                if (inMenu == true)
                {
                    MediaPlayer.Play(menuMusic);
                }
                else if (inMenu == false)
                {
                    MediaPlayer.Play(gameMusic);
                }
                sceneChanged = false;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
