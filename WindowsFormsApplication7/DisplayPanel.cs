using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public class DisplayPanel : Panel
    {
        public DisplayPanel()
        {
            this.DoubleBuffered = true;

            //// or

            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //UpdateStyles();
        }
    }
}
