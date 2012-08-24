using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Engine
{
    public class Camera
    {

        //Call this Propety In every spritebatch to draw Srites Relative to Screen Co-Oridnates form world co-ordinates.
        public static Matrix TransFormMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-position, 0f));
            }
        }


        //Set this Camera Postion.
        public static Vector2 position = Vector2.Zero;



        //Call this Method In the main Update to clamp Camera to the Map, Pass in Map Width And Height for the method to work.
        public static void ClampToMap(int Width, int Height)
        {
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.X > Width)
            {
                position.X = Width;
            }
            if (position.Y > Height)
            {
                position.Y = Height;
            }
        }

        
    }
}
