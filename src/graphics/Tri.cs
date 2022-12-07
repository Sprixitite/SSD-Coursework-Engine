using System;

namespace Engine.Graphics {

    public class Tri3D {

        public Tri3D(Vector3 _p1, Vector3 _p2, Vector3 _p3) { p1=_p1; p2=_p2; p3=_p3; }

        public Tri2D project_onto(Camera c) {

            return new Tri2D(
                project_point_onto(c, p1),
                project_point_onto(c, p2),
                project_point_onto(c, p3)
            );

        }

        private Vector2 project_point_onto(Camera c, Vector3 p) {

            Vector3 relative_to_cam = c.rotation*(p-c.position);
            Console.WriteLine("Vector relative to camera: " + relative_to_cam);
            Vector3 cam_surface = new Vector3(0, 0, c.surface_dist);
            return new Vector2(

                (cam_surface.z/relative_to_cam.z)*relative_to_cam.x,
                (cam_surface.z/relative_to_cam.z)*relative_to_cam.y

            );

        }

        public Vector3 p1 {
            get;
            private set;
        }

        public Vector3 p2 {
            get;
            private set;
        }

        public Vector3 p3 {
            get;
            private set;
        }

    }

    public class Tri2D {

        public static implicit operator System.Drawing.PointF[](Tri2D self) {
            System.Drawing.PointF[] points = new System.Drawing.PointF[3];
            points[0] = self.p1;
            points[1] = self.p2;
            points[2] = self.p3;
            return points;
        }

        public Tri2D to_pixels(Vector2 screen_size) {
            return new Tri2D(
                new Vector2(((p1.x+1)/2)*screen_size.x, ((p1.y+1)/2)*screen_size.y),
                new Vector2(((p2.x+1)/2)*screen_size.x, ((p2.y+1)/2)*screen_size.y),
                new Vector2(((p3.x+1)/2)*screen_size.x, ((p3.y+1)/2)*screen_size.y)
            );
        }

        public Tri2D(Vector2 _p1, Vector2 _p2, Vector2 _p3) { p1 = _p1; p2 = _p2; p3 = _p3; }

        public Vector2 p1 {
            get;
            private set;
        }

        public Vector2 p2 {
            get;
            private set;
        }

        public Vector2 p3 {
            get;
            private set;
        }

    }

}