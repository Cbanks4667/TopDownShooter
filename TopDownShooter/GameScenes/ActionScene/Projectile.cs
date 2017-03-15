using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    #region to be done alter to accept all missile textures pass proper one as enum.....
    class Projectile : GameObject
    {
        #region global variables
        private SpriteEffects myEffect;
        private float rotationChange = 0.3f;
        private float scaleChange = 0.1f;
        private float initScale;
        private float MaxScale;
        private float MinScale;
        bool isLocked;
        private bool playerFire;
        #endregion
        #region Properties
        public bool PlayerFire
        {
            get
            {
                return playerFire;
            }

            set
            {
                playerFire = value;
            }
        }

        public bool IsLocked
        {
            get
            {
                return isLocked;
            }

            set
            {
                isLocked = value;
            }
        }
        #endregion
        public Projectile(Game game, SpriteBatch spriteBatch, Texture2D tex, float scale, float rotation, Vector2 initPosition, Vector2 speed, SpriteEffects myEffect,bool playerFire) : base(game, spriteBatch, tex, scale, rotation, initPosition, speed)
        {
            isLocked = false;
            this.playerFire = playerFire;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.scale = scale;
            this.initScale = scale;
            this.MaxScale = initScale + 3 * scaleChange;
            this.MinScale = initScale - 3 * scaleChange;
            this.rotation = rotation;
            this.initPosition = initPosition;
            this.position = initPosition;
            this.speed = speed;
            this.myEffect = myEffect;
            this.ROWS = 1;
            this.COLUMNS = 1;
            this.dimension = new Vector2((float)tex.Width / COLUMNS, (float)tex.Height / ROWS);
            this.createFrames(dimension);
            this.srcRect = frames[0];
            this.origin = new Vector2((srcRect.Width / 2) * scale, (srcRect.Height / 2) * scale);
            Radius = 40 * scale;
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //updates position of missile
            if (IsLocked == false)
            {
                position += speed;
            }
            

            // handles whether the missile is a antimatter torpedo and is going to change scale and rotate
            if (tex == Game.Content.Load<Texture2D>("images/projectiles/missile"))
            {
                rotation += rotationChange;
                if (scale >= MaxScale)
                {
                    scaleChange = -scaleChange;
                }
                else if (scale <= MinScale)
                {
                    scaleChange = Math.Abs(scaleChange);
                }
                scale += scaleChange; 
            }
            //disables missile if it leaves the stage
            Rectangle Screen = new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y);
            if (!Screen.Contains(position))
            {
                this.Visible = false;
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                this.Visible = true;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            spriteBatch.Draw(tex,position,srcRect,Color.White,rotation, new Vector2(srcRect.Width / 2, srcRect.Height / 2), scale,myEffect,0f);           
            spriteBatch.End();
            base.Draw(gameTime);
        }

       
    }
    #endregion
}
