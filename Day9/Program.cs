using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Intcode;
using AdventOfCode.Utils;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            Day9();
            Day9_part2();
        }

        public static void Day9()
        {
            var input = Utils.LoadInstructions("day9");
            var intCode = new Intcode();
            intCode.LoadMemory(input);
            var producedValue = true;
            var result = new List<long>();
            while (producedValue)
            {
                long? output = null;
                producedValue = false;
                intCode.Run(new long[]{1}, l => output = l);
                if (output != null)
                {
                    result.Add( output.Value);
                    producedValue = true;
                }
            }
            Console.WriteLine(result.First()); 
        }
        
        public static void Day9_part2()
        {
            var input = Utils.LoadInstructions("day9");
            var intCode = new Intcode();
            intCode.LoadMemory(input);
            var producedValue = true;
            var result = new List<long>();
            while (producedValue)
            {
                long? output = null;
                producedValue = false;
                intCode.Run(new long[]{2}, l => output = l);
                if (output != null)
                {
                    result.Add( output.Value);
                    producedValue = true;
                }
            }
            Console.WriteLine(result.First());
        }
    }
}