using System;

using Engine.Graphics;

namespace Engine.UI {

    public sealed class UIPosition {

        public UIPosition() {
            scale = Vector2.ZERO;
            offset = Vector2.ZERO;
        }

        // Position on the screen in screen space
        // E.g: (0.5f, 0.5f) with anchor (0.5f, 0.5f) is the middle of the screen
        public Vector2 scale;

        // Offset from the scale in pixels
        public Vector2 offset;

    }

}