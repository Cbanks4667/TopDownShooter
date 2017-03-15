using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    #region .....Made Alterations....Changed Scale
    class Explosion : GameObject
    {
        private List<Rectangle> explosionFrames = new List<Rectangle>();
        int frameIndex = 0;
        int delay = 3;
        int delayCounter;
        public enum explosionType
        {
            blueThunder,
            fireyDeath,
            greenPoof,
            electricSizzle,
            bloodyMess
        };
        public Explosion(Game game, 
            SpriteBatch spriteBatch, 
            Texture2D tex, 
            float scale, 
            float rotation,
            Vector2 initPosition, 
            Vector2 speed,
            explosionType explosion) : base(game, spriteBatch, tex, scale, rotation, initPosition, speed)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.scale = scale;
            this.rotation = rotation;
            this.initPosition = initPosition;
            this.speed = speed;
            ROWS = 22;
            COLUMNS = 8;
            dimension = new Vector2((float)tex.Width / COLUMNS, (float)tex.Height / ROWS);
            createFrames(dimension);

            if (explosion == explosionType.blueThunder)
            {
                //0 to 31 are blueThunder Frames
                for (int i = 0; i < 32; i++)
                {
                    explosionFrames.Add(frames[i]);
                }
                srcRect = explosionFrames[0];
            }
            else if (explosion == explosionType.fireyDeath)
            {
                //32 to 72 are fireyDeath Frames
                for (int i = 32; i < 72; i++)
                {
                    explosionFrames.Add(frames[i]);
                }
                srcRect = explosionFrames[0];
            }
            else if (explosion == explosionType.greenPoof)
            {
                //72 to 104 are greenPoof Frames
                for (int i = 72; i < 104; i++)
                {
                    explosionFrames.Add(frames[i]);
                }
                srcRect = explosionFrames[0];
            }
            else if (explosion == explosionType.electricSizzle)
            {
                //104 to 144 are electricSizzle Frames
                for (int i = 104; i < 144; i++)
                {
                    explosionFrames.Add(frames[i]);
                }
                srcRect = explosionFrames[0];
            }
            else if (explosion == explosionType.bloodyMess)
            {
                //144 to 176 are bloodyMess Frames
                for (int i = 144; i < 176; i++)
                {
                    explosionFrames.Add(frames[i]);
                }
                srcRect = explosionFrames[0];
            }

            position = initPosition;
            origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);
            radius = 55;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > explosionFrames.Count)
                {
                    frameIndex = -1;
                    this.Enabled = false;
                    this.Visible = false;
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //Loop through explosionFrames animation
                if (frameIndex >= 0 && frameIndex < explosionFrames.Count)
              
                {
                    srcRect = explosionFrames[frameIndex];
                    spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, new Vector2(srcRect.Width/2,srcRect.Height/2), scale, SpriteEffects.None, 0);
                    
                }
            
          
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
    #endregion
}
