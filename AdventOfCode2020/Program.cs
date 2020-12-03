using System;
using System.IO;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(String.Format("Day 1 p. 1: {0}", Get2020PairProduct()));
            Console.WriteLine(String.Format("Day 1 p. 2: {0}", Get2020ThreeProduct()));
        }

        static int Get2020PairProduct()
        {
            string filename = "day1inputs.txt";

            string[] contents = File.ReadAllLines(filename);
            foreach (string line in contents)
            {
                int val1 = Convert.ToInt32(line);

                foreach (string line2 in contents)
                {
                    int val2 = Convert.ToInt32(line2);
                    int sum = val1 + val2;

                    if (sum == 2020)
                    {
                        return val1 * val2;
                    }
                }
            }

            return 0;
        }

        static int Get2020ThreeProduct()
        {
            string filename = "day1inputs.txt";

            string[] contents = File.ReadAllLines(filename);
            foreach (string line in contents)
            {
                int val1 = Convert.ToInt32(line);

                foreach (string line2 in contents)
                {
                    int val2 = Convert.ToInt32(line2);

                    foreach (string line3 in contents)
                    {
                        int val3 = Convert.ToInt32(line3);
                        int sum = val1 + val2 + val3;

                        if (sum == 2020)
                        {
                            return val1 * val2 * val3;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
