using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework;

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


       public static int SetTileWidth
       {
           set
           {
               TileWidth = Math.Max(value, 16);
           }

       }

       public static int SetTileHeight
       {
           set
           {
               TileHeight = Math.Max(value, 16);
           }

       }

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
               return layer.GetLength(1) * TileWidth;
           }
       }

       public int LayerHeightInPixels
       {
           get
           {
               return layer.GetLength(0) * TileHeight;
           }
       }

       public TileLayer()
       {
           layer = new int[,]{
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
               {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1}
           };
       }

       public void Draw(SpriteBatch spriteBatch)
       {
           spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);

           for (int x = 0; x < layer.GetLength(1); x++)
           {
               for (int y = 0; y < layer.GetLength(0); y++)
               {
                   int Index = layer[y, x];
                   if (Index != -1)
                   {
                       Texture2D texture = textureList[Index];
                       spriteBatch.Draw(texture, new Rectangle(
                           TileWidth * x, TileHeight * y, TileWidth, TileHeight), Color.White);

                   }


               }
           }

           spriteBatch.End();
           
       }

    }
}
