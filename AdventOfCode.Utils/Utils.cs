using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Utils
{
    public static class Utils
    {
        public static long[] LoadInstructions(string filePath)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                   return line.Split(',').Select(x => (long.Parse(x))).ToArray();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
        public static int[] LoadDigits(string filePath)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    String line = sr.ReadToEnd();
                    var x = line.Length;
                    return line.Select(x => x - '0').ToArray();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}