using System;

namespace Engine.Graphics {

    public sealed class Camera {

        public Vector3 position {
            get;
            set;
        }
        public Matrix3x3 rotation {
            get;
            set;
        }

        private float _fov;
        public float fov {
            get => _fov;
            set {

            }
        }

        private float _surface_dist;
        public float surface_dist {
            get => _surface_dist;
            set {
                _surface_dist = value;
            }
        }

        public Vector3 surface_pos_rotated {
            get => new Vector3(0, 0, _surface_dist)*rotation;
        }

        public Vector3 surface_pos_world {
            get => position + surface_pos_rotated;
        }

    }

}