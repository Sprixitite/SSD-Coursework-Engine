using System;

using Engine.Graphics;

namespace Engine.Data {

    public abstract class Brush {

        // Can't be publically set, mutate instead
        public Transform transform {
            get;
            private set;
        }

        

    }

}