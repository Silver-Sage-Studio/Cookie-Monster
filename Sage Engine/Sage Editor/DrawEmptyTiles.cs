using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Editor
{
   public static class DrawEmptyTiles
    {
       public static Texture2D EmptyTileTexture;
       public static Color BaseColor = Color.White;
       public static Color SelectedColor = Color.Red;
       public static Form1 form;

       public static void Initialise(Form1 forms)
       {
           form = forms;
           EmptyTileTexture = form.EmptyTile;
       }

       public static void DrawTiles()
       {
           form.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);
          
           for (int x = 0; x < form.currentLayer.LayerWidthinTiles; x++)
           {
               for (int y = 0; y < form.currentLayer.LayerHeightinTiles; y++)
               {
                   form.spriteBatch.Draw(EmptyTileTexture,
                       new Rectangle(x * TileLayer.GetTileWidth, y * TileLayer.GetTileHeight, TileLayer.GetTileWidth, TileLayer.GetTileHeight),
                       Color.Green);
               }
           }
           form.spriteBatch.End();
       }
    }
}
