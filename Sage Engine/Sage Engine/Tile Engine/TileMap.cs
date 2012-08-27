﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sage_Engine
{
   public class TileMap
    {
       List<TileLayer> Layers = new List<TileLayer>();

      

       public void Draw(SpriteBatch spriteBatch)
       {
           foreach (TileLayer layer in Layers)
           {
               layer.Draw(spriteBatch);
           }
       }


       #region Helper-Methods


       /// <summary>
       /// Map Width in tiles. (width of largest layer in map)
       /// </summary>
       /// <returns></returns>
       public int MapWidth()
       {
           int max = -10;
           foreach (TileLayer layer in Layers)
           {
               max = Math.Max(layer.LayerWidthinTiles, max);
           }

           return max;
       }

       /// <summary>
       /// Map Height in tiles. (Width of largest layer in map).
       /// </summary>
       /// <returns></returns>
       public int MapHeight()
       {
           int max = -10;
           foreach (TileLayer layer in Layers)
           {
               max = Math.Max(layer.LayerHeightinTiles, max);
           }

           return max;
       }


       public void Addlayer(TileLayer layer)
       {
           Layers.Add(layer);
           Camera.MapHeight = MapHeight();
           Camera.MapWidth = MapWidth();
       }


       /// <summary>
       /// Enter Screen Pixel Co-ordinates to get the index of the tile at the specified co-ordinates.
       /// -1 if no tile at those co-ordinates or other errors, Checks the top layer first, if Transparent, 
       /// Checks the layerBelow it for an Index and so on.
       /// </summary>
       /// <param name="Pixelx"></param>
       /// <param name="PixelY"></param>
       /// <returns></returns>
       public int TileAtPixel(int Pixelx, int PixelY)
       {
           //SomeOne please test this method and give me feedback.(Faraz)

           int lastlayer;
           if (Layers.Count > 0)
           {
                lastlayer = Layers.Count - 1;
           }
           else
           {
               return -1;
           }

           
           int tileX = Pixelx + (int)Camera.Position.X;
           int tileY = PixelY + (int)Camera.Position.Y;

           tileX = tileX / TileLayer.GetTileWidth;
           tileY = tileX / TileLayer.GetTileHeight;
          
           int Index;

           do
           {
               TileLayer layer = Layers[lastlayer];
               Index = layer.GetCellIndex(tileX, tileY);
               lastlayer = Layers.Count - 1;
           }
           while ((Index == -1) && (lastlayer >= 0));

           return Index;
       }

       #endregion
    }

}
