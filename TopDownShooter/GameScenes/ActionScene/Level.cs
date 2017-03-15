//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace TopDownShooter
//{
//    class Level : ActionScene
//    {
//        protected int currentLevel;
//        protected SpriteBatch spriteBatch;
//        protected Vector2 stage;
//        private PlayerShip myShip;
//        public Level(Game game, SpriteBatch spriteBatch, int currentLevel, Vector2 stage) : base(game, spriteBatch)
//        {
//            this.spriteBatch = spriteBatch;
//            this.currentLevel = currentLevel;
//            this.stage = stage;
//            Texture2D myShipTex = game.Content.Load<Texture2D>("images/myShip");
//            Vector2 myShipPosition = new Vector2((stage.X / 2) + (myShipTex.Width / 2), (stage.Y / 2) + (myShipTex.Height / 2));
//            Vector2 myShipSpeed = new Vector2(5, 5);
//            myShip = new PlayerShip(game, this, spriteBatch, myShipTex, .5f, 1f, myShipPosition, myShipSpeed);
//            this.Components.Add(myShip);
//        }

//        public override void Initialize()
//        {
//            base.Initialize();
//        }

//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            base.Draw(gameTime);
//        }
//    }
//}
