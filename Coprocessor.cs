using System;
using System.Collections.Generic;

namespace Program
{
    public class Coprocessor //Day 23
    {
        public int Number;

        public string[] OriginalInput =
        {
            "set b 67",
            "set c b",
            "jnz a 2",
            "jnz 1 5",
            "mul b 100",
            "sub b -100000",
            "set c b",
            "sub c -17000",
            "set f 1",
            "set d 2",
            "set e 2",
            "set g d",
            "mul g e",
            "sub g b",
            "jnz g 2",
            "set f 0",
            "sub e -1",
            "set g e",
            "sub g b",
            "jnz g -8",
            "sub d -1",
            "set g d",
            "sub g b",
            "jnz g -13",
            "jnz f 2",
            "sub h -1",
            "set g b",
            "sub g c",
            "jnz g 2",
            "jnz 1 3",
            "sub b -17",
            "jnz 1 -23"
        };
        public string[] InputFun1 =
        {
            "set b 67",
            "set c b",
            "jnz a 2",
            "jnz 1 5",
            "mul b 100",
            "sub b -100000",
            "set c b",
            "sub c -17000",
            "set f 1",
            "set d 2",
            "set e 2",
            "fun1",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "jnz g -8",
            "sub d -1",
            "set g d",
            "sub g b",
            "jnz g -13",
            "jnz f 2",
            "sub h -1",
            "set g b",
            "sub g c",
            "jnz g 2",
            "jnz 1 3",
            "sub b -17",
            "jnz 1 -23"
        };
        public string[] InputV2 =
        {
            "set b 67",
            "set c b",
            "jnz a 2",
            "jnz 1 5",
            "mul b 100",
            "sub b -100000",
            "set c b",
            "sub c -17000",
            "set f 1",
            "set d 2",
            "fun2",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "jnz g -13",
            "jnz f 2",
            "sub h -1",
            "set g b",
            "sub g c",
            "jnz g 2",
            "jnz 1 3",
            "sub b -17",
            "jnz 1 -23"
        };
        public string[] Input =
        {
            "set b 67",
            "set c b",
            "jnz a 2",
            "jnz 1 5",
            "mul b 100",
            "sub b -100000",
            "set c b",
            "sub c -17000",
            "set f 1",
            "set d 2",
            "fun3",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "jnz f 2",
            "sub h -1",
            "set g b",
            "sub g c",
            "jnz g 2",
            "jnz 1 3",
            "sub b -17",
            "jnz 1 -23"
        };

        public void fun3()
        {
            Counter += 14;
            for (var d = 2; d < Registers["b"]; d++)
            {
                for (var e = 2; e < Registers["b"]; e++)
                {
                    if (d * e - Registers["b"] == 0)
                    {
                        Registers["f"] = 0;
                        break;
                    }
                    if (d * e - Registers["b"] > 0)
                    {
                        break;
                    }
                }
                if (Registers["f"] == 0)
                {
                    break;
                }
            }
        }

        private bool Fun2Optimize = true;

        public void fun2()
        {
            if (Fun2Optimize)
            {
                Process("set e 2");
                if (Registers["d"] * Registers["e"] - Registers["b"] < 0 && Registers["d"] * Registers["b"] - Registers["b"] > 0
                    && (Registers["d"] * Registers["e"] - Registers["b"]) % 2 == 0)
                {
                    Registers["f"] = 0;
                }
                Process("fun1");
                Registers["d"] = Registers["b"];
                Registers["g"] = 0;
                Counter += 5;
            }
            else
            {
                Process("set e 2");
                Process("fun1");
                //ends at jnz g -8
                Process("sub d -1");
                Process("set g d");
                Process("sub g b");
            }
        }

        private bool Fun1Optimize = true;

        public void fun1()
        {
            if (Fun1Optimize)
            {
                if (Registers["d"] * Registers["e"] - Registers["b"] < 0 && Registers["b"] * Registers["b"] - Registers["b"] > 0
                    && (Registers["d"] * Registers["e"] - Registers["b"]) % 2 == 0)
                {
                    Registers["f"] = 0;
                }
                Registers["e"] = Registers["b"];
                Registers["g"] = 0;
                Counter += 9;
            }
            else
            {
                Process("set g d");
                Process("mul g e");
                Process("sub g b");
                Process("jnz g 2");
                Process("set f 0");
                Process("sub e -1");
                Process("set g e");
                Process("sub g b");
            }
        }

        public void Step()
        {
            if (Counter >= 0 && Counter < (long)Input.Length)
            {
                Process(Input[Counter]);
            }
            else
            {

            }
        }

        private void AlternativeApproach()
        {
            var b = 67 * 100 - (-100000);
            var c = b - (-17000);
            var d = 2;
            var e = 0;
            var f = 0;
            var g = 0;
            var h = 0;
            while (b != c)
            {
                f = 1;
                d = 2;
                while (d != b)
                {
                    e = 2;
                    while (e != b)
                    {
                        if ((d * e - b) % 2 == 0)
                        {
                            f = 0;
                        }
                        e++;
                    }
                    d++;
                }

                if (f == 0) h++;
                b += 17;
            }
        }
        public void Go()
        {
            //alternativeApproach
            //AlternativeApproach();

            SetRegister("a",0);
            SetRegister("a", 1);
            SetRegister("b",0);
            SetRegister("c",0);
            SetRegister("d",0);
            SetRegister("e",0);
            SetRegister("f",0);
            SetRegister("g",0);
            SetRegister("h",0);
            
            //So, the right value for h is: ...0?
            var print = false;
            Input = Input;
            while (Counter >= 0 && Counter < (long)Input.Length)
            {
                if (print)
                {
                    Console.WriteLine("Registers");
                    foreach (var register in Registers)
                    {
                        Console.WriteLine($"{register.Key}: {register.Value}");
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Program (Counter: {Counter})");
                    for (var i = 0; i < Input.Length; i++)
                    {
                        if (i == Counter) Console.WriteLine($"==> {Input[i]}");
                        else Console.WriteLine($"    {Input[i]}");
                    }
                }
                Process(Input[Counter]);
                if (print)
                {
                    Console.Clear();
                }
            }
        }

        public long SendCount = 0;
        public long Counter = 0;
        public Queue<long> Incoming = new Queue<long>();
        public Dictionary<string, long> Registers = new Dictionary<string, long>();
        public void Process(string step)
        {
            switch (step.Split(' ')[0])
            {
                case "fun1":
                    Counter--;
                    fun1();
                    break;
                case "fun2":
                    Counter--;
                    fun2();
                    break;
                case "fun3":
                    Counter--;
                    fun3();
                    break;
                case "nop":
                    Counter++;
                    break;
                case "set":
                    Set(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
                case "sub":
                    Sub(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
                case "mul":
                    Mul(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
                case "jnz":
                    Jnz(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
            }
        }

        public long GetRegister(string target)
        {
            if (!Registers.ContainsKey(target))
            {
                Registers[target] = 0;
            }
            return Registers[target];
        }

        public void SetRegister(string target, long value)
        {
            GetRegister(target);
            Registers[target] = value;
        }
        public void Set(string target, string argument)
        {
            try
            {
                SetRegister(target, long.Parse(argument));
            }
            catch
            {
                SetRegister(target, GetRegister(argument));
            }
        }
        public void Sub(string target, string argument)
        {
            try
            {
                SetRegister(target, GetRegister(target) - long.Parse(argument));
            }
            catch
            {
                SetRegister(target, GetRegister(target) - GetRegister(argument));
            }
        }
        public void Mul(string target, string argument)
        {
            try
            {
                SetRegister(target, GetRegister(target) * long.Parse(argument));
            }
            catch
            {
                SetRegister(target, GetRegister(target) * GetRegister(argument));
            }
        }
        public void Mod(string target, string argument)
        {
            try
            {
                SetRegister(target, GetRegister(target) % long.Parse(argument));
            }
            catch
            {
                SetRegister(target, GetRegister(target) % GetRegister(argument));
            }
        }
        public void Jnz(string target, string argument)
        {
            int _ = 0;
            if (int.TryParse(target, out _))
            {
                if (int.Parse(target) != 0)
                {
                    try
                    {
                        Counter += (long)int.Parse(argument);
                    }
                    catch
                    {
                        Counter += GetRegister(argument);
                    }
                    Counter--;
                }
            }
            else if (GetRegister(target) != 0)
            {
                try
                {
                    Counter += (long)int.Parse(argument);
                }
                catch
                {
                    Counter += GetRegister(argument);
                }
                Counter--;
            }
        }
    }
}