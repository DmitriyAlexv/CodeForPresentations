using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForPresentations
{
    public class SegmentTreeNode<T>
    {
        public T Value { get; set; }
        public SegmentTreeNode<T>? Parent { get; set; }
        public SegmentTreeNode<T>? Right { get; set; }
        public SegmentTreeNode<T>? Left { get; set; }
        public SegmentTreeNode(T value)
        {
            Value = value;
        }
    }
}
