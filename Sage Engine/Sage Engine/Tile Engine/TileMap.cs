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
       }

       #endregion
    }

}
