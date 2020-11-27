using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KejUtils
{
    public struct StructTuple<I,J> : IEquatable<StructTuple<I,J>>
    {
        readonly I First;
        readonly J Second;

        public StructTuple(I first, J second)
        {
            First = first;
            Second = second;
        }

        public I A { get { return First; } }
        public J B { get { return Second; } }

        public override int GetHashCode()
        {
            return First.GetHashCode() ^ Second.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StructTuple<I, J>))
            {
                return false;
            }
            return Equals((StructTuple<I, J>)obj);
        }

        public bool Equals(StructTuple<I, J> other)
        {
            return other.First.Equals(First) && other.Second.Equals(Second);
        }
    }
}
