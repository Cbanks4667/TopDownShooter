using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    #region made alterations new enum ....
    class EnemyShips : GameObject
    {
        #region EnemyShip Global variables
        //Vector2[] pattern;
        int STARTING_FRAME = 0;
        //int index = STARTING_FRAME;
        int index = 0;
        SpriteEffects flip = SpriteEffects.None;
        private bool fire;
        
        int missileTex;
        public enum size {Small,
        Medium,
        Large};
        
        #endregion
        #region Properties
        public SpriteEffects Flip
        {
            get
            {
                return flip;
            }

            set
            {
                flip = value;
            }
        }

        public bool Fire
        {
            get
            {
                return fire;
            }

            set
            {
                fire = value;
            }
        }

        public Vector2[] Pattern
        {
            get
            {
                return pattern;
            }

            set
            {
                pattern = value;
            }
        }

        public int MissileTex
        {
            get
            {
                return missileTex;
            }

            set
            {
                missileTex = value;
            }
        }
        #endregion
        public EnemyShips(Game game, SpriteBatch spriteBatch, Texture2D tex, float scale, float rotation, Vector2 initPosition, Vector2 speed, Vector2[] pattern, size ShipType) : base(game, spriteBatch, tex, scale, rotation, initPosition, speed)
        {
            this.tex = tex;
            this.position = initPosition;
            Random r = new Random();
            

            if (ShipType == size.Large)
            {
                this.ROWS = 1;
                this.COLUMNS = 1;
                Radius = 80f;
                origin = new Vector2(80, 95);
                missileTex = 2;
             }
            else if (ShipType == size.Medium)
            {
                STARTING_FRAME = r.Next(0, 2);
                this.ROWS = 1;
                this.COLUMNS = 2;
                Radius = 48.5f;
                origin = new Vector2(81, 56);
                if (STARTING_FRAME == 0)
                {
                    missileTex = 4;
                }
                else
                {
                    missileTex = 9;
                }
            }
            else if (ShipType == size.Small)
            {
                
                STARTING_FRAME = r.Next(0, 2);
                this.ROWS = 1;
                this.COLUMNS = 2;
                Radius = 26.5f;
                origin = new Vector2(40, 28);
                if (STARTING_FRAME == 0)
                {
                    missileTex = 5;
                }
                else
                {
                    missileTex = 6;
                }
            }
            this.dimension = new Vector2((float)this.tex.Width / COLUMNS, (float)this.tex.Height / ROWS);
            this.createFrames(dimension);
            this.srcRect = frames[STARTING_FRAME];

            //this.origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);

            Shared.CreatePositions(Shared.stage);
            Shared.CreatePatterns();
            this.pattern = pattern;
            
            this.index = 0;
        }
        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            #region disable object if it leaves the boundary of the screen
            //Rectangle Screen = new Rectangle(-50, -50, (int)Shared.stage.X + 50, (int)Shared.stage.Y + 50);
            //if (!Screen.Contains(position))
            //{
            //    this.Visible = false;
            //    this.Enabled = false;
            //}
            //else
            //{
            //     this.Enabled = true;
            //     this.Visible = true;
            //}
            #endregion

            #region Updates Position of Enemy Ship
            //a is hypotenuse distance from enemyShip to the array of vectors it is to move towards
            float a = Vector2.Distance(this.position, this.Pattern[index]);

            // a > 3 handles wonky errors in move method
            if (a > 3)
            {
                //Move object to specific point at a specified vector speed return true if arrived at location
                bool location = this.Move(Pattern[index], Speed);

                //my continued efforts to figure out how to rotate properly

                //Rotation = MathHelper.ToRadians(90) - Triangle.getRotation(position, Shared.playersShip);
                Rotation = Triangle.getRotation(position, Shared.playersShip);

                // create a triangle with point a being position of ship, b being postion of player ship and c the point where it forms a right angle
                Triangle.CreateTriangle(position, Shared.playersShip);
               
            }
            else
            {
                //if the distance between objects is less than 3 register as being at the location
                //form new triangle between objects
                Triangle.CreateTriangle(Position, Shared.playersShip);
               
                //set the fire boolean variable to true
                //action scene will now create a projectile
                fire = true;
              //update the move function to move towards new point
                if (index < Pattern.Length - 1)
                {
                    index++;
                }
                else
                {
                    //disable this object
                    this.Enabled = false;
                    this.Visible = false;
                }
                
            }
            #endregion 
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, Rotation, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
    #endregion....
}
