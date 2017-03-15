using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class BlackHole : GameObject
    {
        public BlackHole(Game game, 
            SpriteBatch spriteBatch,
            Texture2D tex, 
            float scale,
            float rotation,
            Vector2 initPosition,
            Vector2 speed) : base(game, spriteBatch, tex, scale, rotation, initPosition, speed)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.scale = scale;
            this.rotation = rotation;
            this.initPosition = initPosition;
            this.speed = speed;

            ROWS = 1;
            COLUMNS = 1;
            dimension = new Vector2((float)tex.Width / COLUMNS, (float)tex.Height / ROWS);
            createFrames(dimension);

            //only one image for blackhole
            srcRect = frames[0];

            position = initPosition;
            origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);
            radius = 214;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            position += speed;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
