using System;

namespace Engine.Graphics {

    public class Tri<T> 
        where T : Vector
    {

        public Tri(T _p1, T _p2, T _p3) {
            p1 = _p1;
            p2 = _p2;
            p3 = _p3;
        }

        public bool contains(T p) {

            // From the point p1
            // Get a line to the midpoint of the opposing edge constructed by p2 and p3
            Vector angle_bisect = p2.midpoint(p3) - p1;
            Vector to_point = p - p1;

            // Get the vector from p1 to p2, and it's dot product with the vector from p1 to p3
            // This is the angle at p1
            // We divide by 2 because the angle can be different both positive and negative
            float a_len = (p2 - p1).magnitude();
            float b_len = (p3 - p1).magnitude();
            //float angle_thresh = MathF.Acos( (  ) / ( 2 * a_len * b_len ) )
            float angle_thresh = (p2 - p1).dot( ( p3 - p1 ) ) / 2.0f;
            float angle_between = angle_bisect.dot(to_point);

            Console.WriteLine((p2-p1).ToString());
            Console.WriteLine((p3-p1).ToString());
            Console.WriteLine(p.ToString());
            Console.WriteLine(angle_thresh);
            Console.WriteLine(angle_between);
            Console.ReadKey();

            if (!(angle_between <= angle_thresh)) {
                // Point is not within triangle
                return false;
            }

            // From the point p2
            // Get a line to the midpoint of the opposing edge constructed by p1 and p3
            // We need to do this twice to ensure the point is within the bounds
            // If we run the check once, any points outside of the triangle but within the angle of p1 get falsely detected
            angle_bisect = p1.midpoint(p3) - p2;
            to_point = p - p2;

            angle_thresh = (p1 - p2).dot( (p3 - p2) ) / 2.0f;
            angle_between = angle_bisect.dot(to_point);

            return (angle_between <= angle_thresh);

        }

        T p1;
        T p2;
        T p3;

    }

}