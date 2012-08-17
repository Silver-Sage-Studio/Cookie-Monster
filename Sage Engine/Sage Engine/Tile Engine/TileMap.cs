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
    }

}
