using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    
    class Lives : DrawableGameComponent
    {
        SpriteFont font;
        Texture2D tex;
        SpriteBatch spriteBatch;
        PlayerShip player;
        Vector2 position;
        Rectangle srcRect;
        List<Rectangle> frames = new List<Rectangle>();
        const int ROWS = 3;
        const int COLUMNS = 4;
        Vector2 dimension;
        float scale = 0.5f;
        float rotation = 0;
        Vector2 origin;
        string lives;
        Vector2 strPos;
        public Lives(Game game, SpriteBatch spriteBatch, PlayerShip player, SpriteFont font, Health healthbar) : base(game)
        {
            this.font = font;
            this.player = player;
            this.tex = player.Tex;
            this.spriteBatch = spriteBatch;
            
            lives = player.Lives.ToString();
            this.dimension = new Vector2((float)tex.Width / COLUMNS, (float)tex.Height / ROWS);
            createFrames(dimension);
            srcRect = frames[1];
            origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);
            position = new Vector2(healthbar.Tex.Width + srcRect.Width/2 -120,Shared.stage.Y - srcRect.Height/2 + 150);
            strPos = new Vector2(position.X + 60, Shared.stage.Y - 40);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            if (player.Lives > 1)
            {
                lives = "X" + player.Lives;
            }
            else
            {
                lives = "";
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (player.Lives > 0)
            {
                spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
            }           
            spriteBatch.DrawString(font, lives, strPos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void createFrames(Vector2 dimension)
        {

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    //if (i == 3 && j == 3)
                    //{
                    //    return;

                    //}
                    Rectangle frame = new Rectangle(j * (int)dimension.X, i * (int)dimension.Y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(frame);
                }
            }
        }
    }
}
