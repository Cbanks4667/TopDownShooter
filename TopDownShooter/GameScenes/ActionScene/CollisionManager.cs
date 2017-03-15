using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
namespace TopDownShooter
{
    #region Massive Alterations......Work still in progress....
    class CollisionManager : GameComponent
    {
        private List<Projectile> enemyMissiles = new List<Projectile>();
        private List<Projectile> myMissiles = new List<Projectile>();
        public List<Powerup> actionPowers = new List<Powerup>();
        private List<BlackHole> holes = new List<BlackHole>();
        private PlayerShip player;
        private List<Asteroid> meteors = new List<Asteroid>();
        private List<Powerup> powerUps = new List<Powerup>();
        private List<EnemyShips> enemies = new List<EnemyShips>();
        private List<object> allObjects = new List<object>();
        private Boss boss;
        private SoundEffect[] mySounds;
        private SoundEffect[] atariBoomSounds;
        private SoundEffect[] boomSounds;
        private Game game;
        private List<GameComponent> actionList;
        public int Score;
        private Texture2D explosionTex;
        private Texture2D powerupTex;
        private SpriteBatch spriteBatch;
        private List<Explosion> explodeList = new List<Explosion>();

        internal List<Explosion> ExplodeList
        {
            get
            {
                return explodeList;
            }

            set
            {
                explodeList = value;
            }
        }

        public CollisionManager(Game game, SoundEffect[] mySounds, SoundEffect[] atariBoomSounds, SoundEffect[] boomSounds, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.mySounds = mySounds;
            this.atariBoomSounds = atariBoomSounds;
            this.boomSounds = boomSounds;
            this.spriteBatch = spriteBatch;
            explosionTex = game.Content.Load<Texture2D>("images/explosions/explosions");
            powerupTex = game.Content.Load<Texture2D>("images/powerUps/Upgrades(162.5x162.5)813x163");
        }
        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            #region Detects Collision between myFire and enemies
            foreach (Projectile missile in myMissiles)
            {
              
                for (int i = 0; i < enemies.Capacity; i++)
                {


                    if (enemies[i].Enabled == true && missile.Enabled == true)
                    {
                        if (missile.Tex == game.Content.Load<Texture2D>("images/projectiles/torpSingle_yellow"))
                        {
                            if (missile.IsLocked == false)
                            {
                                bool homeing = HomeingMissile(missile, enemies[i]);
                            }
                           

                            if (missile.IsLocked == true)
                            {
                                missile.Move(enemies[i].Position, missile.Speed);
                                
                            }
                        }
                        #region collision Detection
                        bool collide = Collision(missile, enemies[i]);
                        if (collide == true)
                        {
                            Random a = new Random();
                            int power = a.Next(0, 50);
                           
                            missile.Position = new Vector2(-100, -100);
                            missile.Enabled = false;
                            missile.Visible = false;
                            Score += 100;
                            Explosion explosion = new Explosion(game, spriteBatch, explosionTex, 3f, 0f, enemies[i].Position, Vector2.Zero, Explosion.explosionType.electricSizzle);
                            explodeList.Add(explosion);
                            
                            if (power < 5)
                            {
                                Powerup powers = new Powerup(game, spriteBatch, powerupTex, 1f, 0f, enemies[i].Position, new Vector2(2 * (Math.Abs(enemies[i].Speed.X) / enemies[i].Speed.X), 2), (Powerup.upgradeType)power);
                                addPowerUp(powers);
                                actionPowers.Add(powers);

                            }
                            enemies[i].Speed = Vector2.Zero;
                            enemies[i].Enabled = false;
                            enemies[i].Visible = false;
                        }
                        #endregion
                    }          
                    
                }
               
            }
            #endregion

            #region Detects Collisions between Enemy Missiles and our ship

            if (player.Life > 0 && player.IsInvincible == false)
            {
                foreach (Projectile missile in enemyMissiles)
                {
                    if (missile.Enabled == true)
                    {
                        bool collide = Collision(missile, player);
                        if (collide == true)
                        {
            
                            missile.Enabled = false;
                            missile.Visible = false;
                            atariBoomSounds[4].Play();
                            if (player.hasShields == 0)
                            {
                                player.Life -= 10;

                                if (player.Life <= 0 && player.Lives == 0)
                                {
                                    player.Enabled = false;
                                    player.Visible = false;
                                    Explosion explosion = new Explosion(game, spriteBatch, explosionTex, 3f, 0f, player.Position, Vector2.Zero, Explosion.explosionType.fireyDeath);
                                    explodeList.Add(explosion);

                                }
                                else if (player.Life<=0 && player.Lives >1)
                                {
                                    player.Lives--;
                                    player.Life = 100;
                                    
                                    Explosion explosion = new Explosion(game, spriteBatch, explosionTex, 3f, 0f, player.Position, Vector2.Zero, Explosion.explosionType.fireyDeath);
                                    explodeList.Add(explosion);

                                }
                                else if (player.Life <=0 && player.Lives ==1)
                                {
                                    player.Lives--;
                                    player.Enabled = false;
                                    player.Visible = false;
                                    Explosion explosion = new Explosion(game, spriteBatch, explosionTex, 3f, 0f, player.Position, Vector2.Zero, Explosion.explosionType.fireyDeath);
                                    explodeList.Add(explosion);
                                }
                                
                               
                            }
                            else
                            {
                                Explosion explosion = new Explosion(game, spriteBatch, explosionTex, 1f, 05f, missile.Position, Vector2.Zero, Explosion.explosionType.blueThunder);
                                explodeList.Add(explosion);
                                player.hasShields = 0;
                                player.Index = 1;
                                player.SrcRect = player.MyFrames[player.hasWeaponsUpgrade, 1];
                                
                            }
                            
                        }
                    }
                }
            }

            #endregion

            #region Detects Collisions between EnemyShips and Player .....work in progress......
            foreach (EnemyShips ship in enemies)
            {
                if (ship.Visible == true && player.Life > 0)
                {
                    bool collide = Collision(ship, player);
                    if (collide)
                    {
                        ship.Enabled = false;
                        ship.Visible = false;
                        player.Life -= 5;
                        atariBoomSounds[5].Play();
                        if (player.Life <= 0)
                        {
                            player.Enabled = false;
                            player.Visible = false; 

                        }
                    }
                }
            }
            #endregion


            #region Detects Collisions between Player Fire and Asteroids
            foreach (Projectile missile in myMissiles)
            {             
                    for (int i = 0; i < meteors.Count; i++)
                    {
                        if (missile.Enabled == true && meteors[i].Enabled == true)
                        {
                            bool collide = Collision(missile, meteors[i]);
                            if (collide)
                            {
                                    Random r = new Random();
                                    int power  = r.Next(0, 50);
                                    missile.Position = new Vector2(-100, -100);
                                    missile.Enabled = false;
                                    missile.Visible = false;
                                    Score += 100;

                                    Explosion explosion = new Explosion(game, spriteBatch, explosionTex, 3f, 0f, meteors[i].Position, Vector2.Zero, Explosion.explosionType.electricSizzle);
                                    explodeList.Add(explosion);
                                if (power < 5)
                                {
                                    Powerup powers = new Powerup(game, spriteBatch, powerupTex, 1f, 0f, meteors[i].Position, new Vector2(2 * (Math.Abs(meteors[i].Speed.X)/meteors[i].Speed.X),2), (Powerup.upgradeType)power);
                                    addPowerUp(powers);
                                    actionPowers.Add(powers);

                                }
                            meteors[i].Enabled = false;
                            meteors[i].Visible = false;
                            meteors[i].Speed = Vector2.Zero;
                            }
                        }
                    }
                
            }
            #endregion

            #region Detects Collisions between the ship and a powerup ... work in progress....
            foreach (Powerup power in powerUps)
            {
                if (power.Enabled == true && player.Enabled == true)
                {
                    bool collide = Collision(power, player);
                    if (collide)
                    {
                        switch (power.upgrade)
                        {
                            case 0:
                                player.hasShields = 1;
                                break;
                            case 1:
                                player.Life = 100;
                                player.Lives++;
                                break;
                            case 2:
                                player.hasWeaponsUpgrade = 1;
                                break;
                            case 3:
                                player.hasWeaponsUpgrade = 2;
                                break;
                            case 4:
                                player.hasWeaponsUpgrade = 0;
                                break;
                            default:
                                break;
                        }
                        power.Enabled = false;
                        power.Visible = false;
                        mySounds[0].Play();
                    }
                }
            }
            #endregion


            if (boss != null)
            {
                foreach (Projectile missile in myMissiles)
                {
                    if (missile.Enabled == true && boss.Enabled == true && boss.IsInvincible == false)
                    {
                        bool collide = Collision(missile, boss);
                        if (collide)
                        {
                            boss.life -= 1;
                            if (boss.life <= 0)
                            {
                                boss.Enabled = false;
                                boss.Visible = false;
                            }
                        }
                    }
                }
            }


            #region BlackHole
            foreach (BlackHole blackHole in holes)
            {
                bool playerInRange = Collision(player, blackHole);
                if (playerInRange)
                {
                    player.Move(blackHole.Position, new Vector2(2, 2));
                    float distance = distanceBetweenTwoPositions(blackHole, player);
                    if (distance == blackHole.Radius)
                    {
                        player.Enabled = false;
                        player.Visible = false;
                    }
                }
            }
            #endregion


            base.Update(gameTime);
        }

        #region CM Add Methods
        public void addBlackHole(BlackHole Object)
        {
            holes.Add(Object);
            allObjects.Add(Object);
                     
        }
        public void addEnemy(EnemyShips Object)
        {
            enemies.Add(Object);
            allObjects.Add(Object);
        }
        
        public void addPlayer(PlayerShip Object)
        {
            player = Object;
            allObjects.Add(Object);
        }
        public void addPowerUp(Powerup Object)
        {
            powerUps.Add(Object);
            allObjects.Add(Object);
        }

        public void addBoss(Boss Object)
        {
            boss = Object;
            allObjects.Add(Object);
        }
        
        public void addMissile(Projectile Object)
        {
            if (Object.PlayerFire)
            {
                myMissiles.Add(Object);
            }
            else
            {
                enemyMissiles.Add(Object);
            }
            allObjects.Add(Object);
        }

        public void addAsteroid(Asteroid Object)
        {
            meteors.Add(Object);
            allObjects.Add(Object);
        }
        #endregion

        private bool Collision(GameObject first , GameObject second)
        {
            float distance = Vector2.Distance(first.Position, second.Position);
            if (distance < first.Radius + second.Radius)
            {
                return true;
            }
            
            return false;
        }


        private float distanceBetweenTwoPositions(GameObject first, GameObject second)
        {
            float distance = Vector2.Distance(first.Position, second.Position);
            return distance;
        }

        private bool HomeingMissile(Projectile first, GameObject second)
        {
            float distance = Vector2.Distance(first.Position, second.Position);
            float seperation = distance - (first.Radius + second.Radius);
            if (seperation < 50f)
            {
                first.IsLocked = true;
                return true;
            }
            else
            {
                //first.IsLocked = false;
                return false;
            }
        }
    }

   
    #endregion
}
