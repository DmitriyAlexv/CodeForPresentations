using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForPresentations
{
    public interface ISegmentTree<T>
    {
        void Build(T[] array);
        void Update(int index, T value);
        T Request(int leftIndex, int rightIndex);
    }
}
