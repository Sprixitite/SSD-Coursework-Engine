using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Engine.Graphics;

namespace Engine.UI {

    public abstract class UIElement : WinformWrapper {

        public UIElement() {

            elements = new List<UIElement>();

            // Default to bottom left
            anchor = new UIAnchor(AnchorX.LEFT, AnchorY.BOTTOM);

            // Default to four pixels from the bottom left on both axes
            position = new UIPosition(0, 0, 4, 4);

            // Default to (x-4)/2 and (y-4)/2
            // We subtract 4 to make up for the previous offset
            _size = new UIPosition(0.5f, 0.5f, -4, -4);

        }

        public UIAnchor anchor {
            get;
            protected set;
        }

        public UIPosition position {
            get;
            set;
        }

        public UIPosition size {
            get => _size;
            set { _size = value; invalidate_size(); }
        }
        private UIPosition _size;

        protected override void on_transform_invalidated() {
            
            Form current_window = (Form)get_window().underlying;

            underlying.Left = (int)Math.Round(current_window.Width * position.scale.x) + (int)position.offset.x;
            underlying.Top = (int)Math.Round(current_window.Height * position.scale.y) + (int)position.offset.y;

            underlying.Width = (int)Math.Round(current_window.Width * size.scale.x) + (int)size.offset.x;
            underlying.Height = (int)Math.Round(current_window.Height * size.scale.y) + (int)size.offset.y;

            switch (anchor.x) {
                case AnchorX.LEFT:
                    break;
                case AnchorX.CENTER:
                    break;
                case AnchorX.RIGHT:
                    break;
            }

        }

    }

}