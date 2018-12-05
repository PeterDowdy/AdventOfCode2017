using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Program
{
    public class SporificaVirus //Day 22
    {
        private static void Print()
        {
            return;
            Thread.Sleep(100);
            Console.SetCursorPosition(0,0);
            var minX = Nodes.Keys.Select(k => int.Parse(k.Split(',')[0])).Min();
            var maxX = Nodes.Keys.Select(k => int.Parse(k.Split(',')[0])).Max();
            var minY = Nodes.Keys.Select(k => int.Parse(k.Split(',')[1])).Min();
            var maxY = Nodes.Keys.Select(k => int.Parse(k.Split(',')[1])).Max();
            for (var y = maxY; y >= minY; y--)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    if (!Nodes.ContainsKey($"{x},{y}")) Console.Write(' ');
                    else if (Nodes[$"{x},{y}"] == CurrentNode)
                    {
                        switch (VirusDirection)
                        {
                            case Direction.North:
                                Console.Write("^");
                                break;
                            case Direction.East:
                                Console.Write(">");
                                break;
                            case Direction.South:
                                Console.Write("V");
                                break;
                            case Direction.West:
                                Console.Write("<");
                                break;
                        }
                    }
                    else
                    {
                        switch (Nodes[$"{x},{y}"].Infected)
                        {
                            case Infection.Clean:
                                Console.Write('.');
                                break;
                            case Infection.Flagged:
                                Console.Write('F');
                                break;
                            case Infection.Weakened:
                                Console.Write('W');
                                break;
                            case Infection.Infected:
                                Console.Write("#");
                                break;
                        }
                        
                    }
                }
                Console.WriteLine();
            }
        }
        public static Direction VirusDirection = Direction.North;
        public static Dictionary<string,GridNode> Nodes = new Dictionary<string, GridNode>();
        public static void Go()
        {
            Print();
            var infections = 0;
            for (var i = 0; i < 10000000; i++)
            {
                Print();
                switch (CurrentNode.Infected)
                {
                    case Infection.Clean:
                        CurrentNode.Infected = Infection.Weakened;
                        switch (VirusDirection)
                        {
                            case Direction.North:
                                VirusDirection = Direction.West;
                                break;
                            case Direction.West:
                                VirusDirection = Direction.South;
                                break;
                            case Direction.South:
                                VirusDirection = Direction.East;
                                break;
                            case Direction.East:
                                VirusDirection = Direction.North;
                                break;
                        }
                        break;
                    case Infection.Flagged:
                        CurrentNode.Infected = Infection.Clean;
                        switch (VirusDirection)
                        {
                            case Direction.North:
                                VirusDirection = Direction.South;
                                break;
                            case Direction.West:
                                VirusDirection = Direction.East;
                                break;
                            case Direction.South:
                                VirusDirection = Direction.North;
                                break;
                            case Direction.East:
                                VirusDirection = Direction.West;
                                break;
                        }
                        break;
                    case Infection.Weakened:
                        CurrentNode.Infected = Infection.Infected;
                        infections++;
                        break;
                    case Infection.Infected:
                        CurrentNode.Infected = Infection.Flagged;
                        switch (VirusDirection)
                        {
                            case Direction.North:
                                VirusDirection = Direction.East;
                                break;
                            case Direction.East:
                                VirusDirection = Direction.South;
                                break;
                            case Direction.South:
                                VirusDirection = Direction.West;
                                break;
                            case Direction.West:
                                VirusDirection = Direction.North;
                                break;
                        }
                        break;
                }
                switch (VirusDirection)
                {
                    case Direction.North:
                        if (!Nodes.ContainsKey($"{CurrentNode.X},{CurrentNode.Y+1}"))
                        {
                            Nodes[$"{CurrentNode.X},{CurrentNode.Y + 1}"] = new GridNode('.', CurrentNode.X, CurrentNode.Y+1);
                        }
                        CurrentNode = Nodes[$"{CurrentNode.X},{CurrentNode.Y+1}"];
                        break;
                    case Direction.East:
                        if (!Nodes.ContainsKey($"{CurrentNode.X+1},{CurrentNode.Y}"))
                        {
                            Nodes[$"{CurrentNode.X+1},{CurrentNode.Y}"] = new GridNode('.', CurrentNode.X+1, CurrentNode.Y);
                        }
                        CurrentNode = Nodes[$"{CurrentNode.X + 1},{CurrentNode.Y}"];
                        break;
                    case Direction.South:
                        if (!Nodes.ContainsKey($"{CurrentNode.X},{CurrentNode.Y-1}"))
                        {
                            Nodes[$"{CurrentNode.X},{CurrentNode.Y - 1}"] = new GridNode('.', CurrentNode.X, CurrentNode.Y - 1);
                        }
                        CurrentNode = Nodes[$"{CurrentNode.X},{CurrentNode.Y-1}"];
                        break;
                    case Direction.West:
                        if (!Nodes.ContainsKey($"{CurrentNode.X-1},{CurrentNode.Y}"))
                        {
                            Nodes[$"{CurrentNode.X-1},{CurrentNode.Y}"] = new GridNode('.', CurrentNode.X-1, CurrentNode.Y);
                        }
                        CurrentNode = Nodes[$"{CurrentNode.X - 1},{CurrentNode.Y}"];
                        break;
                }
            }
        }

        public enum Infection
        {
            Clean,
            Weakened,
            Infected,
            Flagged
        }
        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        public class GridNode
        {
            public int X;
            public int Y;
            public Infection Infected;

            public GridNode(char start, int x, int y)
            {
                X = x;
                Y = y;
                Nodes[$"{x},{y}"] = this;
                Infected = start == '#' ? Infection.Infected : Infection.Clean;
            }
        }
        static SporificaVirus()
        {
            for (var y = Start.Length-1; y >= 0; y--)
            {
                for (var x = 0; x < Start.Length; x++)
                {
                    Nodes[$"{x},{y}"] = new GridNode(Start[Start.Length-y-1][x],x,y);
                }
            }
            CurrentNode = Nodes[$"{Start.Length/2},{Start.Length/2}"];
        }

        public static GridNode CurrentNode { get; set; }
        private static char[][] Start = @"
..####.###.##..##....##..
.##..#.###.##.##.###.###.
......#..#.#.....#.....#.
##.###.#.###.##.#.#..###.
#..##...#.....##.#..###.#
.#..#...####...#.....###.
##...######.#.###..#.##..
###..#..##.###....##.....
.#.#####.###.#..#.#.#..#.
#.#.##.#.##..#.##..#....#
..#.#.#.#.#.##...#.####..
##.##..##...#..##..#.####
#.#..####.##.....####.##.
..####..#.#.#.#.##..###.#
..#.#.#.###...#.##..###..
#.####.##..###.#####.##..
.###.##...#.#.#.##....#.#
#...######...#####.###.#.
#.####.#.#..#...##.###...
####.#.....###..###..#.#.
..#.##.####.#######.###..
#.##.##.#.#.....#...#...#
###.#.###..#.#...#...##..
##..###.#..#####.#..##..#
#......####.#.##.#.###.##".Trim().Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(c => c.ToArray()).ToArray();
        //        private static char[][] Start = @"
        //..#
        //#..
        //...".Trim().Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(c => c.ToArray()).ToArray();
    }
}