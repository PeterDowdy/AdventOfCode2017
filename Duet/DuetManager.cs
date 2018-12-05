using System;

namespace Program
{
    public class DuetManager
    {
        public static void Go()
        {
            var goThisLong = 0;
            var duet1 = new Duet();
            duet1.Number = 0;
            duet1.Registers["p"] = 0;
            var duet2 = new Duet();
            duet2.Number = 1;
            duet2.Registers["p"] = 1;
            duet1.Other = duet2;
            duet2.Other = duet1;
            while (true)
            {
                //Console.WriteLine("PROGRAM 0");
                //Console.WriteLine(("REGISTERS " + string.Join(" : ", duet1.Registers.Select(kvp => $"{kvp.Key}:{kvp.Value}"))).PadRight(100));
                //Console.WriteLine(("COUNTER " + duet1.Counter + " : " + duet1.Input[duet1.Counter]).PadRight(100));
                //Console.WriteLine("QUEUE LENGTH: " + duet1.Incoming.Count);
                //Console.WriteLine("PROGRAM 1");
                //Console.WriteLine(("REGISTERS " + string.Join(" : ", duet2.Registers.Select(kvp => $"{kvp.Key}:{kvp.Value}"))).PadRight(100));
                //Console.WriteLine(("COUNTER " + duet2.Counter + " : " + duet2.Input[duet2.Counter]).PadRight(100));
                //Console.WriteLine("QUEUE LENGTH: " + duet2.Incoming.Count);
                //Console.SetCursorPosition(0,0);
                duet1.Step();
                duet2.Step();
                if (duet1.Waiting != null && duet2.Waiting != null)
                {

                }
                if (goThisLong < int.MinValue)
                {
                    goThisLong = -1;
                }
                if (goThisLong > 0)
                {
                    goThisLong--;
                }
                else
                {
                    while (goThisLong != 0)
                    {
                        try
                        {
                            var goInput = Console.ReadLine();
                            goThisLong = int.Parse(goInput);
                        }
                        catch { }
                    }
                }
            }
        }
    }
}