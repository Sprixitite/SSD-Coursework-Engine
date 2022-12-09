using System;

namespace Engine.Graphics {

    public struct Transform {

        // This is formatted awfully because structs don't support properties!
        // I wanted the additional optimisation of using a struct so this is how it's gotta be

        // Position
        private Vector3 position;
        public Vector3 get_position() => position;
        public void set_position(Vector3 new_position) => position = new_position;

        // Rotation (matrix)
        private Matrix3x3 rotation_matrix;
        public Matrix3x3 get_rotation_matrix() => rotation_matrix;

        // Rotation (euler)
        public Vector3 get_rotation() => rotation_matrix.euler;
        public void set_rotation(Vector3 new_rotation) => rotation_matrix = new Matrix3x3(new_rotation);

        // Scale
        private Vector3 scale;
        public Vector3 get_scale() => scale;
        public void set_scale(Vector3 new_scale) => scale = new_scale;

    }

}