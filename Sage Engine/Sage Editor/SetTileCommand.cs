using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Editor
{
   public class SetTileCommand  : Command
    {
        protected int PreviousTexture;
        protected TileLayer layer;
        protected Vector2 TileLocation;

       public SetTileCommand(Form1 form)
           :base(form)
       {
       }

        public override void Excute()
        {
            TileLayer currentLayer = this.form.currentLayer;
            if(form.TextureList.Items.Count>0 && form.TextureList.SelectedItem!=null){
                if ((this.form.TileX != null) || (this.form.TileY != null))
                {
                    int TileX = (int)this.form.TileX;
                    int TileY = (int)this.form.TileY;

                    this.TileLocation = new Vector2(TileX, TileY);
                    this.layer = currentLayer;
                    this.PreviousTexture = currentLayer.GetCellIndex(TileX, TileY);

                    string TextureToSet = form.TextureList.SelectedItem.ToString();

                    Texture2D text = form.dictTextures[TextureToSet];
                    int IndexToSet = currentLayer.HasTexture(text);
                    currentLayer.SetCellIndex(TileX, TileY, IndexToSet);
                }
            }
        }

        public override void Undo()
        {
            form.currentLayer = this.layer;
            layer.SetCellIndex((int)TileLocation.X, (int)TileLocation.Y, this.PreviousTexture);
        }

        public override Command Clone()
        {
            return new SetTileCommand(this.form);
        }
    }
}
