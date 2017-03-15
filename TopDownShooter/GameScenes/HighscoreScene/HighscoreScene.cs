using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
namespace TopDownShooter
{
    class HighscoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        private int selectedIndex = 0;
        private KeyboardState oldState;
        private Game game;
        private List<HighscoreComponent> levelHighscores = new List<HighscoreComponent>();
        private HighscoreComponent levelOneHighscores;
        private HighscoreComponent levelTwoHighscores;
        string[] levelOneScores;
        string[] levelTwoScores;
        private string levelOneTextFile = "highscoreLevelOne.txt";
        private string levelTwoTextFile = "highscoreLevelTwo.txt";

        #region HighscoreScene Methods
        /// <summary>
        /// Reads a highscores text file and returns a string array 
        /// </summary>
        /// <param name="highscoreFile">the file name</param>
        /// <returns>A string array</returns>
        private string[] ReadFileToArray(string highscoreFile)
        {
            string[] highscoreArray = new string[11];
            try
            {
                System.IO.Stream fileStream = TitleContainer.OpenStream(highscoreFile);
                System.IO.StreamReader reader = new System.IO.StreamReader(fileStream);

                for (int i = 0; i < highscoreArray.Length; i++)
                {
                    highscoreArray[i] = reader.ReadLine();
                }

                Console.WriteLine("File Size: " + fileStream.Length);
                fileStream.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                // this will be thrown by OpenStream if gamedata.txt
                // doesn't exist in the title storage location
            }
            return highscoreArray;
        } 
        #endregion

        public HighscoreScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("images/menuBackgrounds/highscoreSceneBackground");
            this.game = game;
            //reads level highscore text files
            HighScoreDraw(game, spriteBatch);
            this.Components.Add(levelOneHighscores);
            levelHighscores.Add(levelOneHighscores);
            this.Components.Add(levelTwoHighscores);
            levelHighscores.Add(levelTwoHighscores);
        }

        public void HighScoreDraw(Game game, SpriteBatch spriteBatch)
        {

            levelOneScores = File.ReadAllLines(levelOneTextFile);
            //File.CreateText("tester.txt");
            //File.AppendAllLines("tester.txt", levelOneScores);
           
            levelTwoScores = ReadFileToArray(levelTwoTextFile);

            //Draws level one highscore values
            levelOneHighscores = new HighscoreComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("fonts/ArialBold-50"),
                game.Content.Load<SpriteFont>("fonts/ArialBold-60"),
                levelOneScores);
           

            //Draws level two highscore values
            levelTwoHighscores = new HighscoreComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("fonts/ArialBold-50"),
                game.Content.Load<SpriteFont>("fonts/ArialBold-60"),
                levelTwoScores);
            
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            HighScoreDraw(game, spriteBatch);
            //Changing & looping selectedIndex
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                selectedIndex++;
                if (selectedIndex == levelHighscores.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = levelHighscores.Count - 1;
                }
            }
            oldState = ks;

            //shows and hides level highscores based on selectedIndex
            for (int i = 0; i < levelHighscores.Count; i++)
            {
                if (selectedIndex == i)
                {
                    levelHighscores[i].show();
                }
                else
                {
                    levelHighscores[i].hide();
                }

            }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
