using System;
using System.Windows.Forms;

namespace Engine.UI {

    public enum AnchorX {
        LEFT = 4,
        RIGHT = 8,
        CENTER = 12 // Because of how winforms does anchors, we just manually set "center" as the flag for both left + right
    }

    public enum AnchorY {
        TOP = 1,
        BOTTOM = 2,
        CENTER = 3 // Because of how winforms does anchors, we just manually set "center" as the flag for both top + bottom
    }

    public struct UIAnchor {

        public UIAnchor(AnchorX _x, AnchorY _y) {
            x = _x;
            y = _y;
        }

        public static implicit operator AnchorStyles(UIAnchor self) {
            return (AnchorStyles)(((int)self.x) + ((int)self.y));
        }

        public AnchorX x {
            get;
            private set;
        }

        public AnchorY y {
            get;
            private set;
        }

    }

}