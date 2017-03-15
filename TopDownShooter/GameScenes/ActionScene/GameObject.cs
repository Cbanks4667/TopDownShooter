using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
   abstract class GameObject : DrawableGameComponent
    {
        #region global variables for all inherited objects
        protected Vector2 position;
        protected Vector2 drawPos;
        protected Vector2 initPosition;
        protected Vector2 origin;
        protected Vector2 speed;
        protected Vector2[] pattern;
        protected Texture2D tex;
        protected SpriteBatch spriteBatch;
        protected int rOWS;
        protected int cOLUMNS;
        protected Rectangle srcRect;
        protected Vector2 dimension;
        protected bool isInvincible = false;
        protected float scale;
        protected float rotation;
        protected float radius;
        protected List<Rectangle> frames = new List<Rectangle>();
        #endregion
        #region Properties
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
            }
        }

        public Vector2 Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

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

        public bool IsInvincible
        {
            get
            {
                return isInvincible;
            }

            set
            {
                isInvincible = value;
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public Rectangle SrcRect
        {
            get
            {
                return srcRect;
            }

            set
            {
                srcRect = value;
            }
        }

        public float Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
            }
        }

        public int ROWS
        {
            get
            {
                return rOWS;
            }

            set
            {
                rOWS = value;
            }
        }

        public int COLUMNS
        {
            get
            {
                return cOLUMNS;
            }

            set
            {
                cOLUMNS = value;
            }
        }

        public Vector2 Dimension
        {
            get
            {
                return dimension;
            }

            set
            {
                dimension = value;
            }
        }

        public List<Rectangle> Frames
        {
            get
            {
                return frames;
            }

            set
            {
                frames = value;
            }
        }
        #endregion
        public GameObject(Game game, SpriteBatch spriteBatch, Texture2D tex, float scale, float rotation,Vector2 initPosition, Vector2 speed) : base(game)
        {
           // this.position = initPosition;
            //this.initPosition = initPosition;
            this.speed = speed;
            this.tex = tex;
            this.spriteBatch = spriteBatch;           
            this.scale = scale;
            this.rotation = rotation;
           // this.dimension = new Vector2((float)tex.Width / COLUMNS, (float)tex.Height / ROWS);
           // this.origin = new Vector2((srcRect.Width / 2, (srcRect.Height) / 2);

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
           
            base.Draw(gameTime);
        }

        #region Methods accessible to all drawable Game objects
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

        public Rectangle getBounds()
        {
            return new Rectangle((int)(position.X * scale), (int)(position.Y * scale), (int)(srcRect.Width * scale), (int)(srcRect.Height * scale));
        }

        public Vector2[] SetPattern(Vector2[] Pattern)
        {
            pattern = Pattern;
            
            for (int i = 1; i < Pattern.Length; i++)
            {
                pattern[0] = position;
                pattern[i] = pattern[i-1] + Pattern[i]; 
            }
            return pattern;
        }

        public bool Move(Vector2 Pos, Vector2 Speed)
        {
            //create a triangle from origin to vector 2 speed
            Triangle.CreateTriangle(Vector2.Zero, Speed);
            //sets this objects true speed to the hypotenuse of that triangle
            //this ensures that you are always going the same speed regardless of the angle at which the object is moving
            float TrueSpeed = Triangle.Hypotenuse;
            //create a triangle using your location and the location you are going
            Triangle.CreateTriangle(Position, Pos);
            //get the next position using your current postion the hypotenuse distance you can cover and the angle at which you must travel
            Position = Triangle.getPoint(position, TrueSpeed, Triangle.Alpha);
            //returns true if the distance to position < your truespeed 
            if (Vector2.Distance(Position,Pos) < TrueSpeed)
            {
                position = Pos;
                return true;
            }
            else
            {
                return false;
            }
            
        }
        
       
        public void DrawFrom(Vector2 position, Vector2 origin)
        {
            drawPos = position - origin;
        }
        #endregion
    }


}
