using System;

using Engine.Graphics;

namespace Engine.Data {

    public struct Bounds {

        private Vector3 min;
        private Vector3 max;

        public Vector3 get_min() => min;
        public Vector3 get_max() => max;

    }

}