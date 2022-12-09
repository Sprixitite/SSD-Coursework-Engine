using System;
using System.Drawing;

namespace Engine.Graphics {

    public sealed class StaticTexture : Texture {

        public Image GetImage() => texture;
        private Image texture;

    }

}