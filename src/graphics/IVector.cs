using System;

namespace Engine.Graphics {

    public interface IVector<T> : IEquatable<T> {

        string ToString();
        float dot(T other);
        float magnitude();
        T interp(T other, float interp_amt);
        T midpoint(T other);
        T unit();
        
    }

}