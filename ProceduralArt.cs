using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class ProceduralArt //Day 21
    {
        static char[,] CreateSquareArray(char[][] arrays)
        {
            int length = arrays[0].Length;
            char[,] ret = new char[length, length];
            for (int i = 0; i < arrays.Length; i++)
            {
                var array = arrays[i];
                if (array.Length != length)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (int j = 0; j < length; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }
        static char[,] CreateSquareArray(List<List<char[,]>> arrays)
        {
            int length = arrays[0].Sum(c => c.GetLength(0));
            char[,] ret = new char[length, length];
            for (int y = 0; y < arrays.Count; y++)
            {
                for (var x = 0; x < arrays.Count; x++)
                {
                    for (var yy = 0; yy < arrays[y][x].GetLength(0); yy++)
                    {
                        for (var xx = 0; xx < arrays[y][x].GetLength(0); xx++)
                        {
                            ret[y* arrays[y][x].GetLength(0) + yy,x* arrays[y][x].GetLength(0) + xx] = arrays[y][x][yy, xx];
                        }
                    }
                }
            }

            Console.WriteLine("Enhancing");
            PrintMatrix(ret);
            Console.WriteLine();

            return ret;
        }

        private static Dictionary<string, char[,]> BuildEnhancements(string input)
        {
            var enhancements = new Dictionary<string, char[,]>();
            var maps = input.Split(new [] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var map in maps)
            {
                var tokens = map.Split(new[] {" => "}, StringSplitOptions.RemoveEmptyEntries);
                var key = tokens[0];
                var enhancement = tokens[1];
                enhancements[key] = CreateSquareArray(enhancement.Split('/').Select(s => s.ToCharArray()).ToArray());
            }

            return enhancements;
        }
        public static Dictionary<string,char[,]> Enhancements = BuildEnhancements("../.. => #.#/##./..#\r\n#./.. => ###/.##/..#\r\n##/.. => ..#/.#./##.\r\n.#/#. => ###/.##/###\r\n##/#. => ###/#.#/.##\r\n##/## => #.#/..#/.#.\r\n.../.../... => ..../.#../##.#/#.#.\r\n#../.../... => .##./#.../.##./#..#\r\n.#./.../... => ...#/.#.#/###./##.#\r\n##./.../... => #.##/..#./.#.#/..##\r\n#.#/.../... => ..#./.#../.#.#/###.\r\n###/.../... => #.#./.#../.#../....\r\n.#./#../... => ..#./##../.###/###.\r\n##./#../... => ..#./###./#.#./#.#.\r\n..#/#../... => ..##/###./.#.#/#...\r\n#.#/#../... => #.../...#/.#.#/#...\r\n.##/#../... => ###./####/.###/#.##\r\n###/#../... => #.../#.##/#.../.#.#\r\n.../.#./... => .##./#.#./#..#/..#.\r\n#../.#./... => #.../##.#/#.#./.##.\r\n.#./.#./... => ##../.###/####/....\r\n##./.#./... => #.#./..../###./.#.#\r\n#.#/.#./... => ..../..../#.##/.##.\r\n###/.#./... => ####/#.##/.###/#.#.\r\n.#./##./... => ####/#..#/#.##/.##.\r\n##./##./... => .#.#/#.##/####/.###\r\n..#/##./... => .##./...#/.#.#/..#.\r\n#.#/##./... => #..#/...#/.#../.##.\r\n.##/##./... => ##../#..#/##../..##\r\n###/##./... => ..##/..../#.../..##\r\n.../#.#/... => ###./#.../##.#/.#.#\r\n#../#.#/... => ..#./...#/#..#/#.##\r\n.#./#.#/... => ##../..#./##../###.\r\n##./#.#/... => .#.#/#.#./####/.##.\r\n#.#/#.#/... => .##./.##./#.##/#..#\r\n###/#.#/... => #..#/.##./..#./##..\r\n.../###/... => ###./#..#/.###/#.##\r\n#../###/... => #.../#..#/####/##..\r\n.#./###/... => ###./.##./#..#/.###\r\n##./###/... => #..#/##../.##./#.#.\r\n#.#/###/... => ..#./...#/#.../...#\r\n###/###/... => ...#/##../...#/#.##\r\n..#/.../#.. => ##.#/.#.#/.##./###.\r\n#.#/.../#.. => ###./#..#/.#.#/#.##\r\n.##/.../#.. => ...#/.#.#/.###/###.\r\n###/.../#.. => .#../...#/..#./.#..\r\n.##/#../#.. => .#../...#/.##./..#.\r\n###/#../#.. => .###/##.#/#.##/.###\r\n..#/.#./#.. => ##.#/##../##../#...\r\n#.#/.#./#.. => #.../.###/#.#./#...\r\n.##/.#./#.. => ###./#.##/###./####\r\n###/.#./#.. => .#../..##/##.#/##.#\r\n.##/##./#.. => ##.#/##../.##./...#\r\n###/##./#.. => .#.#/.#../####/.##.\r\n#../..#/#.. => ..##/###./...#/##..\r\n.#./..#/#.. => .#../...#/.#../..##\r\n##./..#/#.. => ###./..##/###./.##.\r\n#.#/..#/#.. => ####/.#.#/...#/..##\r\n.##/..#/#.. => #..#/.#../#.##/####\r\n###/..#/#.. => .#../#.##/#.##/.#..\r\n#../#.#/#.. => ..#./#.##/.#../.##.\r\n.#./#.#/#.. => ##../#.../#.#./###.\r\n##./#.#/#.. => #..#/.##./####/.#..\r\n..#/#.#/#.. => ##.#/..#./..#./.#.#\r\n#.#/#.#/#.. => .#../..#./..#./..##\r\n.##/#.#/#.. => ##../#.##/#.#./#.##\r\n###/#.#/#.. => ##.#/..##/##../##.#\r\n#../.##/#.. => .###/####/#.##/..##\r\n.#./.##/#.. => #.#./.##./###./#.##\r\n##./.##/#.. => ..#./#..#/####/...#\r\n#.#/.##/#.. => ####/.#.#/##../##.#\r\n.##/.##/#.. => #.#./#..#/.#.#/.##.\r\n###/.##/#.. => .#../.##./.##./.###\r\n#../###/#.. => #..#/###./##.#/##..\r\n.#./###/#.. => #.#./#..#/..#./#..#\r\n##./###/#.. => ..../##.#/####/...#\r\n..#/###/#.. => ..../#.../##../#..#\r\n#.#/###/#.. => ..#./.#../..../##.#\r\n.##/###/#.. => #..#/###./##.#/.###\r\n###/###/#.. => #.../.##./#.##/.##.\r\n.#./#.#/.#. => ...#/#.../.#../##.#\r\n##./#.#/.#. => .#.#/#.#./.#../#.##\r\n#.#/#.#/.#. => #.##/.##./###./....\r\n###/#.#/.#. => ##../#..#/#.../.###\r\n.#./###/.#. => ###./#.../.#../#..#\r\n##./###/.#. => ##../##../#.../#...\r\n#.#/###/.#. => ##../.#.#/#.##/#.#.\r\n###/###/.#. => #.##/##.#/#.#./#...\r\n#.#/..#/##. => ..../..#./####/..##\r\n###/..#/##. => #.../...#/#.#./#.#.\r\n.##/#.#/##. => ..##/###./.##./#...\r\n###/#.#/##. => .#../###./##.#/...#\r\n#.#/.##/##. => .###/##../.###/..#.\r\n###/.##/##. => .#.#/##.#/.##./.###\r\n.##/###/##. => ..#./.#.#/.#../#..#\r\n###/###/##. => ###./#..#/####/...#\r\n#.#/.../#.# => .#.#/.#../.#.#/#...\r\n###/.../#.# => #..#/##../.#../...#\r\n###/#../#.# => ..../.#../#.../..##\r\n#.#/.#./#.# => #.#./####/.#.#/.##.\r\n###/.#./#.# => ..#./####/#..#/..##\r\n###/##./#.# => .##./.#../#.##/.#.#\r\n#.#/#.#/#.# => ##../..##/##.#/#.#.\r\n###/#.#/#.# => .##./#..#/#..#/.#.#\r\n#.#/###/#.# => ..#./.###/#.##/#.##\r\n###/###/#.# => ###./###./.#.#/###.\r\n###/#.#/### => #.##/..##/#..#/...#\r\n###/###/### => ...#/.#../##.#/.##.");
        public static char[,] Art = {
            {'.', '#', '.'},
            {'.', '.', '#'},
            {'#', '#', '#'}
        };
        public static void Go()
        {
            for (var i = 0; i < 18; i++)
            {
                Console.WriteLine("Starting");
                PrintMatrix(Art);
                Console.WriteLine();
                var arts = new List<List<char[,]>>();
                if (Art.Length % 2 == 0)
                {
                    for (var y = 0; y < Art.GetLength(0); y += 2)
                    {
                        var row = new List<char[,]>();
                        for (var x = 0; x < Art.GetLength(0); x += 2)
                        {
                            var newGrid = new char[,]
                            {
                                {Art[y,x], Art[y + 1,x]},
                                {Art[y,x + 1], Art[y + 1,x + 1]}
                            };
                            row.Add(newGrid);
                        }
                        arts.Add(row);
                    }
                }
                else
                {
                    for (var y = 0; y < Art.GetLength(0); y += 3)
                    {
                        var row = new List<char[,]>();
                        for (var x = 0; x < Art.GetLength(0); x += 3)
                        {
                            var newGrid = new char[,]
                            {
                                {Art[y,x], Art[y + 1,x], Art[y + 2,x]},
                                {Art[y,x + 1], Art[y + 1,x + 1], Art[y+2,x+1]},
                                {Art[y,x + 2], Art[y + 1,x + 2], Art[y+2,x+2]}
                            };
                            row.Add(newGrid);
                        }
                        arts.Add(row);
                    }
                }
                var newArts = new List<List<char[,]>>();
                for (var y = 0; y < arts.Count; y ++)
                {
                    var row = new List<char[,]>();
                    for (var x = 0; x < arts.Count; x++)
                    {
                        row.Add(Enhance(arts[y][x]));
                    }
                    newArts.Add(row);
                }
                Art = CreateSquareArray(newArts);
            }

            var pixelsOn = 0;
            for (var y = 0; y < Art.GetLength(0); y ++)
            {
                for (var x = 0; x < Art.GetLength(0); x ++)
                {
                    if (Art[y, x] == '#') pixelsOn++;
                }
            }
        }
        private static char[,] RotateMatrixCounterClockwise(char[,] oldMatrix)
        {
            char[,] newMatrix = new char[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newColumn, newRow] = oldMatrix[oldColumn, oldRow];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }

        private static char[,] FlipMatrix(char[,] oldMatrix, int dimension)
        {
            char[,] newMatrix = new char[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            if (dimension == 0) //x
            {
                for (int oldColumn = 0; oldColumn < oldMatrix.GetLength(1); oldColumn++)
                {
                    for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                    {
                        newMatrix[oldMatrix.GetLength(0) -1 - oldColumn, oldRow] = oldMatrix[oldColumn, oldRow];
                    }
                }
            }
            else //y
            {
                for (int oldColumn = 0; oldColumn < oldMatrix.GetLength(1); oldColumn++)
                {
                    for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                    {
                        newMatrix[oldColumn, oldMatrix.GetLength(1) - 1 - oldRow] = oldMatrix[oldColumn, oldRow];
                    }
                }
            }
            return newMatrix;
        }

        private static string Serialize(char[,] input)
        {
            var key = "";
            for (var y = 0; y < input.GetLength(0); y++)
            {
                for (var x = 0; x < input.GetLength(0); x++)
                {
                    key += input[y, x];
                }
                key += '/';
            }
            ;
            return key.Substring(0, key.Length - 1);
        }

        private static void PrintMatrix(char[,] input)
        {
            return;
            for (var y = 0; y < input.GetLength(0); y++)
            {
                for (var x = 0; x < input.GetLength(0); x++)
                {
                    Console.Write(input[y,x]);
                }
                Console.WriteLine();
            }
        }
        private static List<string> BuildKeys(char[,] input)
        {
            var originalMatrix = input;
            var keys = new List<string>();
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));

            input = originalMatrix;
            input = FlipMatrix(input, 0);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));

            input = originalMatrix;
            input = FlipMatrix(input, 1);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));

            input = originalMatrix;
            input = FlipMatrix(input, 0);
            input = FlipMatrix(input, 1);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            input = RotateMatrixCounterClockwise(input);
            keys.Add(Serialize(input));
            return keys.Distinct().ToList();
        }
        private static char[,] Enhance(char[,] input)
        {
            var keys = BuildKeys(input);
            char[,] matching = null;
            foreach (var key in keys)
            {
                if (!Enhancements.ContainsKey(key)) continue;
                if (matching != null)
                {
                    
                }
                matching = Enhancements[key];
            }
            return matching;
        }
    }
}