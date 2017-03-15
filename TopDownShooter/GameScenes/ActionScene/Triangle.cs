using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    public class Triangle
    {
        #region Global variables
        //Global static variable of the triangle class 
        private static float hypotenuse;
        private static float height;
        private static float length;

        private static float adjacent;       
        private static float opposite;

        private static Vector2 enemyPos;
        private static Vector2 playerPos;

        private static Vector2 a;
        private static Vector2 b;
        private static Vector2 c;
        private static Vector2 c2;
        private static Vector2 displacement;
        private static float alpha;
        private static float beta;
        private static float right = 90;
        #endregion
        #region Properties
        public static float Hypotenuse
        {
            get
            {
                return hypotenuse;
            }

            set
            {
                hypotenuse = value;
            }
        }

        

        public static float Adjacent
        {
            get
            {
                return adjacent;
            }

            set
            {
                adjacent = value;
            }
        }

        public static float Opposite
        {
            get
            {
                return opposite;
            }

            set
            {
                opposite = value;
            }
        }

        public static float Alpha
        {
            get
            {
                return alpha;
            }

            set
            {
                alpha = value;
            }
        }

        public static float Beta
        {
            get
            {
                return beta;
            }

            set
            {
                beta = value;
            }
        }

        public static Vector2 Displacement
        {
            get
            {
                return displacement;
            }

            set
            {
                displacement = value;
            }
        }

        public static Vector2 A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public static Vector2 B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }
        #endregion
        /// <summary>
        /// Creates a triangle using two ends of the hypotenuse line segment or vector
        /// </summary>
        /// <param name="Position">The position of origin of the hypotenuse</param>
        /// <param name="DestinationPosition">the terminaton point of the hypotenuse</param>
        /// 
        public static void CreateTriangle(Vector2 Position, Vector2 DestinationPosition)
        {
            a = Position;
            b = DestinationPosition;
            hypotenuse = Vector2.Distance(Position, DestinationPosition);
            height = Math.Abs(DestinationPosition.Y - Position.Y);
            length = Math.Abs(DestinationPosition.X - Position.X);
            c = new Vector2(DestinationPosition.X, Position.Y);
            //c2 = new Vector2(Position.X, DestinationPosition.Y);
            displacement = Vector2.Subtract(DestinationPosition, Position);
           
            opposite = height;
            adjacent = length;
            alpha = (float)Math.Atan((double)opposite / (double)adjacent);
            beta = right - alpha;
        }

        #region Rotates enemy ship
        /// <summary>
        /// Supposed to generate the proper rotation of a given trianlge attempt # 1000
        /// </summary>
        /// <param name="Position">position of origin</param>
        /// <param name="DestinationPosition">terminating position</param>
        /// <returns>An angle in radians</returns>
        public static float getRotation(Vector2 Position, Vector2 DestinationPosition)
        {

            enemyPos = Position;
            playerPos = DestinationPosition;

            //HANDLING CARTESIAN PLANE {radians = angle * (Math.PI / 180);}
            //playeShip is positioned (relative to enemyShip): Top-Right
            if ((enemyPos.X - playerPos.X) < 0 && (enemyPos.Y - playerPos.Y) > 0)
            {
                height = enemyPos.Y - playerPos.Y;
                length = playerPos.X - enemyPos.X;

                opposite = Math.Abs(length);
                adjacent = Math.Abs(height);

                alpha = (float)Math.Atan((double)opposite / (double)adjacent);
            }
            //playeShip is positioned (relative to enemyShip): Bottom-Right
            else if ((enemyPos.X - playerPos.X) < 0 && (enemyPos.Y - playerPos.Y) < 0)
            {
                height = playerPos.Y - enemyPos.Y;
                length = playerPos.X - enemyPos.X;

                opposite = Math.Abs(height);
                adjacent = Math.Abs(length);
                float angle = 90;

                alpha = (float)Math.Atan((double)opposite / (double)adjacent) + MathHelper.ToRadians(angle);
            }
            //playeShip is positioned (relative to enemyShip): Bottom-Left
            else if ((enemyPos.X - playerPos.X) > 0 && (enemyPos.Y - playerPos.Y) < 0)
            {
                height = enemyPos.Y - playerPos.Y;
                length = playerPos.X - enemyPos.X;

                opposite = Math.Abs(length);
                adjacent = Math.Abs(height);
                float angle = 180;

                alpha = (float)Math.Atan((double)opposite / (double)adjacent) + MathHelper.ToRadians(angle);
            }
            //playeShip is positioned (relative to enemyShip): Top-Left
            else if ((enemyPos.X - playerPos.X) > 0 && (enemyPos.Y - playerPos.Y) > 0)
            {
                height = playerPos.Y - enemyPos.Y;
                length = playerPos.X - enemyPos.X;

                opposite = Math.Abs(height);
                adjacent = Math.Abs(length);
                float angle = 270;

                alpha = (float)Math.Atan((double)opposite / (double)adjacent) + MathHelper.ToRadians(angle);
            }

            //HANDLING DIVIDE BY ZEROS {radians = angle * (Math.PI / 180);}
            //playeShip is positioned (relative to enemyShip): Directly Above
            else if ((enemyPos.X - playerPos.X) == 0 && (enemyPos.Y - playerPos.Y) > 0)
            {
                //alpha is 0 because 'angle * (Math.PI / 180)' = 0 when angle is 0
                alpha = 0;
            }
            //playeShip is positioned (relative to enemyShip): Directly Below
            else if ((enemyPos.X - playerPos.X) == 0 && (enemyPos.Y - playerPos.Y) < 0)
            {
                float angle = 180;
                alpha = MathHelper.ToRadians(angle);
                //alpha = angle * (float)(Math.PI / 180);
            }
            //playeShip is positioned (relative to enemyShip): Directly Right
            else if ((enemyPos.X - playerPos.X) < 0 && (enemyPos.Y - playerPos.Y) > 0)
            {
                float angle = 90;
                alpha = MathHelper.ToRadians(angle);
                //alpha = angle * (float)(Math.PI / 180);
            }
            //playeShip is positioned (relative to enemyShip): Directly Left
            else if ((enemyPos.X - playerPos.X) < 0 && (enemyPos.Y - playerPos.Y) > 0)
            {
                float angle = 270;
                alpha = MathHelper.ToRadians(angle);
                //alpha = angle * (float)(Math.PI / 180);
            }
            return alpha;


        }
        #endregion





        /// <summary>
        /// gets the next point along the hypotenuse using the true speed of an object and its current location
        /// </summary>
        /// <param name="Pos">the current position of the object</param>
        /// <param name="TrueSpeed">the true speed of the object</param>
        /// <param name="angle">the angle of the hypotenuse</param>
        /// <returns>the next point along the hypotenuse</returns>
        public static Vector2 getPoint(Vector2 Pos, float TrueSpeed, float angle)
            
            {
                Vector2 destinationPos = new Vector2(0, 0);
                Vector2 startingPos = Pos;
               // Vector2 rightPos = Triangle.c;
                
                float y = 0;
                float x = 0;
                float h = TrueSpeed;

            displacement = new Vector2(displacement.X / Math.Abs(displacement.X), displacement.Y / Math.Abs(displacement.Y));

           
                y = (float)(Math.Sin(alpha) * h);
                x = (float)(Math.Cos(alpha) * h);
          

            destinationPos = new Vector2(startingPos.X + (displacement.X * x), startingPos.Y + (displacement.Y * y));    
                
                return destinationPos;
            }

        /// <summary>
        /// given a hypotenuse distance and current position and angle return the vector representation of that speed
        /// </summary>
        /// <param name="Pos">current position</param>
        /// <param name="TrueSpeed">true speed of object</param>
        /// <param name="angle">angle of hypotenuse</param>
        /// <returns>the vector 2 speed</returns>
        public static Vector2 getSpeed(Vector2 Pos, float TrueSpeed, float angle)

        {
            Vector2 speed = new Vector2(0, 0);
            Vector2 startingPos = Pos;
            //Vector2 rightPos = Triangle.c;

            float y = 0;
            float x = 0;
            float h = TrueSpeed;

            displacement = new Vector2(displacement.X / Math.Abs(displacement.X), displacement.Y / Math.Abs(displacement.Y));

            if (opposite == height)
            {
                y = (float)(Math.Sin(alpha) * h);
                x = (float)(Math.Cos(alpha) * h);
            }
            else
            {
                x = (float)(Math.Sin(alpha) * h);
                y = (float)(Math.Cos(alpha) * h);
            }

            speed = new Vector2(x * displacement.X, y * displacement.Y);

            return speed;
        }
    }
}
