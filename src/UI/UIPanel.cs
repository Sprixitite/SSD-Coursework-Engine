using System;
using System.Drawing;
using System.Windows.Forms;

namespace Engine.UI {

    public class UIPanel : UIElement {

        public UIPanel() {

            underlying = new Panel();
            underlying.Width = 80;
            underlying.Height = 80;
            underlying.BackColor = Color.Blue;

        }

    }

}