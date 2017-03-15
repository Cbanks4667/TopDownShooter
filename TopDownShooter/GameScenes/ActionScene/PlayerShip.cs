using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooter
{
    #region .....Made Alterations two d array to handle shields and upgrades new variables....
    class PlayerShip : GameObject
    {
        #region global variables 
        private ActionScene actionScene;
        const int STARTING_FRAME = 1;
        int index = STARTING_FRAME;
        int i = 0;
        private int life = 100;
        private Rectangle[,] myFrames = new Rectangle[3, 4];
        public int hasShields = 0;
        public int hasWeaponsUpgrade = 1;
        public int Lives = 3;
        public float scale_change = 0.01f;
        #endregion
        public PlayerShip(Game game, ActionScene actionScene, SpriteBatch spriteBatch, Texture2D tex, float scale, float rotation, Vector2 initPosition, Vector2 speed) : base(game, spriteBatch, tex, scale, rotation, initPosition, speed)
        {
            this.actionScene = actionScene;
            this.position = initPosition;
            this.ROWS = 3;
            this.COLUMNS = 4;
            this.dimension = new Vector2((float)tex.Width / COLUMNS, (float)tex.Height / ROWS);
            this.createFrames(dimension);
            isInvincible = false;
            this.origin = new Vector2((srcRect.Width / 2), srcRect.Height / 2);
            //Radius = 90f;
            
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLUMNS; c++)
                {

                    myFrames[r, c] = frames[i];
                        i++;
                }
            }
            
        } 

        public int Life
        {
            get
            {
                return life;
            }

            set
            {
                life = value;
            }
        }

        public Rectangle[,] MyFrames
        {
            get
            {
                return myFrames;
            }

            set
            {
                myFrames = value;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        //this.SetPattern(Shared.Patterns[Shared.PATTERN_LOOP]); 
        //this.SetPattern(Shared.Patterns[Shared.PATTERN_WAVE]);
        //this.SetPattern(Shared.Patterns[Shared.PATTERN_DIAGONAL]);       


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (hasShields == 1)
            {
                Radius = 86f;
            }
            else
            {
                Radius = 55f;
            }
            #region controls player ship movement
            if (ks.IsKeyDown(Keys.Up))
            {
               
                    index = 2 + hasShields;
                    srcRect = myFrames[hasWeaponsUpgrade,index];
                if (position.Y - (srcRect.Height / 2) + 80 > 0)
                {
                    position.Y -= speed.Y;
                }
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                index = 1 - hasShields;
                srcRect = myFrames[hasWeaponsUpgrade,index];
                if (position.Y + (srcRect.Height / 2) - 80 < Shared.stage.Y)
                {
                    position.Y += speed.Y;
                }
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                index = 2 + hasShields;
                srcRect = myFrames[hasWeaponsUpgrade,index];
                if (position.X - (srcRect.Width / 2) + 70 > 0)
                {
                    position.X -= speed.X;
                }

            }
            if (ks.IsKeyDown(Keys.Right))
            {
                index = 2 + hasShields;
                srcRect = myFrames[hasWeaponsUpgrade,index];
                if (position.X + (srcRect.Width/2) -70 <Shared.stage.X)
                {
                    position.X += speed.X;
                }
                
            }
            #endregion
            //Allows all objects to know where the player ship is without having to pass them the player ship reference
            Shared.playersShip = position;
            srcRect = myFrames[hasWeaponsUpgrade, index];

            //#region Disable and Hide ship if it leaves the stage
            //Rectangle Screen = new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y);
            //if (!Screen.Contains(position))
            //{
            //    this.Visible = false;
            //    this.Enabled = false;
            //}
            //else
            //{
            //    this.Enabled = true;
            //    this.Visible = true;
            //}
            //#endregion
            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, new Vector2(srcRect.Width/2,(srcRect.Height -28)/2 ), scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
    #endregion
}
