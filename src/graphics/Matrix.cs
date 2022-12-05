using System;

namespace Engine.Graphics {

    public sealed class Matrix3x3 {

        public Vector3 row_x {
            get => new Vector3(components[0], components[1], components[2]);
        }

        public Vector3 row_y {
            get => new Vector3(components[3], components[4], components[5]);
        }

        public Vector3 row_z {
            get => new Vector3(components[6], components[7], components[8]);
        }

        public Vector3 col_x {
            get => new Vector3(components[0], components[3], components[6]);
        }

        public Vector3 col_y {
            get => new Vector3(components[1], components[4], components[7]);
        }

        public Vector3 col_z {
            get => new Vector3(components[2], components[5], components[8]);
        }

        public Vector3 euler {
            get;
            private set;
        }

        float[] components = new float[9];

        public static Vector3 operator *(Matrix3x3 self, Vector3 other) {
            Vector3 col_x = self.col_x*other.x;
            Vector3 col_y = self.col_y*other.y;
            Vector3 col_z = self.col_z*other.z;
            return new Vector3(
                col_x.x + col_y.x + col_z.x,
                col_x.y + col_y.y + col_z.y,
                col_x.z + col_y.z + col_z.z
            );
        }

        // C# no boilerplate challenge (impossible)
        public static Vector3 operator *(Vector3 other, Matrix3x3 self) {
            Vector3 col_x = self.col_x*other.x;
            Vector3 col_y = self.col_y*other.y;
            Vector3 col_z = self.col_z*other.z;
            return new Vector3(
                col_x.x + col_y.x + col_z.x,
                col_x.y + col_y.y + col_z.y,
                col_x.z + col_y.z + col_z.z
            );
        }

        public static Matrix3x3 operator *(Matrix3x3 self, Matrix3x3 other) {

            Matrix3x3 result = new Matrix3x3();

            result.euler = self.euler+other.euler;

            // X Column
            result.components[0] = self.row_x.dot(other.col_x);
            result.components[3] = self.row_y.dot(other.col_x);
            result.components[6] = self.row_z.dot(other.col_x);

            // Y Column
            result.components[1] = self.row_x.dot(other.col_y);
            result.components[4] = self.row_y.dot(other.col_y);
            result.components[7] = self.row_z.dot(other.col_y);

            // Z Column
            result.components[2] = self.row_x.dot(other.col_z);
            result.components[5] = self.row_y.dot(other.col_z);
            result.components[8] = self.row_z.dot(other.col_z);

            return result;

        }

        public Matrix3x3() {
            components = new float[9];
        }

        public Matrix3x3(Vector3 _euler) {

            euler = _euler;

            // X Column
            components[0] = MathF.Cos(euler.y)*MathF.Cos(euler.z);
            components[3] = MathF.Cos(euler.y)*MathF.Sin(euler.z);
            components[6] = -MathF.Sin(euler.y);

            // Y Column
            components[1] = MathF.Sin(euler.x)*MathF.Sin(euler.y)*MathF.Cos(euler.z) - MathF.Cos(euler.x)*MathF.Sin(euler.z);
            components[4] = MathF.Sin(euler.x)*MathF.Sin(euler.y)*MathF.Sin(euler.z) + MathF.Cos(euler.x)*MathF.Cos(euler.z);
            components[7] = MathF.Sin(euler.x)*MathF.Cos(euler.y);

            // Z Column
            components[2] = MathF.Cos(euler.x)*MathF.Sin(euler.y)*MathF.Cos(euler.z) + MathF.Sin(euler.x)*MathF.Sin(euler.z);
            components[5] = MathF.Cos(euler.x)*MathF.Sin(euler.y)*MathF.Sin(euler.z) - MathF.Sin(euler.x)*MathF.Cos(euler.z);
            components[8] = MathF.Cos(euler.x)*MathF.Cos(euler.y);

        }

    }

}