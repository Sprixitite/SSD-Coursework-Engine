using System;

namespace Engine.Graphics {

    public sealed class Vector3 : IVector<Vector3> {

        public Vector3() { x = 0; y = 0; z = 0; }
        public Vector3(float _x, float _y, float _z) { x = _x; y = _y; z = _z; }

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

        public bool Equals(Vector3 other) {
            return (
                x == other.x &&
                y == other.y &&
                z == other.z
            );
        }

        public override string ToString() {
            return "Vector3 {\n\t" + x + ",\n\t" + y + ",\n\t" + z + "\n}";
        }

        public float dot(Vector3 other) { float total = 0.0f; total+=(this.x*other.x); total+=(this.y*other.y); total+=(this.z*other.z); return total; }
        public float magnitude() { return (float)Math.Sqrt((float)Math.Pow(x, 2.0f) + (float)Math.Pow(y, 2.0f) + (float)Math.Pow(z, 2.0f)); }
        public Vector3 unit() { float mag = magnitude(); return new Vector3(x/mag, y/mag, z/mag); }
        public Vector3 interp(Vector3 other, float interp_amt) { Vector3 offset = other-this; return this+(offset*interp_amt); }
        public Vector3 midpoint(Vector3 other) { return (this+other)/2.0f; }

        public static bool operator ==(Vector3 self, Vector3 other) => self.Equals(other);
        public static bool operator !=(Vector3 self, Vector3 other) => !self.Equals(other);
        public static Vector3 operator +(Vector3 self, Vector3 other) { return new Vector3(self.x+other.x, self.y+other.y, self.z+other.z); }
        public static Vector3 operator -(Vector3 self, Vector3 other) { return new Vector3(self.x-other.x, self.y-other.y, self.z-other.z); }
        public static Vector3 operator *(Vector3 self, float other) { return new Vector3(self.x*other, self.y*other, self.z*other); }
        public static Vector3 operator /(Vector3 self, float other) { return new Vector3(self.x/other, self.y/other, self.z/other); }

        public static readonly Vector3 ZERO = new Vector3(0.0f, 0.0f, 0.0f);

    }

}