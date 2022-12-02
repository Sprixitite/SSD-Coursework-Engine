using System;

namespace Engine.Graphics {

    public struct Line2D {

        public Line2D(float num, float den, float off) {
            gradient = num/den;
            offset = off;
        }

        public Line2D(Vector2 p1, Vector2 p2) {
            gradient = ( p1.y-p2.y ) / ( p1.x-p2.x );
            offset = -( p1.y - ( p1.x*gradient ) );
        }

        public Line2D(float grad, float off) {
            gradient = grad;
            offset = off;
        }

        public float gradient {
            get;
            private set;
        }
        public float offset {
            get;
            private set;
        }

        public float y_of(float x) {
            return (x*gradient) + offset;
        }

        public float x_of(float y) {
            return (y-offset) / gradient;
        }

    }

}