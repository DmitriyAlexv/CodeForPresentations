using CodeForPresentations;

internal class Program
{
    private static void Main(string[] args)
    {
        var array = new int[] { 7, 3, 2, 4, 5, 6, 1 };
        var segmentTree = new SegmentTree<int>(array, (int a, int b) => { return Math.Min(a,b); }, int.MaxValue);
        var count = segmentTree.Count;
        segmentTree.Update(6, 4);
        segmentTree.Update(0, 1);
        segmentTree.Update(2, 10);
        segmentTree.Update(4, 8);
        var result = segmentTree.Request(0, 7);
        result = segmentTree.Request(0, 6);
        result = segmentTree.Request(2, 3);
        result = segmentTree.Request(1, 4);
        result = segmentTree.Request(2, 5);
        result = segmentTree.Request(3, 4);
        result = segmentTree.Request(1, 1);
        result = segmentTree.Request(1, 3);
        result = segmentTree.Request(4, 6);
    }
}