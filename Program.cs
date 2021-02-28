using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms_and_Data_Structures
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }
        public Node(T value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
    //public interface ILinkedList
    //{
    //    int GetCount(); // возвращает количество элементов в списке
    //    void AddNode(int value);  // добавляет новый элемент списка
    //    void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
    //    void RemoveNode(int index); // удаляет элемент по порядковому номеру
    //    void RemoveNode(Node node); // удаляет указанный элемент
    //    Node FindNode(int searchValue); // ищет элемент по его значению
    //}

    class DoublyLinkedList<T> : IEnumerable<T>   //: ILinkedList
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public int count { get; set; }
        public DoublyLinkedList() { } //создание пустого списка

        public DoublyLinkedList(T value) // создание списка с 1 эллементом
        {
            var item = new Node<T>(value);
            Head = item; //первый элемент
            Tail = item; //последний элемент
            count = 1;
        }
        public int GetCount() //возвращает количество элементов +
        {
            return count - 1;
        }
        public void AddNode(T value) //добавляет новый элемент в конец списка +
        {
            var item = new Node<T>(value);

            if (count == 0)
            {
                Head = item; //первый элемент
                Tail = item; //последний элемент
                count = 1;
            }

            Tail.Next = item; //ссылка с последенго элемента на новый элемент
            item.Prev = Tail; //ссылка с нового элемента на старый элемент
            Tail = item;      //последний элемент
            count++;
        }

        public void AddNodeAfter(Node<T> node, T value) //добавляем элемент списка после определённого значения
        {
            var newNode = new Node<T>(value); //ссылка на новый элемент
            var nextItem = node.Next; //запоминаем ссылку на следующей элемент перед которым вставляем новый
            node.Next = newNode; //ставим ссылку на новый элемент
            newNode.Next = nextItem; //новому элементу даём ссылку на следующий перед которым вставили
            count++;
        }

        public void RemoveNodeIndex(int index) //удаляет элемент под данным номером
        {
            var current = Head;
            for (int i = 1; i <= index; i++)
            {
                if (i.Equals(index))
                {
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;
                    count--;
                    return;
                }
                current = current.Next;
            }
        }

        public void RemoveItem(T value) // удаление указанного элемента
        {
            var current = Head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;
                    count--;
                    return;
                }
                current = current.Next;
            }
        }
        // чё т не понял, мне тут нужно вывести элемент не под каким-то номером, а с таким же значением?
        //public Node FindNode(int searchValue)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerator GetEnumerator() // так, ну это вроде возвращает списком все элементы
        {
            var current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() // а, что делает это я вообще хз
        {
            return (IEnumerator<T>)GetEnumerator();
        }
    }

    class Program
    {
        static void Cleaning(string text)
        {
            if (text == "очистить")
            {
                Console.Clear();
            }
        }
        public static int count;

        static int BinSearch(int[] inputArray, int searchValue)
        {
            Array.Sort(inputArray);
            int min = 0;
            int max = inputArray.Length - 1;
            count = 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (inputArray[mid] == searchValue)
                {
                    return mid;
                }
                else if (inputArray[mid] < searchValue)
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid - 1;
                }
                count++;
            }
            return -1;
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            var duplexList = new DoublyLinkedList<int>();
            duplexList.AddNode(1);
            duplexList.AddNode(2);
            duplexList.AddNode(3);
            duplexList.AddNode(3);
            duplexList.AddNode(4);
            duplexList.AddNode(5);

            duplexList.RemoveItem(4);
            duplexList.RemoveNodeIndex(2);

            foreach (var item in duplexList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Количество элементов: " + duplexList.GetCount());

            Console.WriteLine("\nДля очичения консоли напишите: очистить");
            Cleaning(Console.ReadLine());

            int[] arr = { 123, 41, 654, 078, 12, 48, 63636, 6464, 687, 98, 868768, 69, 434, 4343, 2844531, 873438, 483 };
            Console.WriteLine(BinSearch(arr, 69) + $"\nНа {count} ходе нашлось нужное значение");
            Console.WriteLine(BinSearch(arr, 873438) + $"\nНа {count} ходе нашлось нужное значение");
            Console.WriteLine(BinSearch(arr, 999) + "\nИскомого значения нет");
            Console.WriteLine("Асимптотическая сложность О(log(n))");


        }
    }
}