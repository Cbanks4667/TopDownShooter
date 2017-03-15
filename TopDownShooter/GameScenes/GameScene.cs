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
    public class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;

        #region GameScene Properties
        public List<GameComponent> Components
        {
            get
            {
                return components;
            }

            set
            {
                components = value;
            }
        }
        #endregion

        #region GameScene Methods
        /// <summary>
        /// Enables and makes a GameScene visible
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Disables and makes a GameScene invisible
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        } 
        #endregion

        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Updates a GameComponent in the components list if it is enabled
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Draws only components that are visible
            DrawableGameComponent component = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    component = (DrawableGameComponent)item;
                    if (component.Visible)
                    {
                        component.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }

    }
}
