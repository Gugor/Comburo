using System;
using System.Collections;

namespace Comburo.Tools
{
    [System.Serializable]
    public class Weighted<T> : IComparable<Weighted<T>>
    {
        private T type;
        private int weight;

        public Weighted(T _type, int _weight)
        {
            this.type = _type;
            this.weight = _weight;
        }

        
        public int Weight { get => weight; set => weight = value; }
        public T Type { get => type;}

        int IComparable<Weighted<T>>.CompareTo(Weighted<T> _other)
        {
            Weighted<T> other = (Weighted<T>)_other;
            if (this.weight < other.weight)
            {
                return 1;
            }
            if (this.weight > other.weight)
            {
                return -1;
            }

            return 0;
        }

        public override string ToString()
        {
            
            return String.Format("[0] weights [1]", type,weight);
        }

    }
}