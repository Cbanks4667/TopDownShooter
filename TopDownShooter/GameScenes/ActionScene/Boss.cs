using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class Boss : GameObject
    {
        int index = 0;
        int index_change = 1;
        bool atPosition = false;
        private float scale_change = 0.02f;
        private bool fire = false;
        ActionScene actionScene;
        public int life = 100;
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

        public Boss(Game game, SpriteBatch spriteBatch, Texture2D tex, float scale, float rotation, Vector2 initPosition, Vector2 speed,ActionScene actionScene) : base(game, spriteBatch, tex, scale, rotation, initPosition, speed)
        {

            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.scale = scale;
            this.rotation = rotation;
            this.initPosition = initPosition;
            this.position = initPosition;
            this.speed = speed;
            this.isInvincible = false;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);
            this.actionScene = actionScene;
            Radius = 90f * scale;
           
            this.pattern = new Vector2[] {Shared.Positions[Shared.STAGE_BELOW_TOP_LEFT],Shared.Positions[Shared.STAGE_TOP_LEFT_CENTER],Shared.Positions[Shared.STAGE_CENTER],Shared.Positions[Shared.STAGE_TOP_RIGHT_CENTER],Shared.Positions[Shared.STAGE_BELOW_TOP_RIGHT]};
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            actionScene.ScrollingOverlay2 = new Rectangle(0,-1080,1920,1080);
            radius = radius * scale;
            
            //if (Vector2.Distance(position,Shared.Positions[Shared.STAGE_BELOW_TOP_LEFT_CENTER]) < 9)
            //{
            //    position = Shared.Positions[Shared.STAGE_BELOW_TOP_LEFT_CENTER];
            //    atPosition = true;
            //}
            //if (atPosition != true)
            //{
            //    Move(Shared.Positions[Shared.STAGE_BELOW_TOP_LEFT_CENTER], speed);
            //}

            #region Updates Position of Enemy Ship
            //a is hypotenuse distance from enemyShip to the array of vectors it is to move towards
            float a = Vector2.Distance(this.position, this.pattern[index]);

            // a > 3 handles wonky errors in move method
            if (a > 3)
            {
                //Move object to specific point at a specified vector speed return true if arrived at location
                bool location = this.Move(pattern[index], Speed);
               
            }
            else
            {
                //set the fire boolean variable to true
                //action scene will now create a projectile
                fire = true;
                //update the move function to move towards new point
                if (index < pattern.Length ||  index > -1)
                {
                    index += index_change;
                    if (index == pattern.Length)
                    {
                        index_change = -1;
                        index = pattern.Length - 1;
                    }
                    else if (index == 0)
                    {
                        index_change = 1;
                        index = 0;

                    }
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
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, scale, SpriteEffects.FlipVertically, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
