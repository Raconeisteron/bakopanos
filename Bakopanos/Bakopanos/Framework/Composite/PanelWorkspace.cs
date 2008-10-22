using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bakopanos.Framework.Composite
{
    public class PanelWorkspace : Panel, IWorkspace
    {
        public void Show(Control control)
        {
            control.Dock = DockStyle.Fill;

            Controls.Add(control);
        }
    }
}