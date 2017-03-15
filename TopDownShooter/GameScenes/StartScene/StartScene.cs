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
    public class StartScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private MenuItem menu;
        private Texture2D[] menuTex;

        public MenuItem Menu
        {
            get
            {
                return menu;
            }

            set
            {
                menu = value;
            }
        }

        public StartScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("images/menuBackgrounds/startSceneBackground");

            menuTex = new Texture2D[]
            {
                game.Content.Load<Texture2D>("images/menuBackgrounds/selectedMenuItems/playSelected"),
                game.Content.Load<Texture2D>("images/menuBackgrounds/selectedMenuItems/controlsSelected"),
                game.Content.Load<Texture2D>("images/menuBackgrounds/selectedMenuItems/gameDescriptionSelected"),
                game.Content.Load<Texture2D>("images/menuBackgrounds/selectedMenuItems/highscoreSelected"),
                game.Content.Load<Texture2D>("images/menuBackgrounds/selectedMenuItems/creditsSelected"),
                game.Content.Load<Texture2D>("images/menuBackgrounds/selectedMenuItems/exitSelected")
            };

            menu = new MenuItem(game, spriteBatch, menuTex);
            this.Components.Add(menu);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
