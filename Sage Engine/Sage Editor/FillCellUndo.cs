using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Editor
{
   public class FillCellUndo : Command
    {
       TileLayer currentLayer;
       TileLayer LayerStateBeforeRecursion;
       int fillCounter = 5000;


       public void FillCellIndex(int x, int y, int DesiredIndex)
       {
           int oldIndex = currentLayer.GetCellIndex(x, y);

           if (oldIndex == DesiredIndex || fillCounter == 0)
           {
               return;
           }

           fillCounter--;
           currentLayer.SetCellIndex(x, y, DesiredIndex);

           if (x > 0 && currentLayer.GetCellIndex(x - 1, y) == oldIndex)
           {
               FillCellIndex(x - 1, y, DesiredIndex);
           }
           if (x < currentLayer.LayerWidthinTiles - 1 && currentLayer.GetCellIndex(x + 1, y) == oldIndex)
           {
               FillCellIndex(x + 1, y, DesiredIndex);
           }
           if (y > 0 && currentLayer.GetCellIndex(x, y - 1) == oldIndex)
           {
               FillCellIndex(x, y - 1, DesiredIndex);
           }
           if (y < currentLayer.LayerHeightinTiles - 1 && currentLayer.GetCellIndex(x, y + 1) == oldIndex)
           {
               FillCellIndex(x, y + 1, DesiredIndex);
           }
       }


       public FillCellUndo(Form1 form)
        :base(form)
       {
           this.currentLayer = form.currentLayer;
       }
       
       public override void Excute()
        {
           LayerStateBeforeRecursion = new TileLayer(currentLayer.TileMapArray);
           foreach(Texture2D text in currentLayer.TexturesList)
           {
                LayerStateBeforeRecursion.AddTexture(text);
           }



        }

        public override void Undo()
        {
            form.currentLayer = LayerStateBeforeRecursion;
          }

        public override Command Clone()
        {
            return new FillCellUndo(form);
        }
    }
}
