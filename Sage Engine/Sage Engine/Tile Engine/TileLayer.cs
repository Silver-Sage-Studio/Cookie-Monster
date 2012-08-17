using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Sage_Engine
{
   public class TileLayer
    {
        int[,] layer;
        List<Texture2D> textureList = new List<Texture2D>();

        float alpha = 1f;

       //Change These According to the tile Height and width Must Be Same for all classes.
       private static  int TileWidth = 32;
       private static  int TileHeight = 32;

       public int LayerWidthinTiles
       {
           get
           {
               return layer.GetLength(1);
           }
       }

       public int LayerHeightinTiles
       {
           get
           {
               return layer.GetLength(0);
           }
       }

       public int LayerWidthInPixels
       {
           get
           {

           }
       }

    }
}
