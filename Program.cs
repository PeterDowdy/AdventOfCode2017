using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace Program
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Turing.Go();
        }
    }

    public class Turing //Day 25
    {
        private class TapeNode
        {
            public int Value;
            public TapeNode Right;
            public TapeNode Left;
        }
        private static string _state = "A";
        public static void Go()
        {
            var tape = new TapeNode() {Value = 0};
            for (var i = 0; i < 12425180; i++)
            {
                switch (_state + tape.Value)
                {
                    case "A0":
                        tape.Value = 1;
                        if (tape.Right == null)
                        {
                            tape.Right = new TapeNode { Value = 0, Left = tape};
                        }
                        tape = tape.Right;
                        _state = "B";
                        break;
                    case "A1":
                        tape.Value = 0;
                        if (tape.Right == null)
                        {
                            tape.Right = new TapeNode { Value = 0, Left = tape };
                        }
                        tape = tape.Right;
                        _state = "F";
                        break;
                    case "B0":
                        tape.Value = 0;
                        if (tape.Left == null)
                        {
                            tape.Left = new TapeNode { Value = 0, Right = tape };
                        }
                        tape = tape.Left;
                        _state = "B";
                        break;
                    case "B1":
                        tape.Value = 1;
                        if (tape.Left == null)
                        {
                            tape.Left = new TapeNode { Value = 0, Right = tape };
                        }
                        tape = tape.Left;
                        _state = "C";
                        break;
                    case "C0":
                        tape.Value = 1;
                        if (tape.Left == null)
                        {
                            tape.Left = new TapeNode { Value = 0, Right = tape };
                        }
                        tape = tape.Left;
                        _state = "D";
                        break;
                    case "C1":
                        tape.Value = 0;
                        if (tape.Right == null)
                        {
                            tape.Right = new TapeNode { Value = 0, Left = tape };
                        }
                        tape = tape.Right;
                        _state = "C";
                        break;
                    case "D0":
                        tape.Value = 1;
                        if (tape.Left == null)
                        {
                            tape.Left = new TapeNode { Value = 0, Right = tape };
                        }
                        tape = tape.Left;
                        _state = "E";
                        break;
                    case "D1":
                        tape.Value = 1;
                        if (tape.Right == null)
                        {
                            tape.Right = new TapeNode { Value = 0, Left = tape };
                        }
                        tape = tape.Right;
                        _state = "A";
                        break;
                    case "E0":
                        tape.Value = 1;
                        if (tape.Left == null)
                        {
                            tape.Left = new TapeNode { Value = 0, Right = tape };
                        }
                        tape = tape.Left;
                        _state = "F";
                        break;
                    case "E1":
                        tape.Value = 0;
                        if (tape.Left == null)
                        {
                            tape.Left = new TapeNode { Value = 0, Right = tape };
                        }
                        tape = tape.Left;
                        _state = "D";
                        break;
                    case "F0":
                        tape.Value = 1;
                        if (tape.Right == null)
                        {
                            tape.Right = new TapeNode { Value = 0, Left = tape };
                        }
                        tape = tape.Right;
                        _state = "A";
                        break;
                    case "F1":
                        tape.Value = 0;
                        if (tape.Left == null)
                        {
                            tape.Left = new TapeNode { Value = 0, Right = tape };
                        }
                        tape = tape.Left;
                        _state = "E";
                        break;
                }
            }

            while (tape.Left != null)
            {
                tape = tape.Left;
            }
            var oneCount = tape.Value;

            while (tape.Right != null)
            {
                tape = tape.Right;
                oneCount += tape.Value;
            }
        }
    }
}