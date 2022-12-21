using System;

namespace Engine.Graphics {

    public sealed class ColourRGBA : IEquatable<ColourRGBA> {

        public byte r;
        public byte g;
        public byte b;
        public byte a;

        public bool Equals(ColourRGBA other) {
            return (
                other.r == r &&
                other.g == g &&
                other.b == b &&
                other.a == a
            );
        }

        public static implicit operator System.Drawing.Color(ColourRGBA self) {
            return System.Drawing.Color.FromArgb(self.a, self.r, self.g, self.b);
        }

    }

    public sealed class ColourHSVA : IEquatable<ColourHSVA>, IEquatable<ColourRGBA> {

        private float _h;
        public float h {
            get => _h;
            set {
                if (value > 1.0f || value < 0.0f) throw new Exception("Attempted to set hue to value outside of the range 0-1");
                _h = value;
            }
        }

        private float _s;
        public float s {
            get => _s;
            set {
                if (value > 1.0f || value < 0.0f) throw new Exception("Attempted to set saturation to value outside of the range 0-1");
                _s = value;
            }
        }

        private float _v;
        public float v {
            get => _v;
            set {
                if (value > 1.0f || value < 0.0f) throw new Exception("Attempted to set value to value outside of the range 0-1");
                _v = value;
            }
        }

        private float _a;
        public float a {
            get => _a;
            set {
                if (value > 1.0f || value < 0.0f) throw new Exception("Attempted to set alpha to value outside of the range 0-1");
                _a = value;
            }
        }

        public static bool operator==(ColourHSVA self, ColourHSVA other) => self.Equals(other);
        public static bool operator!=(ColourHSVA self, ColourHSVA other) => !self.Equals(other);
        public bool Equals(ColourHSVA other) {
            // Unsure if there's any perf cost to accessing properties
            // Accessing underlying values can't be slower though...
            return (
                other._h == _h &&
                other._s == _s &&
                other._v == _v &&
                other._a == _a
            );
        }

        public static bool operator==(ColourHSVA self, ColourRGBA other) => self.Equals(other);
        public static bool operator!=(ColourHSVA self, ColourRGBA other) => !self.Equals(other);
        public bool Equals(ColourRGBA other) {
            ColourRGBA self = this.to_rgba();
            return (
                self.r == other.r &&
                self.g == other.g &&
                self.b == other.b &&
                self.a == other.a
            );
        }

        public ColourRGBA to_rgba() {
            float c = _v * _s;
            float h1 = 6 * _h;
            float x = chroma * ( 1 - Math.Abs( ( h1 % 2 ) - 1 ) );
            switch (Math.Floor(c)) {
                case 0:     return new ColourRGBA(c, x, 0, _a);
                case 1:     return new ColourRGBA(x, c, 0, _a);
                case 2:     return new ColourRGBA(0, c, x, _a);
                case 3:     return new ColourRGBA(0, x, c, _a);
                case 4:     return new ColourRGBA(x, 0, c, _a);
                default:    return new ColourRGBA(c, 0, x, _a);
            }
        }

        public static implicit operator System.Drawing.Color(ColourHSVA self) => self.to_rgba();

    }

}