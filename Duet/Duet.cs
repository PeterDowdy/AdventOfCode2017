using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Duet //Day 18
    {
        public int Number;
        public string[] Input = { "set i 31", "set a 1", "mul p 17", "jgz p p", "mul a 2", "add i -1", "jgz i -2", "add a -1", "set i 127", "set p 618", "mul p 8505", "mod p a", "mul p 129749", "add p 12345", "mod p a", "set b p", "mod b 10000", "snd b", "add i -1", "jgz i -9", "jgz a 3", "rcv b", "jgz b -1", "set f 0", "set i 126", "rcv a", "rcv b", "set p a", "mul p -1", "add p b", "jgz p 4", "snd a", "set a b", "jgz 1 3", "snd b", "set f 1", "add i -1", "jgz i -11", "snd a", "jgz f -16", "jgz a -19" };
        public Duet Other { get; set; }

        public void Step()
        {
            if (!string.IsNullOrEmpty(Waiting))
            {
                Rcv(Waiting);
            }
            else if (Counter >= 0 && Counter < (long)Input.Length)
            {
                Process(Input[Counter]);
            }
            else
            {
                
            }
        }
        public void Go()
        {
            while (Counter >= 0 && Counter < (long)Input.Length)
            {
                Process(Input[Counter]);
            }
        }

        public long SendCount = 0;
        public string Waiting = null;
        public long Counter = 0;
        public Queue<long> Incoming = new Queue<long>();
        public Dictionary<string,long> Registers = new Dictionary<string, long>();
        public void Process(string step)
        {
            switch (step.Split(' ')[0])
            {
                case "snd":
                    Snd(step.Split(' ')[1]);
                    Counter++;
                    break;
                case "set":
                    Set(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
                case "add":
                    Add(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
                case "mul":
                    Mul(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
                case "mod":
                    Mod(step.Split(' ')[1], step.Split(' ')[2]);
                    Counter++;
                    break;
                case "rcv":
                    Rcv(step.Split(' ')[1]);
                    Counter++;
                    break;
                case "jgz":
                    Jgz(step.Split(' ')[1], step.Split(' ')[2]);
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
        public void Snd(string target)
        {
            SendCount++;
            lock (Other.Incoming)
            {
                try
                {
                    Other.Incoming.Enqueue(long.Parse(target));
                }
                catch
                {
                    Other.Incoming.Enqueue(GetRegister(target));
                }
            }
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
        public void Add(string target, string argument)
        {
            try
            {
                SetRegister(target, GetRegister(target) + long.Parse(argument));
            }
            catch
            {
                SetRegister(target, GetRegister(target) + GetRegister(argument));
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
        public void Rcv(string target)
        {
            if (!Incoming.Any())
            {
                Waiting = target;
                return;
            }
            else
            {
                Waiting = null;
                SetRegister(target, Incoming.Dequeue());
            }
        }
        public void Jgz(string target, string argument)
        {
            int _ = 0;
            if (int.TryParse(target, out _))
            {
                if (int.Parse(target) > 0)
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
            else if (GetRegister(target) > 0)
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