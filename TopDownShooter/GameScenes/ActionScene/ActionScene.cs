using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace TopDownShooter
{
    #region ...............added asteroid  and powerups end of level animation..........................
    public class ActionScene : GameScene
    {
        #region ActionScene Global Variables
        int checkAndSaveCount;
        public bool GameOver = false;
        CollisionManager cm;
        Asteroid tempAsteroid;
        EnemyShips temp;
        private bool playMusic = false;
        Vector2 DestinationPos = Shared.Positions[Shared.STAGE_CENTER];
        private Texture2D myBackGroundTex; 
        private SpriteBatch spriteBatch;
        private SpriteFont scoreFont;
        protected int currentLevel;
        private int ScreenChange;       
        protected Vector2 stage;
        private PlayerShip myShip;
        private List<Projectile> missiles = new List<Projectile>();
        private List<Projectile> enemyFire = new List<Projectile>();
        private List<EnemyShips> allEnemeys = new List<EnemyShips>();
        private Queue<EnemyShips> firstWave = new Queue<EnemyShips>();
        private Queue<EnemyShips> secondWave = new Queue<EnemyShips>();
        private Texture2D[] myMissileTex = new Texture2D[11];
        private Game myGame;
        private KeyboardState oldState;
        private EnemyShips myEnemy;
        private Rectangle scrollingStars1 = new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y);
        private Rectangle scrollingStars2 =  new Rectangle(0, -(int)Shared.stage.Y, (int)Shared.stage.X, (int)Shared.stage.Y);
        private Rectangle scrollingOverlay1 = new Rectangle(0, 10800-1080, 1920, 10800);
        private Rectangle scrollingOverlay2 = new Rectangle(0, -10800, 1920, 1080);
        private Texture2D overlayTex, overlayTex2, gameOverTex, levelCompleteTex;
        Rectangle Screen = new Rectangle(-50, -50, (int)Shared.stage.X + 50, (int)Shared.stage.Y + 50);
        private SoundEffect[] mySounds = new SoundEffect[22];
        private SoundEffect[] atariBoomSounds = new SoundEffect[6];
        private SoundEffect[] boomSounds = new SoundEffect[9];
        private Queue<Asteroid> meteors = new Queue<Asteroid>();
        private Texture2D[] enemyShipTex = new Texture2D[3];
        Boss myBoss;

        //BlackHole
        private Texture2D blackHoleTex;

        #region Read and Write Highscore Methods
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

                //Console.WriteLine("File Size: " + fileStream.Length);
                fileStream.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                // this will be thrown by OpenStream if gamedata.txt
                // doesn't exist in the title storage location
            }
            return highscoreArray;
        }

        /// <summary>
        /// Writes a highscores text file given a list
        /// </summary>
        /// <param name="highscoreFile">the file name</param>
        /// <param name="scores">a list of scores</param>
        /// <param name="levelName">the level name</param>
        private void WriteFileToArray(string highscoreFile, List<int> scores, string levelName)
        {
            try
            {
                //System.IO.Stream fileStream = TitleContainer.OpenStream(highscoreFile);
                //System.IO.StreamWriter writer = new System.IO.StreamWriter(fileStream);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(highscoreFile);
                writer.WriteLine(levelName);
                for (int i = 0; i < scores.Count; i++)
                {
                    writer.WriteLine(scores[i].ToString());
                }

                //Console.WriteLine("File Size: " + fileStream.Length);
                writer.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                // this will be thrown by OpenStream if gamedata.txt
                // doesn't exist in the title storage location

            }
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.GetBaseException());
            //}
        }
        #endregion


        #endregion
        #region Properties
        public Texture2D[] MyMissileTex
        {
            get
            {
                return myMissileTex;
            }

            set
            {
                myMissileTex = value;
            }
        }

        public Rectangle ScrollingOverlay2
        {
            get
            {
                return scrollingOverlay2;
            }

            set
            {
                scrollingOverlay2 = value;
            }
        }
        #endregion

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            
            this.spriteBatch = spriteBatch;
            this.myGame = game;

            #region Load SoundEffects
            this.mySounds[0] = myGame.Content.Load<SoundEffect>("soundEffects/activate_1");
            this.mySounds[1] = myGame.Content.Load<SoundEffect>("soundEffects/alarm_1");
            this.mySounds[2] = myGame.Content.Load<SoundEffect>("soundEffects/complain_1");
            this.mySounds[3] = myGame.Content.Load<SoundEffect>("soundEffects/creature_1");
            this.mySounds[4] = myGame.Content.Load<SoundEffect>("soundEffects/creature_2");
            this.mySounds[5] = myGame.Content.Load<SoundEffect>("soundEffects/creature_3");
            this.mySounds[6] = myGame.Content.Load<SoundEffect>("soundEffects/creature_4");
            this.mySounds[7] = myGame.Content.Load<SoundEffect>("soundEffects/danger");
            this.mySounds[8] = myGame.Content.Load<SoundEffect>("soundEffects/demonic_1");
            this.mySounds[9] = myGame.Content.Load<SoundEffect>("soundEffects/flight_1");
            this.mySounds[10] = myGame.Content.Load<SoundEffect>("soundEffects/flight_maneuver_1");
            this.mySounds[11] = myGame.Content.Load<SoundEffect>("soundEffects/flight_maneuver_2");
            this.mySounds[12] = myGame.Content.Load<SoundEffect>("soundEffects/global-sprite");
            this.mySounds[13] = myGame.Content.Load<SoundEffect>("soundEffects/health_1");
            this.mySounds[14] = myGame.Content.Load<SoundEffect>("soundEffects/laser_gun_1");
            this.mySounds[15] = myGame.Content.Load<SoundEffect>("soundEffects/laser_gun_2");
            this.mySounds[16] = myGame.Content.Load<SoundEffect>("soundEffects/laser1");
            this.mySounds[17] = myGame.Content.Load<SoundEffect>("soundEffects/magnetic_field_1");
            this.mySounds[18] = myGame.Content.Load<SoundEffect>("soundEffects/magnetic_field_2");
            this.mySounds[19] = myGame.Content.Load<SoundEffect>("soundEffects/magnetic_field_3");
            this.mySounds[20] = myGame.Content.Load<SoundEffect>("soundEffects/magnetic_field_4");
            this.mySounds[21] = myGame.Content.Load<SoundEffect>("soundEffects/wind_1");

            this.atariBoomSounds[0] = myGame.Content.Load<SoundEffect>("soundEffects/atari_boom");
            this.atariBoomSounds[1] = myGame.Content.Load<SoundEffect>("soundEffects/atari_boom2");
            this.atariBoomSounds[2] = myGame.Content.Load<SoundEffect>("soundEffects/atari_boom3");
            this.atariBoomSounds[3] = myGame.Content.Load<SoundEffect>("soundEffects/atari_boom4");
            this.atariBoomSounds[4] = myGame.Content.Load<SoundEffect>("soundEffects/atari_boom5");
            this.atariBoomSounds[5] = myGame.Content.Load<SoundEffect>("soundEffects/atari_boom6");

            this.boomSounds[0] = myGame.Content.Load<SoundEffect>("soundEffects/boom1");
            this.boomSounds[1] = myGame.Content.Load<SoundEffect>("soundEffects/boom2");
            this.boomSounds[2] = myGame.Content.Load<SoundEffect>("soundEffects/boom3");
            boomSounds[3] = myGame.Content.Load<SoundEffect>("soundEffects/boom4");
            boomSounds[4] = myGame.Content.Load<SoundEffect>("soundEffects/boom5");
            boomSounds[5] = myGame.Content.Load<SoundEffect>("soundEffects/boom6");
            boomSounds[6] = myGame.Content.Load<SoundEffect>("soundEffects/boom7");
            boomSounds[7] = myGame.Content.Load<SoundEffect>("soundEffects/boom8");
            boomSounds[8] = myGame.Content.Load<SoundEffect>("soundEffects/boom9");
            #endregion

            
        
            cm = new CollisionManager(game,mySounds,atariBoomSounds,boomSounds,spriteBatch);
            this.Components.Add(cm);
            Shared.CreateFormations(16);

            #region Asteroid Texture
            Texture2D meteorTex = game.Content.Load<Texture2D>("images/miscObjects/asteroid");

            #endregion

            #region Load Ship Textures
            Texture2D myShipTex = game.Content.Load<Texture2D>("images/ships/PlayerShip(1300x1100)");
                enemyShipTex[0] = game.Content.Load<Texture2D>("images/ships/enemyShip_small(80x80)160x80");
                enemyShipTex[1] = game.Content.Load<Texture2D>("images/ships/enemyShip_medium(160x160)320x160");
                enemyShipTex[2] = game.Content.Load<Texture2D>("images/ships/enemyShip_large(160x260)");
            #endregion

            #region Projectile Textures
                myMissileTex[0] = game.Content.Load<Texture2D>("images/projectiles/missile");
                myMissileTex[1] = game.Content.Load<Texture2D>("images/projectiles/laserLarge_blue");
                myMissileTex[2] = game.Content.Load<Texture2D>("images/projectiles/laserLarge_red");
                myMissileTex[3] = game.Content.Load<Texture2D>("images/projectiles/laserMedium_blue");
                myMissileTex[4] = game.Content.Load<Texture2D>("images/projectiles/laserMedium_red");
                myMissileTex[5] = game.Content.Load<Texture2D>("images/projectiles/laserSmall_green");
                myMissileTex[6] = game.Content.Load<Texture2D>("images/projectiles/laserSmall_red");
                myMissileTex[7] = game.Content.Load<Texture2D>("images/projectiles/torpDouble_red");
                myMissileTex[8] = game.Content.Load<Texture2D>("images/projectiles/torpDouble_yellow");
                myMissileTex[9] = game.Content.Load<Texture2D>("images/projectiles/torpSingle_red");
                myMissileTex[10] = game.Content.Load<Texture2D>("images/projectiles/torpSingle_yellow");
            #endregion

            #region MyShip
            Vector2 myShipPosition = Shared.Positions[Shared.STAGE_CENTER];
            Vector2 myShipSpeed = new Vector2(7, 7);
            myShip = new PlayerShip(game,this, spriteBatch, myShipTex, .75f, 0f, myShipPosition, myShipSpeed);
            this.Components.Add(myShip);
            cm.addPlayer(myShip);
            #endregion 
            
            #region EnemyShips
            int numberOfShips = 8;
            for (int x = 0; x < numberOfShips; x++)
            {
                myEnemy = new EnemyShips(game, spriteBatch, enemyShipTex[(int)EnemyShips.size.Small], 1f, 0f, Shared.Positions[Shared.STAGE_BOTTOM_LEFT], new Vector2(3, 3), new Vector2[] { Shared.Positions[Shared.STAGE_ABOVE_BOTTOM_LEFT_CENTER], Shared.Positions[Shared.STAGE_CENTER_LEFT], Shared.Positions[Shared.STAGE_TOP_CENTER], new Vector2(-10, -100) }, EnemyShips.size.Small);
                myEnemy.Enabled = false;
                myEnemy.Visible = false;
                firstWave.Enqueue(myEnemy);
                this.Components.Add(myEnemy);
                cm.addEnemy(myEnemy);
            }
            for (int x = 0; x < numberOfShips; x++)
            {
                myEnemy = new EnemyShips(game, spriteBatch, enemyShipTex[(int)EnemyShips.size.Medium], 1f, 0f, Shared.Positions[Shared.STAGE_BOTTOM_RIGHT], new Vector2(3, 3), new Vector2[] { Shared.Positions[Shared.STAGE_ABOVE_BOTTOM_LEFT_CENTER], Shared.Positions[Shared.STAGE_CENTER_LEFT], Shared.Positions[Shared.STAGE_TOP_CENTER], new Vector2(-10, -100) }, EnemyShips.size.Medium);
                myEnemy.Enabled = false;
                myEnemy.Visible = false;
                secondWave.Enqueue(myEnemy);
                this.Components.Add(myEnemy);
                cm.addEnemy(myEnemy);
            }
            #endregion
            
            #region Asteroids
            int numberOfAsteroids = 30;
            Random r = new Random();
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                int sign; 
                int n = r.Next(0, 1);               
                if (n == 0)
                {
                    sign = -1; 
                }
                else
                {
                    sign = 1;
                }
                
                Asteroid meteor = new Asteroid(game, spriteBatch, meteorTex, .5f, 0f, Shared.Positions[r.Next(0,25)], new Vector2(r.Next(1, 3) * sign, r.Next(1, 5)));
                meteor.Enabled = false;
                meteor.Visible = false;
                meteors.Enqueue(meteor);
                this.Components.Add(meteor);
                cm.addAsteroid(meteor);
            }

            #endregion

            #region BlackHoles
            blackHoleTex = game.Content.Load<Texture2D>("images/miscObjects/blackhole");
            //2.4F rotation not working
            BlackHole blackHole = new BlackHole(game, spriteBatch, blackHoleTex, .75F, 0F, new Vector2(Shared.stage.X / 2 - blackHoleTex.Width / 2, Shared.stage.Y / -blackHoleTex.Height / 2), new Vector2(1, 1));
            this.Components.Add(blackHole);
            cm.addBlackHole(blackHole);
            #endregion


            Health healthbar = new Health(game, spriteBatch, myShip,Health.healthType.player);
            this.Components.Add(healthbar);
            
            #region Background Textures
            myBackGroundTex = game.Content.Load<Texture2D>("images/levelBackgrounds/DistantStarsBackground");
            overlayTex = game.Content.Load<Texture2D>("images/levelBackgrounds/levelOneTransparentBackground");
            overlayTex2 = game.Content.Load<Texture2D>("images/levelBackgrounds/levelOneFinalBackground");
            gameOverTex = game.Content.Load<Texture2D>("images/levelBackgrounds/GameOverScreen");
            levelCompleteTex = game.Content.Load<Texture2D>("images/levelBackgrounds/levelCompleteScreen");
            #endregion

            //ADDED THIS WEEKED by Andrew
            scoreFont = game.Content.Load<SpriteFont>("fonts/ArialBold-20");
            

            Lives myLives = new Lives(game, spriteBatch, myShip, scoreFont, healthbar);
            this.Components.Add(myLives);

        }

        public override void Initialize()
        {
            

            base.Initialize();
          
        }
        public override void Update(GameTime gameTime)
        {

            if (cm.ExplodeList.Count > 0)
            {
                this.Components.Add(cm.ExplodeList[0]);
                cm.ExplodeList.Clear();
            }

            if (cm.actionPowers.Count > 0)
            {
                for (int i = 0; i < cm.actionPowers.Count; i++)
                {
                    this.Components.Add(cm.actionPowers[i]);
                }
                
                cm.actionPowers.Clear();

            }
            
            
            KeyboardState ks = Keyboard.GetState();

            #region Create Wave of Asteroids
            if (tempAsteroid == null && meteors.Count> 0)
            {
                tempAsteroid = meteors.Dequeue();
                tempAsteroid.Enabled = true;
                tempAsteroid.Visible = true;

            }
            else
            {
                if (meteors.Count == 0 || Vector2.Distance(tempAsteroid.Position, meteors.Peek().Position) > meteors.Peek().Radius * 5.1)
                {
                    tempAsteroid = null;
                }
            }
            #endregion

            #region Create Waves of EnemyShips

            if (temp == null && firstWave.Count > 0)
            {
                temp = firstWave.Dequeue();                            
                temp.Enabled = true;
                temp.Visible = true;
                //secondWave.Enqueue(temp);
                //firstWave.Enqueue(temp);
                allEnemeys.Add(temp);
            }
            else
            {               
                    if (firstWave.Count == 0 || Vector2.Distance(temp.Position, firstWave.Peek().Position) > firstWave.Peek().Radius * 3.1)
                    {
                        temp = null;
                    }
               
            }

            if (ScreenChange >= Shared.stage.Y)
            {
                ScreenChange = 0;
                if (temp == null && secondWave.Count > 0)
                {
                    temp = secondWave.Dequeue();


                    temp.Enabled = true;
                    temp.Visible = true;
                    //secondWave.Enqueue(temp);
                    //firstWave.Enqueue(temp);
                    allEnemeys.Add(temp);
                }
                else
                {
                    if (secondWave.Count == 0 || Vector2.Distance(temp.Position, secondWave.Peek().Position) > secondWave.Peek().Radius * 3.1)
                    {
                        temp = null;
                       // ScreenChange = 0;
                    }

                } 
            }
            else 
            {
                ScreenChange += 10;
            }


            if (allEnemeys.Count > 0)
            {
                #region Handles EnemyShip projectiles
                foreach (EnemyShips ship in allEnemeys)
                {
                    if (ship.Fire)
                    {
                        Triangle.CreateTriangle(ship.Position, Shared.playersShip);
                        Vector2 MissileSpeed = Triangle.getSpeed(ship.Position, 13f, Triangle.Alpha);
                        Vector2 MissileOrigin = new Vector2(ship.Position.X, ship.Position.Y);
                        Projectile missile = new Projectile(myGame, spriteBatch, myMissileTex[ship.MissileTex], 1f, ship.Rotation
                            , MissileOrigin, MissileSpeed, SpriteEffects.FlipVertically, false);
                        this.Components.Add(missile);
                        missile.Enabled = true;
                        missile.Visible = true;
                        enemyFire.Add(missile);
                        cm.addMissile(missile);
                        ship.Fire = false;

                    }
                    
                }
                #endregion
            }
            #endregion

            #region Create Player Projectiles

            if (ks.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && myShip.Enabled == true)
            {
                Texture2D playerMissileTex = myMissileTex[0];
                float scale = .5f;
                if (myShip.hasWeaponsUpgrade == 0)
                {
                     playerMissileTex = myMissileTex[0];
                    scale = .5f;
                }
                else if(myShip.hasWeaponsUpgrade == 1)
                {
                    playerMissileTex = myMissileTex[10];
                    scale = 1f;
                }

                else if (myShip.hasWeaponsUpgrade == 2)
                {
                    playerMissileTex = myMissileTex[1];
                    scale = 1f;
                }
                Rectangle myRect = myShip.getBounds();
                Vector2 MissileOrigin = new Vector2(myShip.Position.X, myShip.Position.Y);
                Projectile missile = new Projectile(myGame, spriteBatch, playerMissileTex, scale, 0f,MissileOrigin, new Vector2(0, -10), SpriteEffects.None,true);
                this.Components.Add(missile);
                missile.Enabled = true;
                missile.Visible = true;
                missiles.Add(missile);
                cm.addMissile(missile);
            }
            oldState = Keyboard.GetState();
            #endregion

            #region Boss Fires Projectiles........
            if (myBoss != null)
            {
                if (myBoss.Fire)
                {
                    Triangle.CreateTriangle(myBoss.Position, Shared.playersShip);
                    // Vector2 MissileSpeed = Triangle.getSpeed(myBoss.Position, 13f, Triangle.Alpha);
                    Vector2 MissileSpeed = new Vector2(0, 10);

                    Vector2 MissileOrigin = new Vector2(myBoss.Position.X, myBoss.Position.Y);
                    Projectile missile = new Projectile(myGame, spriteBatch, myMissileTex[2], 1f, myBoss.Rotation
                        , MissileOrigin, MissileSpeed, SpriteEffects.FlipVertically, false);
                    this.Components.Add(missile);
                    missile.Enabled = true;
                    missile.Visible = true;
                    enemyFire.Add(missile);
                    cm.addMissile(missile);

                    MissileSpeed = new Vector2(10, 5);
                    missile = new Projectile(myGame, spriteBatch, myMissileTex[2], 1f, myBoss.Rotation
                       , MissileOrigin, MissileSpeed, SpriteEffects.FlipVertically, false);
                    this.Components.Add(missile);
                    missile.Enabled = true;
                    missile.Visible = true;
                    enemyFire.Add(missile);
                    cm.addMissile(missile);

                    MissileSpeed = new Vector2(-10, 5);
                    missile = new Projectile(myGame, spriteBatch, myMissileTex[2], 1f, myBoss.Rotation
                       , MissileOrigin, MissileSpeed, SpriteEffects.FlipVertically, false);
                    this.Components.Add(missile);
                    missile.Enabled = true;
                    missile.Visible = true;
                    enemyFire.Add(missile);
                    cm.addMissile(missile);

                    MissileSpeed = new Vector2(-10, 10);
                    missile = new Projectile(myGame, spriteBatch, myMissileTex[2], 1f, myBoss.Rotation
                       , MissileOrigin, MissileSpeed, SpriteEffects.FlipVertically, false);
                    this.Components.Add(missile);
                    missile.Enabled = true;
                    missile.Visible = true;
                    enemyFire.Add(missile);
                    cm.addMissile(missile);

                    MissileSpeed = new Vector2(10, 10);
                    missile = new Projectile(myGame, spriteBatch, myMissileTex[2], 1f, myBoss.Rotation
                       , MissileOrigin, MissileSpeed, SpriteEffects.FlipVertically, false);
                    this.Components.Add(missile);
                    missile.Enabled = true;
                    missile.Visible = true;
                    enemyFire.Add(missile);
                    cm.addMissile(missile);

                    MissileSpeed = Triangle.getSpeed(myBoss.Position, 13f, Triangle.Alpha);
                    missile = new Projectile(myGame, spriteBatch, myMissileTex[2], 1f, myBoss.Rotation
                        , MissileOrigin, MissileSpeed, SpriteEffects.FlipVertically, false);
                    this.Components.Add(missile);
                    missile.Enabled = true;
                    missile.Visible = true;
                    enemyFire.Add(missile);
                    cm.addMissile(missile);

                    myBoss.Fire = false;
                }
            }
            #endregion

            #region Paralax Scrolling Background Updates.....Updated with end of level animation...

            if (scrollingStars2.Y - myBackGroundTex.Height == 0)
            {
               
                scrollingStars2.Y = scrollingStars1.Y - myBackGroundTex.Height;
            }
            if (scrollingStars1.Y - myBackGroundTex.Height == 0)
            {
              
                scrollingStars1.Y = scrollingStars2.Y - myBackGroundTex.Height;
            }

            if (scrollingOverlay2.Y < -1080)
            {
                scrollingOverlay2.Y += 3;
            }
            else if (scrollingOverlay2.Y >= -1080 && scrollingOverlay2.Y < 0)
            {
                scrollingOverlay2.Y += 1;
                if (myBoss == null)
                {
                    myBoss = new Boss(myGame, spriteBatch, enemyShipTex[2], 1f, 0f, Shared.Positions[Shared.STAGE_TOP_CENTER], new Vector2(6, 6), this);
                    this.Components.Add(myBoss);
                    cm.addBoss(myBoss);
                    Health bossHealth = new Health(myGame, spriteBatch, myBoss, Health.healthType.boss);
                    this.Components.Add(bossHealth);
                }
                
            }
            else
            {
                myShip.Speed = Vector2.Zero;
                myShip.Move(Shared.Positions[Shared.STAGE_CENTER], new Vector2(3, 3));
                if (Vector2.Distance(myShip.Position,Shared.Positions[Shared.STAGE_CENTER] )< 150f)
                {
                    if (myShip.Scale > 0f)
                    {
                        myShip.Scale -= myShip.scale_change;
                        GameOver = true;
                    }
                    
                }
            }
            
            scrollingOverlay1.Y -= 3;          
            scrollingStars1.Y += 1;
            scrollingStars2.Y += 1;
            #endregion

            //#region Read and Write Highscore
            ////Compares Score to Highscores for level when level is over -------------------------SHIT NOT WORKING =/ 
            //if (GameOver == true && checkAndSaveCount == 0)
            //{
            //    if (checkAndSaveCount == 0)
            //    {
            //        //int score = cm.Score;
            //        int score = 1800;

            //        string levelOneFilename = "highscoreLevelOne.txt";
            //        string[] levelOneHighscores = File.ReadAllLines(levelOneFilename);
            //        int[] currentHighScoreValues = new int[10];

            //        //converts string array to int array (without level name)
            //        for (int i = 1; i < levelOneHighscores.Length; i++)
            //        {
            //            for (int j = 0; j < currentHighScoreValues.Length; j++)
            //            {
            //                currentHighScoreValues[j] = int.Parse(levelOneHighscores[i]);
            //            }
            //        }
            //        checkAndSaveCount++;
            //        //checks scores to see if score is highenough
            //        List<int> scores = currentHighScoreValues.ToList();
            //        for (int i = 0; i < scores.Count; i++)
            //        {
            //            if (score >= scores[i])
            //            {
            //                scores.RemoveAt(scores.Count - 1);
            //                scores.Insert(i, score);
                            
            //            }
            //        }
            //        //write scores to highscoreLevelOne.txt
            //        WriteFileToArray(levelOneFilename, scores, levelOneHighscores[0]);

            //    }
            //}
            //#endregion

            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            #region Draw Background
            spriteBatch.Draw(myBackGroundTex, scrollingStars2, Color.White);
            spriteBatch.Draw(myBackGroundTex, scrollingStars1, Color.White);
            spriteBatch.Draw(overlayTex, new Vector2(0, 0), scrollingOverlay1, Color.White);
            spriteBatch.Draw(overlayTex2, scrollingOverlay2, Color.White);


            //draws the game over screen when player life  hits 0
            if (myShip.Lives <= 0 && myShip.Life <=0)
            {
                spriteBatch.Draw(gameOverTex, Vector2.Zero, Color.White);
            }

            //draws the mission sucess screen
            if (GameOver == true)
            {
                spriteBatch.Draw(levelCompleteTex, Vector2.Zero, Color.White);
                //levelComplete = true;           
            }
            //draws score in top right corner
            string score = cm.Score.ToString();
            Vector2 strDimensions = scoreFont.MeasureString(score);
            spriteBatch.DrawString(scoreFont, score, new Vector2(Shared.stage.X - strDimensions.X - 40, 10), Color.White);

            #endregion
            spriteBatch.End();
            base.Draw(gameTime);
        }
       


    }
    #endregion
}
