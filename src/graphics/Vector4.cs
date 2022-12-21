using System;

namespace Engine.Graphics {

    public sealed class Vector4 : IVector<Vector4> {

        public Vector4() { x = 0; y = 0; z = 0; w=0; }
        public Vector4(float _x, float _y, float _z, float _w) { x = _x; y = _y; z = _z; w = _w; }

        public float x {
            get;
            set;
        }

        public float y {
            get;
            set;
        }

        public float z {
            get;
            set;
        }

        public float w {
            get;
            set;
        }

        public bool Equals(Vector4 other) {
            return (
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w
            );
        }

        public override string ToString() {
            return "Vector4 {\n\t" + x + ",\n\t" + y + ",\n\t" + z + ",\n\t" + w + "\n}";
        }

        public float dot(Vector4 other) { float total = 0.0f; total+=(this.x*other.x); total+=(this.y*other.y); total+=(this.z*other.z); total+=(this.w*other.w); return total; }
        public float magnitude() { return (float)Math.Sqrt((float)Math.Pow(x, 2.0f) + (float)Math.Pow(y, 2.0f) + (float)Math.Pow(z, 2.0f) + (float)Math.Pow(w, 2.0f)); }

        public Vector4 interp(Vector4 other, float interp_amt) { Vector4 offset = other-this; return this+(offset*interp_amt); }
        public Vector4 midpoint(Vector4 other) { return (this+other)/2.0f; }
        public Vector4 unit() { float mag = magnitude(); return new Vector4(x/mag, y/mag, z/mag, w/mag); }

        public static bool operator ==(Vector4 self, Vector4 other) => self.Equals(other);
        public static bool operator !=(Vector4 self, Vector4 other) => !self.Equals(other);
        public static Vector4 operator +(Vector4 self, Vector4 other) { return new Vector4(self.x+other.x, self.y+other.y, self.z+other.z, self.w+other.w); }
        public static Vector4 operator -(Vector4 self, Vector4 other) { return new Vector4(self.x-other.x, self.y-other.y, self.z-other.z, self.w-other.w); }
        public static Vector4 operator *(Vector4 self, float other) { return new Vector4(self.x*other, self.y*other, self.z*other, self.w*other); }
        public static Vector4 operator /(Vector4 self, float other) { return new Vector4(self.x/other, self.y/other, self.z/other, self.w/other); }

        public static readonly Vector4 ZERO = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

    }

}