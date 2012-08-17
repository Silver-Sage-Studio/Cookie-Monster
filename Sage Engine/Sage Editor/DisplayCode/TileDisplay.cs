using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage_Editor
{
    public class TileDisplay : GraphicsDeviceControl
    {

        public event EventHandler OnInitialise;
        public event EventHandler OnDraw;

        protected override void Initialize()
        {
            if (OnInitialise != null)
            {
                OnInitialise(this, null);
            }
        }

        protected override void Draw()
        {
            if (OnDraw != null)
            {
                OnDraw(this, null);
            }
        }
    }
}
