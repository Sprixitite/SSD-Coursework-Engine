using System;

namespace Engine.Graphics {

    public struct RenderDescriptor {
        
        public Vector2 aspect_ratio {
            get;
            private set;
        }

        public Vector2 virtual_res {
            get;
            private set;
        }

    }

}