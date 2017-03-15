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
    class HighscoreComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, boldFont;
        private List<string> highscores;
        private Vector2 position;

        #region HighscoreComponent Methods
        public void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        } 
        #endregion

        public HighscoreComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont boldFont,
            string[] levelHighscores) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.boldFont = boldFont;
            highscores = levelHighscores.ToList();
            this.position = new Vector2(Shared.stage.X/2, 60);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //add index logic here for navigating level highscores????

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 nextPos = position;

            spriteBatch.Begin();
            for (int i = 0; i < highscores.Count; i++)
            {
                if (i == 0)
                {
                    Vector2 strDimensions = boldFont.MeasureString(highscores[i]);
                    spriteBatch.DrawString(boldFont, highscores[i], new Vector2(nextPos.X - strDimensions.X/2, nextPos.Y), Color.White);
                    nextPos.Y += boldFont.LineSpacing;
                }
                else
                {
                    Vector2 strDimensions = regularFont.MeasureString(highscores[i]);
                    spriteBatch.DrawString(regularFont, highscores[i], new Vector2(nextPos.X + 200 - strDimensions.X, nextPos.Y), Color.White);
                    nextPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
