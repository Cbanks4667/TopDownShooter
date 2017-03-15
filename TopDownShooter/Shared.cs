using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace TopDownShooter
{
    public class Shared
    {
        public static Vector2 stage;
        public static Vector2 dimension;
        public static Vector2[] Positions = new Vector2[25];
        public static Vector2[][] Patterns;
        public static Vector2 playersShip;
        //public static int[][] Patterns;
        public static Vector2[][] Formations;

        public const int PATTERN_WAVE = 0;
        public const int PATTERN_LOOP = 1;
        public const int PATTERN_STRAFE = 2;
        public const int PATTERN_DIAGONAL = 3;
        public const int PATTERN_DOWN = 4;

        public const int FORMATION_DIAMOND = 0;
        public const int FORMATION_BOX = 1;
        public const int FORMATION_VEE = 2;
        public const int FORMATION_CLUSTER = 3;


        public const int STAGE_TOP_LEFT = 24;
        public const int STAGE_TOP_LEFT_CENTER = 23;
        public const int STAGE_TOP_CENTER = 22;
        public const int STAGE_TOP_RIGHT_CENTER = 21;
        public const int STAGE_TOP_RIGHT = 20;

        public const int STAGE_BELOW_TOP_LEFT = 19;
        public const int STAGE_BELOW_TOP_LEFT_CENTER = 18;
        public const int STAGE_BELOW_TOP_CENTER = 17;
        public const int STAGE_BELOW_TOP_RIGHT_CENTER = 16;
        public const int STAGE_BELOW_TOP_RIGHT = 15;

        public const int STAGE_CENTER_LEFT = 14;
        public const int STAGE_CENTER_LEFT_CENTER = 13;
        public const int STAGE_CENTER = 12;
        public const int STAGE_CENTER_RIGHT_CENTER = 11;
        public const int STAGE_CENTER_RIGHT = 10;

        public const int STAGE_ABOVE_BOTTOM_LEFT = 9;
        public const int STAGE_ABOVE_BOTTOM_LEFT_CENTER = 8;
        public const int STAGE_ABOVE_BOTTOM_CENTER = 7;
        public const int STAGE_ABOVE_BOTTOM_RIGHT_CENTER = 6;
        public const int STAGE_ABOVE_BOTTOM_RIGHT = 5;

        public const int STAGE_BOTTOM_LEFT = 4;
        public const int STAGE_BOTTOM_LEFT_CENTER = 3;
        public const int STAGE_BOTTOM_CENTER = 2;
        public const int STAGE_BOTTOM_RIGHT_CENTER = 1;
        public const int STAGE_BOTTOM_RIGHT = 0;
        public static void CreatePositions(Vector2 stage)
        {
            int index = 0;
            dimension = new Vector2(stage.X / 4, stage.Y / 4);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {

                    Positions[index] = new Vector2(stage.X - (j * dimension.X), stage.Y - (i * dimension.Y));
                    index++;
                }

            }

        }

        
        public static void CreatePatterns()
        {
            Patterns = new Vector2[][]
            {
                new Vector2[] {Positions[STAGE_BELOW_TOP_LEFT],Positions[STAGE_ABOVE_BOTTOM_RIGHT_CENTER],Positions[STAGE_ABOVE_BOTTOM_LEFT_CENTER],Positions[STAGE_BELOW_TOP_RIGHT_CENTER]},
                new Vector2[] {Positions[STAGE_BELOW_TOP_LEFT],Positions[STAGE_ABOVE_BOTTOM_LEFT_CENTER],Positions[STAGE_ABOVE_BOTTOM_RIGHT_CENTER],Positions[STAGE_BELOW_TOP_RIGHT_CENTER]},
                new Vector2[] {Positions[STAGE_BELOW_TOP_LEFT],Positions[STAGE_ABOVE_BOTTOM_LEFT_CENTER],Positions[STAGE_ABOVE_BOTTOM_RIGHT_CENTER],Positions[STAGE_BELOW_TOP_RIGHT_CENTER]},
                new Vector2[] {Positions[STAGE_BELOW_TOP_LEFT],Positions[STAGE_ABOVE_BOTTOM_LEFT_CENTER],Positions[STAGE_ABOVE_BOTTOM_RIGHT_CENTER],Positions[STAGE_BELOW_TOP_RIGHT_CENTER]},
                new Vector2[] { Positions[STAGE_BELOW_TOP_LEFT], Positions[STAGE_ABOVE_BOTTOM_LEFT_CENTER], Positions[STAGE_ABOVE_BOTTOM_RIGHT_CENTER], Positions[STAGE_BELOW_TOP_RIGHT_CENTER] }
            };
        }



        public static void CreateFormations(int numberOfObjects)
        {
            if (numberOfObjects % 4 != 0)
            {
                numberOfObjects += numberOfObjects % 4;
            }
            Vector2[] formation = new Vector2[numberOfObjects];
            Formations = new Vector2[][]
           {
                new Vector2[numberOfObjects],
                new Vector2[numberOfObjects],
                new Vector2[numberOfObjects],
                new Vector2[numberOfObjects],
                new Vector2[numberOfObjects]
           };
            int count = 0;
            formation[count] = new Vector2(0, 0);
            int VGAP = 20;
            int HGAP = 20;
            for (int i = 0; i < numberOfObjects / 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {


                    formation[count] = formation[0] + new Vector2(i * VGAP, j * HGAP);
                    count++;
                }

            }
            Formations[1] = formation;

        }
        public static Vector2 ConvertVector(Vector2 vector)
        {
            return vector = new Vector2(Math.Abs((int)(vector.X)), Math.Abs((int)vector.Y));

        }

        public static float ConvertToRad(float degrees)
        {
            return (degrees * 0175);
        }

        

    }
}


