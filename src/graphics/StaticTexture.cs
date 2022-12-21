using System;
using System.Drawing;

namespace Engine.Graphics {

    public sealed class StaticTexture : Texture {

        public override Image get_texture() => texture;
        private Image texture;

    }

}