using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sage_Engine
{
   public class TileMap
   {
       #region Varaibles
       List<TileLayer> layers = new List<TileLayer>();

       int[,] CollisionMap;
       #endregion

       #region Properties
       /// <summary>
       /// Array list of all the tile layers
       /// </summary>
       public List<TileLayer> Layers
       {
           get
           {
               return layers;
           }
       }


       public int CollisoionMapWidth
       {
           get
           {
               return CollisionMap.GetLength(1);
           }
       }

       public int ColliosnMapHeight
       {
           get
           {
               return CollisionMap.GetLength(0);
           }
       }
       #endregion

       #region Logic
       public void Draw(SpriteBatch spriteBatch)
       {
           foreach (TileLayer layer in layers)
           {
               layer.Draw(spriteBatch);
           }
       }
       #endregion

       #region Helper-Methods

       public void SetCollisionTile(int x, int y, int CollisionIndex)
       {
           if ((y >= 0) && (y < ColliosnMapHeight) &&
            (x >= 0) && (x <= CollisoionMapWidth))
               CollisionMap[y, x] = CollisionIndex;
       }


       /// <summary>
       /// Map Width in tiles. (width of largest layer in map)
       /// </summary>
       /// <returns></returns>
       public int MapWidth()
       {
           int max = -10;
           foreach (TileLayer layer in layers)
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
           foreach (TileLayer layer in layers)
           {
               max = Math.Max(layer.LayerHeightinTiles, max);
           }

           return max;
       }

       /// <summary>
       /// Add Layers
       /// </summary>
       /// <param name="layer"></param>
       public void Addlayer(TileLayer layer)
       {
           layers.Add(layer);
           Camera.MapHeight = MapHeight() * TileLayer.GetTileHeight;
           Camera.MapWidth = MapWidth() * TileLayer.GetTileWidth;
           modifyCollisionMap();
       }


           //public void SetCellIndex(int x, int y, int Collided)
           //{
           //    if ((y >= 0) && (y < LayerHeightinTiles) &&
           //        (x >= 0) && (x <= LayerWidthinTiles))
           //        layer[y, x] = Index;
           //}


       private void modifyCollisionMap()
       {
           int maxWidth = -1;
           int maxHeight = -1;

           foreach (TileLayer layer in Layers)
           {
              if(layer.LayerWidthinTiles > maxWidth)
               {
                   maxWidth = layer.LayerWidthinTiles;
               }
               if(layer.LayerHeightinTiles > maxHeight)
               {
                   maxHeight = layer.LayerHeightinTiles;
               }
           }

           
           if (Layers.Count > 0)
           {
               if (CollisionMap != null)
               {
                   int[,] TempMap = (int[,])CollisionMap.Clone();

                   CollisionMap = new int[maxWidth, maxHeight];

                   for (int x = 0; x < Math.Min(maxWidth, TempMap.GetLength(0)); x++)
                   {
                       for (int y = 0; y < Math.Min(maxHeight, TempMap.GetLength(1)); y++)
                       {
                           CollisionMap[x, y] = TempMap[x, y];
                       }
                   }
               }
               else
               {
                   CollisionMap = new int[maxWidth, maxHeight];
               }

                
           }
           else
           {
               CollisionMap = null;
           }

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

           int lastlayer;
           if (layers.Count > 0)
           {
                lastlayer = layers.Count - 1;
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
               TileLayer layer = layers[lastlayer];
               Index = layer.GetCellIndex(tileX, tileY);
               lastlayer = layers.Count - 1;
           }
           while ((Index == -1) && (lastlayer >= 0));

           return Index;
       }

       public void RemoveLayer(TileLayer Layer)
       {
           layers.Remove(Layer);
           //modifyCollisionMap();
       }

       #endregion
    }

}
