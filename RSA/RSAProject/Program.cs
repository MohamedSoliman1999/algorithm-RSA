using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RSA;
using System.Diagnostics;

namespace algoproject
{
    class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {
                    
            RSA.BigInteger x = new RSA.BigInteger();
            string Mul, Sub, Add;
            List<string> ans = new List<string>();
            Console.WriteLine("********************RSA Project********************");
            string choose;
            Console.WriteLine("[1]-Adding Function:");
            Console.WriteLine("[2]-Subtract Function:");
            Console.WriteLine("[3]-Multiply Function:");
            Console.Write("Enter your choose :");
            choose = Console.ReadLine();
            if (choose == "1")
            {
                string filepath = "AddTestCases.txt";
                List<string> lines = File.ReadAllLines(filepath).ToList();
                List<string> fileinput = new List<string>();
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                        fileinput.Add(line);
                }
                Add = "Add_output.txt";

                
                /////////////////////////////////////////
                Stopwatch cas = Stopwatch.StartNew();
                cas.Start();
                for (int i = 1; i < fileinput.Count; i += 2)
                {
                   // sw.Start();
                  
                    ans.Add(x.Add(fileinput[i], fileinput[i + 1]));
                    if (i + 2 < fileinput.Count)
                        ans.Add("");
                }

                cas.Stop();
                Console.WriteLine("case "  + " is " + cas.Elapsed);

                File.WriteAllLines(Add, ans);
            }
            else if (choose == "2")
            {
                string filepath = "SubtractTestCases.txt";
                List<string> lines = File.ReadAllLines(filepath).ToList();
                List<string> fileinput = new List<string>();
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                        fileinput.Add(line);
                }
                Sub = "Sub_output.txt";
                Stopwatch cas = Stopwatch.StartNew();
                cas.Start();
                for (int i = 1; i < fileinput.Count; i += 2)
                {
                    ans.Add(x.subtract(fileinput[i], fileinput[i + 1]));
                    if (i + 2 < fileinput.Count)
                        ans.Add("");
                }
                cas.Stop();
                Console.WriteLine("case " + " is " + cas.Elapsed);
                File.WriteAllLines(Sub, ans);
            }
            else if (choose == "3")
            {
                string filepath = "MultiplyTestCases.txt";
                List<string> lines = File.ReadAllLines(filepath).ToList();
                List<string> fileinput = new List<string>();
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                        fileinput.Add(line);
                }
                Mul = "Mul_output.txt";
                Stopwatch cas = Stopwatch.StartNew();
                cas.Start();
                for (int i = 1; i < fileinput.Count; i += 2)
                {
                    ans.Add(x.Mul(fileinput[i], fileinput[i + 1]));
                    if (i + 2 < fileinput.Count)
                        ans.Add("");
                }
                cas.Stop();
                Console.WriteLine("case " + " is " + cas.Elapsed);
                File.WriteAllLines(Mul, ans);
            }
        
            }
        }
    }
}
