using Microsoft.VisualStudio.GraphModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;

[DebuggerDisplay("{Value}")]
public class Node<T>
{
    private Color BackgroundColor;
    private GraphNode GraphNode;
    private GraphProperty BackgroundProperty;
    public T Value;
    public Node<T> Left, Right;
    /*************************************************************************/
    public Color Background
    {
        set
        {
            BackgroundColor = value;
            GraphNode[BackgroundProperty] = new SolidColorBrush(value);
        }
        get { return BackgroundColor; }
    }

    public Node(T value)
    {
        this.Value = value;
    }
    /*************************************************************************/
    public Graph ToGraph()
    {
        Graph graph = new Graph();
        GraphPropertyCollection properties = graph.DocumentSchema.Properties;
        GraphProperty background = properties.AddNewProperty("Background", typeof(Brush));

        var nodeToId = new Dictionary<Node<T>, GraphNodeId>();
        var leaves = new Queue<Node<T>>();

        leaves.Enqueue(this);
        GraphNode n = graph.Nodes.GetOrCreate(this.Value.ToString());
        nodeToId.Add(this, n.Id);

        while (leaves.Count > 0)
        {
            var node = leaves.Dequeue();

            GraphNode parent = graph.Nodes.Get(nodeToId[node]);
            parent.Label = node.Value.ToString();
            node.GraphNode = parent;
            node.BackgroundProperty = background;

            string labelLeft = node.Left == null ? "None" : node.Left.Value.ToString();
            GraphNode left = graph.Nodes.CreateNew(labelLeft);
            left.Label = labelLeft;
            GraphLink leftLink = graph.Links.GetOrCreate(parent, left);
            leftLink.Label = "Left";

            string labelRight = node.Right == null ? "None" : node.Right.Value.ToString();
            GraphNode right = graph.Nodes.CreateNew(labelRight);
            right.Label = labelRight;
            GraphLink rightLink = graph.Links.GetOrCreate(parent, right);
            rightLink.Label = "Right";

            if (node.Left != null)
            {
                nodeToId.Add(node.Left, left.Id);
                leaves.Enqueue(node.Left);
            }

            if (node.Right != null)
            {
                nodeToId.Add(node.Right, right.Id);
                leaves.Enqueue(node.Right);
            }
        }
        return graph;
    }
    /***********************************************************************************************************/
    public int GetNodeDistance(T node1, T node2)
    {
        int level1 = -1, level2 = -1, lcaLevel = -1;
        GetNodeDistance(this, node1, node2, 0, ref lcaLevel, ref level1, ref level2);
        if (lcaLevel == -1) return -1;
        return level1 - lcaLevel + level2 - lcaLevel + 1;
    }
    private static void GetNodeDistance(
        Node<T> node, T node1, T node2, int level,
        ref int lcaLevel, ref int level1, ref int level2)
    {
        if (node == null) return;

        int prevL1 = level1, prevL2 = level2;
        if (node.Value.Equals(node1)) level1 = level;
        if (node.Value.Equals(node2)) level2 = level;

        if (level1 == -1 || level2 == -1)
        {
            GetNodeDistance(node.Left, node1, node2, level + 1, ref lcaLevel, ref level1, ref level2);
            GetNodeDistance(node.Right, node1, node2, level + 1, ref lcaLevel, ref level1, ref level2);
        }
        if (prevL1 != level1 && prevL2 != level2 && lcaLevel == -1) lcaLevel = level;
        if ((prevL1 != level1 || prevL2 != level2) && level >= lcaLevel) node.Background = Colors.Aqua;
    }
    /***********************************************************************************************************/
    public static void LinkLevels(Node<T> root, Graph g)
    {
        Node<T> marker = new Node<T>(default(T));
        Queue<Node<T>> q = new Queue<Node<T>>();
        q.Enqueue(root);

        while (q.Count > 0)
        {
            Node<T> prev = null;
            int size = q.Count;
            for (int i = 0; i < size; i++)
            {
                Node<T> n = q.Dequeue();
                if (n == null) continue;

                q.Enqueue(n.Left);
                q.Enqueue(n.Right);

                if (prev != null)
                    g.Links.GetOrCreate(prev.GraphNode, n.GraphNode);
                prev = n;
            }
        }
    }
    /***********************************************************************************************************/
    public static int SumLeftLeaves(Node<int> n, bool isLeft = false)
    {
        if (n == null) return 0;
        if (isLeft && n.Left == null && n.Right == null)
        {
            n.Background = Colors.DarkTurquoise;
            return n.Value;
        }
        return SumLeftLeaves(n.Left, true) + SumLeftLeaves(n.Right);
    }
    /***********************************************************************************************************/
}
public static class Trees
{
    /***********************************************************************************************************/
    public static Node<T> RandomTree<T>(IEnumerable<T> values)
    {
        int seed = (int)(DateTime.Now.Ticks % int.MaxValue);
        return RandomTree(values, seed);
    }
    public static Node<T> RandomTree<T>(IEnumerable<T> values, int seed)
    {
        var valueList = new List<T>(values);
        var nodeSetters = new List<Action<Node<T>>>();
        var randomizer = new Random(seed);

        Node<T> root = null;
        Action<Node<T>> rootSetter = r => { root = r; };
        nodeSetters.Add(rootSetter);

        while (valueList.Count > 0)
        {
            int valueIndex = randomizer.Next(0, valueList.Count - 1);
            var value = valueList[valueIndex];
            valueList.RemoveAt(valueIndex);

            int nodeIndex = randomizer.Next(0, nodeSetters.Count - 1);
            var node = new Node<T>(value);
            nodeSetters[nodeIndex](node);
            nodeSetters.RemoveAt(nodeIndex);

            Action<Node<T>> leftSetter = left => { node.Left = left; };
            Action<Node<T>> rightSetter = right => { node.Right = right; };

            nodeSetters.Add(leftSetter);
            nodeSetters.Add(rightSetter);
        }
        return root;
    }
    /*************************************************************************/
    // 2^n complexity? :(
    public static void ReplaceNodeValueWithSubtreeSum(Node<int> tree)
    {
        if (tree == null) return;
        tree.Value = SumLess(tree, tree.Value);
        ReplaceNodeValueWithSubtreeSum(tree.Left);
        ReplaceNodeValueWithSubtreeSum(tree.Right);
    }
    public static int SumLess(Node<int> tree, int value)
    {
        if (tree == null) return 0;
        int s = tree.Value < value ? tree.Value : 0;
        return s + SumLess(tree.Left, value) + SumLess(tree.Right, value);
    }
    /*************************************************************************/
    public static void InOrderIterative(Node<int> n)
    {
        Stack<Node<int>> s = new Stack<Node<int>>();
        PushLeft(s, n);
        while (InOrderStack.Count > 0)
        {
            Node<int> l = InOrderStack.Pop();
            System.Console.WriteLine(l.Value);
            PushLeft(s, l.Right);
        }
    }
    private static readonly Stack<Node<int>> InOrderStack = new Stack<Node<int>>();
    public static void InOrderInit(Node<int> n)
    {
        PushLeft(InOrderStack, n);
    }
    public static Node<int> InOrderNextNode()
    {
        if (InOrderStack.Count == 0) return null;
        Node<int> n = InOrderStack.Pop();
        PushLeft(InOrderStack, n.Right);
        return n;
    }
    private static void PushLeft(Stack<Node<int>> s, Node<int> n)
    {
        while (n != null)
        {
            InOrderStack.Push(n);
            n = n.Left;
        }
    }
    /*************************************************************************/
    public static void PreOrderIterative(Node<int> n)
    {
        var s = new Stack<Node<int>>();
        s.Push(n);
        while (s.Count > 0)
        {
            var l = s.Pop();
            System.Console.WriteLine(l.Value);
            if (l.Right != null) s.Push(l.Right);
            if (l.Left != null) s.Push(l.Left);
        }
    }
    /*************************************************************************/
    public static void PostOrderIterative(Node<int> n)
    {
        Stack<Node<int>> s = new Stack<Node<int>>();
        PostOrderPush(s, n);
        while (s.Count > 0)
        {
            Node<int> p = s.Pop();
            Node<int> c = s.Count > 0 ? s.Peek() : null;
            if (c != null && (c == p.Left || c == p.Right))
            {
                s.Pop();
                s.Push(p);
                PostOrderPush(s, c);
            }
            else
            {
                System.Console.WriteLine(p.Value);
            }
        }
    }
    private static void PostOrderPush(Stack<Node<int>> s, Node<int> n)
    {
        if (n.Right != null) s.Push(n.Right);
        if (n.Left != null) s.Push(n.Left);
        s.Push(n);
    }
    /*************************************************************************/
    // segment start/end, query start/end
    public static int[] MakeSegmentTree(int[] a)
    {
        int height = (int)Math.Ceiling(Math.Log(a.Length, 2));
        int nodes = (int)Math.Pow(2, height + 1) - 1;
        int[] st = new int[nodes];
        MakeSegmentTree(a, st, 0, a.Length - 1, 0);
        return st;
    }
    private static int MakeSegmentTree(int[] a, int[] st, int ss, int se, int node)
    {
        int mid = ss + (se - ss) / 2;
        st[node] = ss == se ? a[ss] :
                   MakeSegmentTree(a, st, ss, mid, node * 2 + 1) +
                   MakeSegmentTree(a, st, mid + 1, se, node * 2 + 2);
       return st[node];
    }
    public static int SegmentTreeSum(int[] st, int se, int qs, int qe)
    {
        return SegmentTreeSum(st, 0, se, qs, qe, 0);
    }
    private static int SegmentTreeSum(int[] st, int ss, int se, int qs, int qe, int node)
    {
        if (qs <= ss && qe >= se) return st[node];
        if (se < qs || ss > qe) return 0;
        int mid = ss + (se - ss) / 2;
        return SegmentTreeSum(st, ss, mid, qs, qe, 2 * node + 1) +
               SegmentTreeSum(st, mid + 1, se, qs, qe, 2 * node + 2);
    }
    // s/e: start/end; n: node
    public static void Update(int[] input, int[] st, int index, int value)
    {
        if (index < 0 || index > input.Length - 1) return;
        input[index] = value;
        int diff = value - input[index];
        Update(st, 0, input.Length - 1, index, diff, 0);
    }
    private static void Update(int[] st, int s, int e, int index, int diff, int node)
    {
        if (index < s || index > e) return;
        st[node] = st[node] + diff;
        if (e != s)
        {
            int m = s + (e - s) / 2;
            Update(st, s, m, index, diff, 2 * node + 1);
            Update(st, m + 1, e, index, diff, 2 * node + 2);
        }
    }
    /*************************************************************************/
}
