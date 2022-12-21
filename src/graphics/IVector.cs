using System;

namespace Engine.Graphics {

    public interface IVector<T> : IComparable<T> {

        public override string ToString();
        public bool Equals(T other);
        public float dot();
        public float magnitude();
        public T interp();
        public T midpoint();
        public T unit();
        
    }

}