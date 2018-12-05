using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Scanner
    {
        private static void Init(int step)
        {
            if (step == 0)
            {
                _layers = new Dictionary<int, int>
                {
                    {0, 3},
                    {1, 2},
                    {2, 4},
                    {4, 4},
                    {6, 5},
                    {8, 8},
                    {10, 6},
                    {12, 6},
                    {14, 8},
                    {16, 6},
                    {18, 6},
                    {20, 8},
                    {22, 12},
                    {24, 8},
                    {26, 8},
                    {28, 12},
                    {30, 8},
                    {32, 12},
                    {34, 9},
                    {36, 14},
                    {38, 12},
                    {40, 12},
                    {42, 12},
                    {44, 14},
                    {46, 14},
                    {48, 10},
                    {50, 14},
                    {52, 12},
                    {54, 14},
                    {56, 12},
                    {58, 17},
                    {60, 10},
                    {64, 14},
                    {66, 14},
                    {68, 12},
                    {70, 12},
                    {72, 18},
                    {74, 14},
                    {78, 14},
                    {82, 14},
                    {84, 24},
                    {86, 14},
                    {94, 14}
                };
                _scanners = new Dictionary<int, int>
                {
                    {0, 0},
                    {1, 0},
                    {2, 0},
                    {4, 0},
                    {6, 0},
                    {8, 0},
                    {10, 0},
                    {12, 0},
                    {14, 0},
                    {16, 0},
                    {18, 0},
                    {20, 0},
                    {22, 0},
                    {24, 0},
                    {26, 0},
                    {28, 0},
                    {30, 0},
                    {32, 0},
                    {34, 0},
                    {36, 0},
                    {38, 0},
                    {40, 0},
                    {42, 0},
                    {44, 0},
                    {46, 0},
                    {48, 0},
                    {50, 0},
                    {52, 0},
                    {54, 0},
                    {56, 0},
                    {58, 0},
                    {60, 0},
                    {64, 0},
                    {66, 0},
                    {68, 0},
                    {70, 0},
                    {72, 0},
                    {74, 0},
                    {78, 0},
                    {82, 0},
                    {84, 0},
                    {86, 0},
                    {94, 0}
                };
                _directions = new Dictionary<int, bool>
                {
                    {0, false},
                    {1, false},
                    {2, false},
                    {4, false},
                    {6, false},
                    {8, false},
                    {10, false},
                    {12, false},
                    {14, false},
                    {16, false},
                    {18, false},
                    {20, false},
                    {22, false},
                    {24, false},
                    {26, false},
                    {28, false},
                    {30, false},
                    {32, false},
                    {34, false},
                    {36, false},
                    {38, false},
                    {40, false},
                    {42, false},
                    {44, false},
                    {46, false},
                    {48, false},
                    {50, false},
                    {52, false},
                    {54, false},
                    {56, false},
                    {58, false},
                    {60, false},
                    {64, false},
                    {66, false},
                    {68, false},
                    {70, false},
                    {72, false},
                    {74, false},
                    {78, false},
                    {82, false},
                    {84, false},
                    {86, false},
                    {94, false}
                };
            }
            else
            {
                _layers = _storedLayers[step];
                _scanners = _storedScanners[step];
                _directions = _storedDirections[step];
            }
        }
        private static Dictionary<int, int> _layers;
        private static Dictionary<int, int> _scanners;
        private static Dictionary<int, bool> _directions;

        private static Dictionary<int, Dictionary<int, int>> _storedLayers = new Dictionary<int, Dictionary<int, int>>();
        private static Dictionary<int, Dictionary<int, int>> _storedScanners = new Dictionary<int, Dictionary<int, int>>();
        private static Dictionary<int, Dictionary<int, bool>> _storedDirections = new Dictionary<int, Dictionary<int, bool>>();
        //less than 18 million!
        private static int _picoSecond = 0;


        private static Dictionary<int, int> Copy(Dictionary<int, int> fuck)
        {
            var output = new Dictionary<int, int>();
            foreach (var kvp in fuck)
            {
                output[kvp.Key] = kvp.Value;
            }
            return output;
        }
        private static Dictionary<int, bool> Copy(Dictionary<int, bool> fuck)
        {
            var output = new Dictionary<int, bool>();
            foreach (var kvp in fuck)
            {
                output[kvp.Key] = kvp.Value;
            }
            return output;
        }

        private static void Pulse()
        {
            foreach (var key in _layers.Keys)
            {
                var scanner = _scanners[key];
                var layer = _layers[key];
                if (scanner + 1 == layer || scanner == 0)
                {
                    _directions[key] = !_directions[key];
                }
                _scanners[key] += _directions[key] ? 1 : -1;
            }
        }

        public static void Run()
        {
            var start = DateTime.UtcNow;
            int severity = 0;
            //do
            //{
            //    Init(_picoSecond);
            //    _picoSecond++;
            //    severity = 0;
            //    Pulse();
            //    _storedLayers = new Dictionary<int, Dictionary<int, int>> { [_picoSecond] = Copy(_layers) };
            //    _storedScanners = new Dictionary<int, Dictionary<int, int>> { [_picoSecond] = Copy(_scanners) };
            //    _storedDirections = new Dictionary<int, Dictionary<int, bool>> { [_picoSecond] = Copy(_directions) };
            //    for (var i = 0; i < 95; i++)
            //    {
            //        if (_scanners.ContainsKey(i) && _scanners[i] == 0 && i != 0)
            //        {
            //            severity = -1;
            //            Console.WriteLine($"[{_picoSecond}]+[{i+1}]=[{_picoSecond+i+1}] Got caught at step {i+1}:Depth({_scanners[i]}/{_layers[i]})");
            //            break;
            //        }
            //        Pulse();
            //    }
            //    if (_picoSecond % 100 == 0)
            //    {

            //    }
            //} while (severity != 0);
            do
            {
                Init(_picoSecond);
                _picoSecond++;
                severity = 0;
                Pulse();
                _storedLayers = new Dictionary<int, Dictionary<int, int>> { [_picoSecond] = Copy(_layers) };
                _storedScanners = new Dictionary<int, Dictionary<int, int>> { [_picoSecond] = Copy(_scanners) };
                _storedDirections = new Dictionary<int, Dictionary<int, bool>> { [_picoSecond] = Copy(_directions) };

                for (var i = 0; i < 95; i++)
                {
                    if (_scanners.ContainsKey(i) && _scanners[i] == 0)
                    {
                        severity = 1;
                        break;
                    }
                    Pulse();
                }
            } while (severity != 0);
            Console.WriteLine($"Severity: {severity}");
            Console.WriteLine($"{_picoSecond}ps");
        }
    }
}
