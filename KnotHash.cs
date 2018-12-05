using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class KnotHash
    {
        public static string HashToHex(string steps)
        {
            var result = KnotHash.Hash(steps);
            var hexresult = string.Join("", result.Select(r => r.ToString("X").PadLeft(2, '0')));
            return hexresult;
        }
        public static byte[] Hash(string steps)
        {
            var input = new List<byte>();
            for (var i = 0; i < 256; i++)
            {
                input.Add((byte)i);
            }
            var localInput = input.ToList();
            var localSteps = steps.Select(c => (byte)c).ToList();
            localSteps.AddRange(new byte[] {17, 31, 73, 47, 23});
            var skipSize = 0;
            var cur = 0;
            for (var k = 0; k < 64; k++)
            {
                foreach (var length in localSteps)
                {
                    for (var i = cur; i < cur + (length / 2) + ((length % 2 == 0) ? 0 : 1); i++)
                    {
                        var hereIdx = i % input.Count;
                        var otherIdx = (cur + length - (i - cur) - 1) % input.Count;
                        if (hereIdx == otherIdx) break;
                        var here = localInput[hereIdx];
                        var otherSide = localInput[otherIdx];
                        localInput[otherIdx] = here;
                        localInput[hereIdx] = otherSide;
                    }

                    cur += length + skipSize;
                    cur = cur % input.Count;
                    skipSize++;
                }
            }

            var finalOutput = new List<byte>();

            for (var i = 0; i < 256; i += 16)
            {
                finalOutput.Add((byte)(
                        localInput[i+0] ^
                        localInput[i + 1] ^
                        localInput[i + 2] ^
                        localInput[i + 3] ^
                        localInput[i + 4] ^
                        localInput[i + 5] ^
                        localInput[i + 6] ^
                        localInput[i + 7] ^
                        localInput[i + 8] ^
                        localInput[i + 9] ^
                        localInput[i + 10] ^
                        localInput[i + 11] ^
                        localInput[i + 12] ^
                        localInput[i + 13] ^
                        localInput[i + 14] ^
                        localInput[i + 15])
                );
            }

            return finalOutput.ToArray();
        }
    }
}