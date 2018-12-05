using System;

namespace Program
{
    public static class Spinlock //Day 17
    {
        public static int CurrentPosition = 0;
        public static int CurrentValue = 0;
        public static void Go()
        {
            var node = new LinkedListNode<int>(0);
            node.Next = node;
            do
            {
                CurrentValue++;
                if (CurrentValue % 10000 == 0)
                    Console.Write($"{CurrentValue} ({Math.Round(Convert.ToDouble(CurrentValue)/Convert.ToDouble(50000000), 2)})\r");
                node = node.Traverse(354);
                var nextNode = new LinkedListNode<int>(CurrentValue);
                nextNode.Next = node.Next;
                node.Next = nextNode;
                node = nextNode;
            } while (CurrentValue < 50000000);
            do
            {
                node = node.Next;
            } while (node.Value != 0);
        }

        internal class LinkedListNode<T>
        {
            public LinkedListNode(T value)
            {
                Value = value;
            }

            public LinkedListNode<T> Next { get; set; }
            
            public T Value { get; set; }

            public LinkedListNode<T> Traverse(int steps)
            {
                if (steps == 0) return this;
                return Next.Traverse(steps - 1);
            }
        }
    }
}