using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace Algorithms_and_Data_Structures
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Green;

            //BenchmarkSwitcher.FromAssembly(typeof(BenchmarkHashSet).Assembly).Run(args);

            BTree bt = new BTree();

            Test test = new Test(bt);

            int userAnswer = -1;
            while (userAnswer != 0)
            {
                Console.WriteLine("\n Choose action: ");
                Console.WriteLine("0 - Exit ");
                Console.WriteLine("1 - Add Node By Value ");
                Console.WriteLine("2 - Remove Node By Value ");
                Console.WriteLine("3 - Find Node By Value ");
                Console.WriteLine();
                userAnswer = Convert.ToInt32(Console.ReadLine());

                switch (userAnswer)
                {

                    case 1:
                        Console.Write("Input value for Adding and press Enter: ");
                        bt.AddItem(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine();
                        bt.PrintTree();
                        break;
                    case 2:
                        Console.Write("Input value for Removing and press Enter: ");
                        bt.RemoveItem(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine();
                        bt.PrintTree();
                        break;
                    case 3:
                        Console.Write("Input value for Finding and press Enter: ");
                        bt.Node = bt.GetNodeByValue(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine();
                        Console.Write($"Value: {bt.Node.Value}; Parent: {bt.Node.Parent.Value}; LeftChild: {bt.Node.LeftChild.Value}; RigthChild: {bt.Node.RightChild.Value}");
                        break;
                    case 0:
                    default:
                        break;

                }
                Console.WriteLine();
            }



        }

    }

    #region 1 задание

    public class BenchmarkHashSet
    {
        // Массив строк
        string[] arr = new string[1_000_000];
        HashSet<string> hashSet;
        int test = 1000;

        private readonly Random _generator;
        public BenchmarkHashSet()
        {
            arr = new string[1_000_000];
            hashSet = new HashSet<string>();
            _generator = new Random();

            for (int i = 0; i < 1_000_000; i++)
            {
                arr[i] = RandomWords(ref arr, hashSet);
                hashSet.Add(arr[i]);
            }
        }

        // Сделайте случайные слова.
        public static string RandomWords(ref string[] array, HashSet<string> hashSet)
        {
            if (array == null)
                array = new string[10000];

            if (hashSet == null)
                hashSet = new HashSet<string>();
            else hashSet.Clear();

            // Получаем количество слов и букв за слово.
            Random random = new Random();
            int num_letters = random.Next(0, 25);
            int num_words = random.Next(0, 5);

            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
            string word = "";
            // Делаем слова.
            for (int i = 1; i <= num_words; i++)
            {
                // Сделайте слово.

                for (int j = 1; j <= num_letters; j++)
                {
                    // Выбор случайного числа от 0 до 69
                    // для выбора буквы из массива букв.
                    int letter_num = random.Next(0, letters.Length - 1);

                    // Добавить письмо.
                    word += letters[letter_num];
                }
                word += " ";
            }
            return word;
        }

        public bool SearchInArray(string value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
        public bool SearchInHashSet(string value) => hashSet.Contains(value);



        [Benchmark(Description = "поиск по значению")]
        public void TestsearchInArray()
        {
            for (int i = 0; i < test; i++)
            {
                int index = _generator.Next(0, arr.Length);
                SearchInArray(arr[index]);
            }
        }


        [Benchmark(Description = "поиск по хэшу")]
        public void TestSearchInHashSet()
        {
            for (int i = 0; i < test; i++)
            {
                int index = _generator.Next(0, arr.Length);
                SearchInHashSet(arr[index]);
            }
        }

        /*
        * BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
        Intel Core i5-7400 CPU 3.00GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
        .NET Core SDK=5.0.103
        [Host]     : .NET Core 3.1.12 (CoreCLR 4.700.21.6504, CoreFX 4.700.21.6905), X64 RyuJIT
        DefaultJob : .NET Core 3.1.12 (CoreCLR 4.700.21.6504, CoreFX 4.700.21.6905), X64 RyuJIT


        |              Method |           Mean |        Error |       StdDev |
        |-------------------- |---------------:|-------------:|-------------:|
        | 'поиск по значению' | 2,059,626.3 us | 38,796.09 us | 64,819.54 us |
        |     'поиск по хэшу' |       204.3 us |      1.18 us |      1.10 us |
        */
    }

    #endregion

    #region 2 задание

    class BTree : ITree<int>
    {

        public TreeNode<int> Node { get; set; }
        TreeNode<int> GetFreeNode(int value, TreeNode<int> parent)
        {
            return new TreeNode<int>
            {
                Parent = parent,
                Value = value
            };
        }

        public TreeNode<int> GetRoot()
        {
            if (Node != null)
            {
                while (Node.Parent != null)
                {
                    Node = Node.Parent;
                }
                return Node;
            }
            else
            {
                return null;
            }

        }
        // добавить узел
        public void AddItem(int value)
        {
            TreeNode<int> tmp = null;
            TreeNode<int> head = GetRoot();
            if (head == null)
            {
                head = GetFreeNode(value, null);
                this.Node = head;
                return;
            }
            tmp = head;
            while (tmp != null)
            {
                if (value > tmp.Value)
                {
                    if (tmp.RightChild != null)
                    {
                        tmp = tmp.RightChild;
                        continue;
                    }
                    else
                    {
                        tmp.RightChild = GetFreeNode(value, tmp);
                        return;
                    }
                }
                else if (value < tmp.Value)
                {
                    if (tmp.LeftChild != null)
                    {
                        tmp = tmp.LeftChild;
                        continue;
                    }
                    else
                    {
                        tmp.LeftChild = GetFreeNode(value, tmp);
                        return;
                    }
                }
                else
                {
                    throw new Exception("Wrong tree state");                  // Дерево построено неправильно
                }
            }
            return;

        }
        // удалить узел по значению
        public void RemoveItem(int value)
        {
            TreeNode<int> head = GetNodeByValue(value);
            if (head == null)
            {
                return;
            }

            var currentNodeSide = head.NodeSide;

            if (head.LeftChild == null && head.RightChild == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    head.Parent.LeftChild = null;
                }
                else
                {
                    head.Parent.RightChild = null;
                }
            }

            else if (head.LeftChild == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    head.Parent.LeftChild = head.RightChild;
                }
                else
                {
                    head.Parent.RightChild = head.RightChild;
                }

                head.RightChild.Parent = head.Parent;
            }

            else if (head.RightChild == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    head.Parent.LeftChild = head.LeftChild;
                }
                else
                {
                    head.Parent.RightChild = head.LeftChild;
                }

                head.LeftChild.Parent = head.Parent;
            }

            else
            {
                TreeNode<int> curTree;
                if (head.RightChild != null && head.LeftChild != null)
                {
                    curTree = head.RightChild;

                    while (curTree.LeftChild != null)
                    {
                        curTree = curTree.LeftChild;
                    }

                    //Если самый левый элемент является первым потомком
                    if (curTree.Parent == head)
                    {
                        curTree.LeftChild = head.LeftChild;
                        head.LeftChild.Parent = curTree;
                        curTree.Parent = head.Parent;
                        if (head == head.Parent.LeftChild)
                        {
                            head.Parent.LeftChild = curTree;
                        }
                        else if (head == head.Parent.RightChild)
                        {
                            head.Parent.RightChild = curTree;
                        }
                        return;
                    }
                    //Если самый левый элемент НЕ является первым потомком
                    else
                    {
                        if (curTree.RightChild != null)
                        {
                            curTree.RightChild.Parent = curTree.Parent;
                        }
                        curTree.Parent.LeftChild = curTree.RightChild;
                        curTree.RightChild = head.RightChild;
                        curTree.LeftChild = head.LeftChild;
                        head.LeftChild.Parent = curTree;
                        head.RightChild.Parent = curTree;
                        curTree.Parent = head.Parent;
                        if (head == head.Parent.LeftChild)
                        {
                            head.Parent.LeftChild = curTree;
                        }
                        else if (head == head.Parent.RightChild)
                        {
                            head.Parent.RightChild = curTree;

                        }
                    }
                }
            }
        }
        //получить узел дерева по значению
        public TreeNode<int> GetNodeByValue(int value)
        {
            TreeNode<int> head = GetRoot();
            while (head != null)
            {
                if (head.Value == value)
                {
                    return head;
                }
                else if (head.Value < value)
                {
                    head = head.RightChild;
                    continue;
                }
                else
                {
                    head = head.LeftChild;
                    continue;
                }
            }
            throw new Exception("Node is not found");                  // Узел не найден
        }
        //вывести дерево в консоль
        public void PrintTree()
        {
            PrintTree(GetRoot());
        }

        private void PrintTree(TreeNode<int> startNode, string indent = "", Side? side = null)
        {
            if (startNode != null)
            {
                var nodeSide = side == null ? "+" : side == Side.Left ? "L" : "R";
                Console.WriteLine($"{indent} [{nodeSide}] - {startNode.Value}");
                indent += new string(' ', 3);
                //рекурсивный вызов для левой и правой веток
                PrintTree(startNode.LeftChild, indent, Side.Left);
                PrintTree(startNode.RightChild, indent, Side.Right);
            }
        }


    }

    public interface ITree<T>
    {
        TreeNode<T> GetRoot();
        void AddItem(T value); // добавить узел
        void RemoveItem(T value); // удалить узел по значению
        TreeNode<T> GetNodeByValue(T value); //получить узел дерева по значению
        void PrintTree(); //вывести дерево в консоль
    }

    class Test
    {

        BTree tree;
        public Test(BTree bt)
        {
            tree = bt;
            GenerateBTree();
        }

        void GenerateBTree()
        {
            byte[] arrValue = { 50, 25, 75, 13, 37, 63, 87, 6, 21, 29, 45, 55, 68, 82, 95, 1, 7, 18, 23, 27, 35, 40, 48, 51, 58, 65, 70, 78, 85, 90, 100 };
            for (byte i = 0; i < arrValue.Length; i++)
            {
                tree.AddItem(arrValue[i]);
            }

            Console.WriteLine("Source tree:");
            tree.PrintTree();
            Console.WriteLine("\nAdded note(value = 84)");
            tree.AddItem(84);
            tree.PrintTree();

            Console.WriteLine("\nRemoved note(value = 37)");
            tree.RemoveItem(37);
            tree.PrintTree();

            Console.WriteLine("\nFound note(value = 95)");
            tree.Node = tree.GetNodeByValue(95);
            Console.Write($"Value: {tree.Node.Value}; Parent: {tree.Node.Parent.Value}; LeftChild: {tree.Node.LeftChild.Value}; RigthChild: {tree.Node.RightChild.Value}");
            Console.WriteLine();


        }
    }

    public enum Side
    {
        Left,
        Right
    }
    public class TreeNode<T>
    {
        public Side? NodeSide =>
        Parent == null
        ? (Side?)null
        : Parent.LeftChild == this
            ? Side.Left
            : Side.Right;
        public T Value { get; set; }
        public TreeNode<T> LeftChild { get; set; }
        public TreeNode<T> RightChild { get; set; }
        public TreeNode<T> Parent { get; set; }

        public override bool Equals(object obj)
        {
            var node = obj as TreeNode<T>;

            if (node == null)
                return false;

            return node.Value.Equals(Value);
        }
    }
}

#endregion