using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program
{
    public class BridgeBuilding //Day 24
    {
        private static List<Tuple<int,int>> Pieces => new[] { "48/5", "25/10", "35/49", "34/41", "35/35", "47/35", "34/46", "47/23", "28/8", "27/21", "40/11", "22/50", "48/42", "38/17", "50/33", "13/13", "22/33", "17/29", "50/0", "20/47", "28/0", "42/4", "46/22", "19/35", "17/22", "33/37", "47/7", "35/20", "8/36", "24/34", "6/7", "7/43", "45/37", "21/31", "37/26", "16/5", "11/14", "7/23", "2/23", "3/25", "20/20", "18/20", "19/34", "25/46", "41/24", "0/33", "3/7", "49/38", "47/22", "44/15", "24/21", "10/35", "6/21", "14/50" }.Select(s => new Tuple<int,int>(int.Parse(s.Split('/')[0]),int.Parse(s.Split('/')[1]))).ToList();
        private static List<int> Bridge = new List<int> {0};

        public static void Go()
        {
            var maxWeight = BuildMaxBridge(Bridge, Pieces);
        }

        public static List<int> BuildMaxBridge(List<int> bridgeSoFar, List<Tuple<int, int>> PiecesRemaining)
        {
            var fragments = new List<List<int>>();
            Parallel.ForEach(PiecesRemaining, piece =>
            {
                if (piece.Item1 != bridgeSoFar.Last() && piece.Item2 != bridgeSoFar.Last())
                {
                    return;
                }
                int[] nextBridge = new int[bridgeSoFar.Count + 2];
                bridgeSoFar.CopyTo(nextBridge);
                if (piece.Item1 == bridgeSoFar.Last())
                {
                    nextBridge[bridgeSoFar.Count] = piece.Item1;
                    nextBridge[bridgeSoFar.Count + 1] = piece.Item2;
                }
                else
                {
                    nextBridge[bridgeSoFar.Count] = piece.Item2;
                    nextBridge[bridgeSoFar.Count + 1] = piece.Item1;
                }
                fragments.Add(BuildMaxBridge(nextBridge.ToList(), PiecesRemaining.Where(p => p != piece).ToList()));
            });
            if (fragments.Any())
            {
                return fragments.OrderByDescending(f => f.Count).ThenByDescending(f => f.Sum()).First();
            }
            return bridgeSoFar;
        }
    }
}