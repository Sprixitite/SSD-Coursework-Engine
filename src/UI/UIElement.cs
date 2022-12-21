using System;

using Engine.Graphics;

namespace Engine.UI {

    public abstract class UIElement {

        public Vector2 anchor {
            get;
            protected set;
        }

        public UIPosition position {
            get;
            protected set;
        }

        public abstract UIPosition size {
            get;
            protected set;
        }

        public UIElement parent {
            get;
            set;
        }

        

    }

}