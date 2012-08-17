using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Engine
{
   public class TileLayer
    {
        int[,] layer;
        List<Texture2D> textureList = new List<Texture2D>();

        float alpha = 1f;
    }
}
