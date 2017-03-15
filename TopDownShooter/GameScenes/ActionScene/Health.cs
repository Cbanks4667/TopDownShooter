using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    class Health : DrawableGameComponent
    {
        Vector2 origin;
        float scale;
        float rotation;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Rectangle srcRect;
        PlayerShip player;
        Boss boss;

        public Texture2D Tex
        {
            get
            {
                return tex;
            }

            set
            {
                tex = value;
            }
        }

        public enum healthType {player, boss };
        public Health(Game game, SpriteBatch spriteBatch, GameObject player, healthType type) : base(game)
        {
            tex = game.Content.Load<Texture2D>("images/HUD/LifeBar");
            this.spriteBatch = spriteBatch;
            if (type == healthType.player)
            {
                this.player = (PlayerShip)player;
                position = new Vector2(tex.Width/2,Shared.stage.Y -tex.Height/2);
                rotation = 0f;
                scale = 1f;
            }
            else if(type == healthType.boss)
            {
                this.boss = (Boss)player;
                // position = new Vector2(Shared.stage.X - tex.Width, 0);
                position = boss.Position;
                rotation = -MathHelper.ToRadians(90);
                scale = 1f;
            }
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
           // srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)

        {
          
            if (player != null)
            {
                srcRect = new Rectangle(0, 0, tex.Width * player.Life / 100, tex.Height);
            }
            else if (boss != null)
            {
                position = new Vector2(boss.Position.X - (boss.Tex.Width/2) +10, boss.Position.Y);
                srcRect = new Rectangle(0, 0, tex.Width * boss.life / 100, tex.Height);
            }
           
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
            
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(tex,position,srcRect,Color.White);
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
