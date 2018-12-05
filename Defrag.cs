using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Defrag //Day 14
    {
        public static int[][] Sort(int[][] map)
        {
            bool aChangeHappend = false;
            do
            {
                var numChanges = 0;
                aChangeHappend = false;
                for (var i = 0; i < map.Length; i++)
                {
                    for (var j = 0; j < map.Length - 1; j++)
                    {
                        var l = map[i][j];
                        var r = map[i][j + 1];
                        if (l != 0 && r != 0 && l != r)
                        {
                            numChanges++;
                            aChangeHappend = true;
                            map[i][j + 1] = Math.Max(l, r);
                            map[i][j] = Math.Max(l, r);
                        }
                    }
                }
                for (var i = 0; i < map.Length - 1; i++)
                {
                    for (var j = 0; j < map.Length; j++)
                    {
                        var l = map[i][j];
                        var r = map[i + 1][j];
                        if (l != 0 && r != 0 && l != r)
                        {
                            numChanges++;
                            aChangeHappend = true;
                            map[i + 1][j] = Math.Max(l, r);
                            map[i][j] = Math.Max(l, r);
                        }
                    }
                }
                Console.WriteLine($"{numChanges} changes made.");
            } while (aChangeHappend);
            return map;
        }
        public static int[][] GetSquares(string hashInput)
        {
            var seq = 1;
            var result = new List<List<int>>();
            for (var i = 0; i < 128; i++)
            {
                var hexresult = KnotHash.HashToHex($"amgozmfv-{i}");
                string binarystring = String.Join(String.Empty,
                    hexresult.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );
                result.Add(new List<int>(binarystring.Select(b => b == '1' ? ++seq : 0).ToList()));
            }
            return result.Select(r => r.ToArray()).ToArray();
        }
    }
}