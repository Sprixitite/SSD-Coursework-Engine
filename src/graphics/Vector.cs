using System;

namespace Engine.Graphics {

    public abstract class Vector {

        abstract protected int component_count {
            get;
        }

        public Vector(params float[] _components) {
            switch (_components.Length.CompareTo(this.component_count)) {
                case 0:
                    break;
                default:
                    throw new Exception("Type of " + this.GetType().Name + " requires " + this.component_count + " components in the constructor.");
            }
            components = _components;
        }

        private float[] components;

        public float this[char component] {
            get {
                uint idx;
                switch (Char.ToLower(component)) {
                    case 'x':
                        idx=0;
                        break;
                    case 'y':
                        idx=1;
                        break;
                    case 'z':
                        idx=2;
                        break;
                    case 'w':
                        idx=3;
                        break;
                    default:
                        idx = ((uint)component)-93;
                        break;
                }
                return components[idx];
            }
            set {
                uint idx;
                switch (Char.ToLower(component)) {
                    case 'x':
                        idx=0;
                        break;
                    case 'y':
                        idx=1;
                        break;
                    case 'z':
                        idx=2;
                        break;
                    case 'w':
                        idx=3;
                        break;
                    default:
                        idx = ((uint)component)-93;
                        break;
                }
                components[idx] = value;
            }
        }

        public float magnitude() {
            float total = 0.0f;
            foreach (float f in components) {
                total += MathF.Pow(f, 2.0f);
            }
            return total/components.Length;
        }

        public float dot(Vector other) {
            if (!(this.GetType() == other.GetType())) throw new Exception("Cannot get dot product of vectors of different types!");
            float total = 0.0f;
            for (int i=0; i<this.components.Length; i++) {
                total += this.components[i]*other.components[i];
            }
            return total;
        }

        public Vector interp(Vector other, float interp_amt) {
            return this + ( ( this-other ) * interp_amt );
        }

        public Vector midpoint(Vector other) {
            return ( this+other ) / 2.0f;
        }

        public Vector unit() {
            return this / magnitude();
        }

        public static bool operator ==(Vector self, Vector other) {

            for (int i=0; i<self.components.Length; i++) {
                switch (self.components[i] == other.components[i]) {
                    case false:
                        return false;
                    case true:
                        continue;
                }
            }

            return true;

        }

        public static bool operator !=(Vector self, Vector other) {

            for (int i=0; i<self.components.Length; i++) {
                switch (self.components[i] == other.components[i]) {
                    case false:
                        return true;
                    case true:
                        continue;
                }
            }

            return false;

        }

        public static Vector operator +(Vector self, Vector other) {

            Vector to_return = (Vector)Activator.CreateInstance(self.GetType());

            for (int i=0; i<self.components.Length; i++) {
                to_return.components[i] = (self.components[i] + other.components[i]);
            }

            return to_return;

        }

        public static Vector operator -(Vector self, Vector other) {

            Vector to_return = (Vector)Activator.CreateInstance(self.GetType());

            for (int i=0; i<self.components.Length; i++) {
                to_return.components[i] = (self.components[i] - other.components[i]);
            }

            return to_return;

        }

        public static Vector operator *(Vector self, float multiplier) {

            Vector to_return = (Vector)Activator.CreateInstance(self.GetType());

            for (int i=0; i<self.components.Length; i++) {
                to_return.components[i] = self.components[i]*multiplier;
            }

            return to_return;

        }

        public static Vector operator /(Vector self, float denominator) {

            Vector to_return = (Vector)Activator.CreateInstance(self.GetType());

            for (int i=0; i<self.components.Length; i++) {
                to_return.components[i] = self.components[i]/denominator;
            }

            return to_return;

        }

        public override string ToString() {
            string as_str = "";
            as_str += this.GetType().Name + ": {\n";
            for (int i=0; i<this.components.Length; i++) {
                as_str += "\t" + this.components[i] + ((i == this.components.Length-1) ? "" : ",") + "\n";
            }
            as_str += "}";
            return as_str;
        }

    }

    public class Vector2 : Vector {

        public Vector2() : base(0, 0) {}
        public Vector2(float x, float y) : base(x, y) {}

        public static bool operator ==(Vector2 self, Vector2 other) { return (Vector2)((Vector)self)==((Vector)other); }
        public static bool operator !=(Vector2 self, Vector2 other) { return (Vector2)((Vector)self)!=((Vector)other); }
        public static Vector2 operator +(Vector2 self, Vector2 other) { return (Vector2)((Vector)self)+other; }
        public static Vector2 operator -(Vector2 self, Vector2 other) { return (Vector2)((Vector)self)-other; }
        public static Vector2 operator *(Vector2 self, float other) { return (Vector2)((Vector)self)*other; }
        public static Vector2 operator /(Vector2 self, float other) { return (Vector2)((Vector)self)/other; }

        protected override int component_count => 2;

        public float x {
            get => this['x'];
            set => this['x'] = value;
        }

        public float y {
            get => this['y'];
            set => this['y'] = value;
        }

    }

    public class Vector3 : Vector {

        public Vector3() : base(0, 0, 0) {}
        public Vector3(float x, float y, float z) : base(x, y, z) {}

        public static bool operator ==(Vector3 self, Vector3 other) { return (Vector3)((Vector)self)==((Vector)other); }
        public static bool operator !=(Vector3 self, Vector3 other) { return (Vector3)((Vector)self)!=((Vector)other); }
        public static Vector3 operator +(Vector3 self, Vector3 other) { return (Vector3)((Vector)self)+other; }
        public static Vector3 operator -(Vector3 self, Vector3 other) { return (Vector3)((Vector)self)-other; }
        public static Vector3 operator *(Vector3 self, float other) { return (Vector3)((Vector)self)*other; }
        public static Vector3 operator /(Vector3 self, float other) { return (Vector3)((Vector)self)/other; }

        protected override int component_count => 3;

        public float x {
            get => this['x'];
            set => this['x'] = value;
        }

        public float y {
            get => this['y'];
            set => this['y'] = value;
        }

        public float z {
            get => this['z'];
            set => this['z'] = value;
        }

    }

    public class Vector4 : Vector {

        public Vector4() : base(0, 0, 0, 0) {}
        public Vector4(float x, float y, float z, float w) : base(x, y, z, w) {}

        public static bool operator ==(Vector4 self, Vector4 other) { return (Vector4)((Vector)self)==((Vector)other); }
        public static bool operator !=(Vector4 self, Vector4 other) { return (Vector4)((Vector)self)!=((Vector)other); }
        public static Vector4 operator +(Vector4 self, Vector4 other) { return (Vector4)((Vector)self)+other; }
        public static Vector4 operator -(Vector4 self, Vector4 other) { return (Vector4)((Vector)self)-other; }
        public static Vector4 operator *(Vector4 self, float other) { return (Vector4)((Vector)self)*other; }
        public static Vector4 operator /(Vector4 self, float other) { return (Vector4)((Vector)self)/other; }

        protected override int component_count => 4;

        public float x {
            get => this['x'];
            set => this['x'] = value;
        }

        public float y {
            get => this['y'];
            set => this['y'] = value;
        }

        public float z {
            get => this['z'];
            set => this['z'] = value;
        }

        public float w {
            get => this['w'];
            set => this['w'] = value;
        }

    }

}