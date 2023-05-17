using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeForPresentations
{
    public class SegmentTree<T> : ISegmentTree<T>
    {
        private SegmentTreeNode<T> _root;
        private Func<T, T, T> _associativeOperation;
        private T _neutralElement;
        public int Count { get; private set; }
        public SegmentTree(T[] array, Func<T, T, T> associativeOperation, T neutralElement)
        {
            _associativeOperation = associativeOperation;
            _neutralElement = neutralElement;
            _root = new(neutralElement);
            Build(array); 
        }

        public void Build(T[] array)
        {
            var index = 0;
            Build(array, ref index, 1, _root);
        }

        public void Update(int index, T value)
        {
            if (0 <= index && index < Count) Update(index, value, 0, Count, _root);
            else throw new IndexOutOfRangeException();
        }

        public T Request(int leftIndex, int rightIndex) // leftIndex <= k[i] < rightIndex
        {
            if (0 <= leftIndex && rightIndex < Count) return Request(leftIndex, rightIndex, 0, Count, _root);
            else throw new IndexOutOfRangeException();
        }

        private void Build(T[] array, ref int currentIndex, int currentHeigth, SegmentTreeNode<T> currentNode)
        {
            if(currentHeigth < array.Length)
            {
                var rigthChild = new SegmentTreeNode<T>(_neutralElement) { Parent = currentNode};
                var leftChild = new SegmentTreeNode<T>(_neutralElement) { Parent = currentNode };
                currentNode.Right = rigthChild;
                currentNode.Left = leftChild;
                Build(array, ref currentIndex, currentHeigth * 2, currentNode.Left);
                Build(array, ref currentIndex, currentHeigth * 2, currentNode.Right);
                currentNode.Value = _associativeOperation(currentNode.Left.Value, currentNode.Right.Value);
            }
            else
            {
                if(currentIndex == array.Length)
                {
                    currentNode.Value = _neutralElement;
                }
                else
                {
                    currentNode.Value = array[currentIndex];
                    currentIndex += 1;
                }
                Count++;
            }
        }

        private void Update(int index, T value, int leftIndex, int rightIndex, SegmentTreeNode<T> currentNode)
        {
            if (leftIndex == rightIndex - 1)
            {
                currentNode.Value = value;
                return;
            }
            var middleIndex = (rightIndex - leftIndex) / 2 + leftIndex;
            if (leftIndex <= index && index < middleIndex)
            {
                Update(index, value, leftIndex, middleIndex, currentNode.Left);
                currentNode.Value = _associativeOperation(currentNode.Left!.Value, currentNode.Right!.Value);
            }
            else
            {
                Update(index, value, middleIndex, rightIndex, currentNode.Right);
                currentNode.Value = _associativeOperation(currentNode.Left!.Value, currentNode.Right!.Value);
            }
        }

        private T Request(int leftIndex, int rightIndex, int currentLeftIndex, int currentRightIndex, SegmentTreeNode<T> currentNode)
        {
            if(leftIndex <= currentLeftIndex && currentRightIndex <= rightIndex)
            {
                return currentNode.Value;
            }
            else if (currentLeftIndex > rightIndex - 1 || currentRightIndex - 1 < leftIndex)
            {
                return _neutralElement;
            }
            else
            {
                var middleIndex = (currentRightIndex - currentLeftIndex) / 2 + currentLeftIndex;
                return _associativeOperation(Request(leftIndex,rightIndex,currentLeftIndex, middleIndex, currentNode.Left),
                    Request(leftIndex, rightIndex, middleIndex, currentRightIndex, currentNode.Right));
            }
        }
    }
}
