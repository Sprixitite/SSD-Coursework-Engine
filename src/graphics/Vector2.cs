using System;

namespace Engine.Graphics {

    public sealed class Vector2 {

        public Vector2() { x = 0; y = 0; }
        public Vector2(float _x, float _y) { x = _x; y = _y; }

        public static bool operator ==(Vector2 self, Vector2 other) { return (self.x==other.x) && (self.y==other.y); }
        public static bool operator !=(Vector2 self, Vector2 other) { return (self.x!=other.x) && (self.y!=other.y); }
        public static Vector2 operator +(Vector2 self, Vector2 other) { return new Vector2(self.x+other.x, self.y+other.y); }
        public static Vector2 operator -(Vector2 self, Vector2 other) { return new Vector2(self.x-other.x, self.y-other.y); }
        public static Vector2 operator *(Vector2 self, float other) { return new Vector2(self.x*other, self.y*other); }
        public static Vector2 operator /(Vector2 self, float other) { return new Vector2(self.x/other, self.y/other); }

        public static implicit operator System.Drawing.PointF(Vector2 self) { return new System.Drawing.PointF(self.x, self.y); }

        public float magnitude() { return MathF.Sqrt(MathF.Pow(x, 2.0f) + MathF.Pow(y, 2.0f)); }
        public Vector2 unit() { float mag = magnitude(); return new Vector2(x/mag, y/mag); }
        public float dot(Vector2 other) { float total = 0.0f; total+=(this.x*other.x); total+=(this.y*other.y); return total; }
        public Vector2 interp(Vector2 other, float interp_amt) { Vector2 offset = other-this; return this+(offset*interp_amt); }
        public Vector2 midpoint(Vector2 other) { return (this+other)/2.0f; }

        public float x {
            get;
            set;
        }

        public float y {
            get;
            set;
        }

    }

}