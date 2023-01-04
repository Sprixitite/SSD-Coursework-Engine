using System;

namespace Engine.Graphics {

    public sealed class ColourRGBA : IEquatable<ColourRGBA> {

        public ColourRGBA(byte red, byte green, byte blue, byte alpha) {
            r = red; g = green; b = blue; a = alpha;
        }

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

        public ColourHSVA(float hue, float saturation, float value, float alpha) {
            _h = hue; _s = saturation; _v = value; _a = alpha;
        }

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
            float x = c * ( 1 - Math.Abs( ( h1 % 2 ) - 1 ) );
            switch ((int)Math.Floor(c)) {
                case 0:     return new ColourRGBA((byte)c, (byte)x, 0, (byte)(_a*255));
                case 1:     return new ColourRGBA((byte)x, (byte)c, 0, (byte)(_a*255));
                case 2:     return new ColourRGBA(0, (byte)c, (byte)x, (byte)(_a*255));
                case 3:     return new ColourRGBA(0, (byte)x, (byte)c, (byte)(_a*255));
                case 4:     return new ColourRGBA((byte)x, 0, (byte)c, (byte)(_a*255));
                default:    return new ColourRGBA((byte)c, 0, (byte)x, (byte)(_a*255));
            }
        }

        public static implicit operator System.Drawing.Color(ColourHSVA self) => self.to_rgba();

    }

}