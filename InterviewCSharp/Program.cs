public class Program
{
    public static void Main()
    {
        //List<string> input;
        //IList<string> result;
        //input = new List<string>() { "abc", "def", "", "a" };
        //result = InnovationsIQPuzzle.getLongestString(1, input);
        //result = InnovationsIQPuzzle.getLongestString(2, input);
        //result = InnovationsIQPuzzle.getLongestString(3, input);
        //result = InnovationsIQPuzzle.getLongestString(4, input);

        //var r = InnovationsIQPuzzle.findMissing(new int[] { 5, 6, 7, 9, 10 });

        //InnovationsIQPuzzle.permuteString("abcde");

        int[] a = { 1, 3, 5, 7, 9, 11 };
        int[] st = Trees.MakeSegmentTree(a);
        int sum = Trees.SegmentTreeSum(st, a.Length - 1, -100, 100);

        Node<int> tree = Trees.RandomTree(Enumerable.Range(1, 10));
        //tree.ToGraph().Save("BinaryTree.dgml");

        //Trees.PostOrderIterative(tree);
        //Trees.PreOrderIterative(tree);
        //Trees.InOrderIterative(tree);
        //Trees.InOrderInit(tree);
        //Node<int> n;
        //do
        //{
        //    n = Trees.InOrderNextNode();
        //} 
        //while (n != null);

        //tree.ToGraph().Save("BinaryTree.dgml");
        //Trees.ReplaceNodeValueWithSubtreeSum(tree);
        //tree.ToGraph().Save("BinaryTreeChanged.dgml");
    }
}
