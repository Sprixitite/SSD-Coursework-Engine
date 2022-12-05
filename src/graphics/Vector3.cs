using System;

namespace Engine.Graphics {

    public sealed class Vector3 {

        public Vector3() { x = 0; y = 0; z = 0; }
        public Vector3(float _x, float _y, float _z) { x = _x; y = _y; _z = _z; }

        public static bool operator ==(Vector3 self, Vector3 other) { return (self.x==other.x) && (self.y==other.y) && (self.z==other.z); }
        public static bool operator !=(Vector3 self, Vector3 other) { return (self.x!=other.x) && (self.y!=other.y) && (self.z==other.z); }
        public static Vector3 operator +(Vector3 self, Vector3 other) { return new Vector3(self.x+other.x, self.y+other.y, self.z+other.z); }
        public static Vector3 operator -(Vector3 self, Vector3 other) { return new Vector3(self.x-other.x, self.y-other.y, self.z-other.z); }
        public static Vector3 operator *(Vector3 self, float other) { return new Vector3(self.x*other, self.y*other, self.z*other); }
        public static Vector3 operator /(Vector3 self, float other) { return new Vector3(self.x/other, self.y/other, self.z/other); }

        public float magnitude() { return MathF.Sqrt(MathF.Pow(x, 2.0f) + MathF.Pow(y, 2.0f) + MathF.Pow(z, 2.0f)); }
        public Vector3 unit() { float mag = magnitude(); return new Vector3(x/mag, y/mag, z/mag); }
        public float dot(Vector3 other) { float total = 0.0f; total+=(this.x*other.x); total+=(this.y*other.y); total+=(this.z*other.z); return total; }
        public Vector3 interp(Vector3 other, float interp_amt) { Vector3 offset = other-this; return this+(offset*interp_amt); }
        public Vector3 midpoint(Vector3 other) { return (this+other)/2.0f; }

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

    }

}