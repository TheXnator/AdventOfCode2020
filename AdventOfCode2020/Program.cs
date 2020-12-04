using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            // Day 1
            Console.WriteLine(String.Format("Day 1 p. 1: {0}", Get2020PairProduct()));
            Console.WriteLine(String.Format("Day 1 p. 2: {0}", Get2020ThreeProduct()));

            // Day 2
            Console.WriteLine(String.Format("Day 2 p. 1: {0}", CheckValidPasswords()));
            Console.WriteLine(String.Format("Day 2 p. 2: {0}", CheckValidPasswords(true)));

            // Day 3
            Console.WriteLine(String.Format("Day 3 p. 1: {0}", FindTrees(3, 1)));
            ulong treeproduct = FindTrees(1, 1) * FindTrees(3, 1) * FindTrees(5, 1) * FindTrees(7, 1) * FindTrees(1, 2);
            Console.WriteLine(String.Format("Day 3 p. 2: {0}", treeproduct));

            // Day 4
            Console.WriteLine(String.Format("Day 4 p. 1: {0}", GetValidPassports()));
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

        static int CheckValidPasswords(bool isnew=false)
        {
            string filename = "day2inputs.txt";
            int valid = 0;

            string[] contents = File.ReadAllLines(filename);
            foreach (string line in contents)
            {
                string[] dat = line.Split(" ");
                string[] minmax = dat[0].Split("-");

                char c = dat[1][0];
                int min = Convert.ToInt32(minmax[0]);
                int max = Convert.ToInt32(minmax[1]);
                int count = dat[2].Split(c).Length - 1;

                if ((!isnew && count >= min && count <= max) || (isnew && ((dat[2][min-1] == c && dat[2][max-1] != c) || (dat[2][max - 1] == c && dat[2][min - 1] != c))))
                {
                    valid++;
                }
            }

            return valid;
        }

        static ulong FindTrees(int across, int down)
        {
            string filename = "day3inputs.txt";
            ulong trees = 0;

            string[] contents = File.ReadAllLines(filename);
            int linelength = contents[0].Length;
            int pos = across;

            for (int x = down; x < contents.Length;)
            {
                char c = contents[x][pos];

                if (c == '#')
                {
                    trees++;
                }

                pos = ((pos + across) >= linelength) ? (across - (linelength - pos)) : pos + across;
                x = x + down;
            }

            return trees;
        }

        static int GetValidPassports()
        {
            string filename = "day4inputs.txt";
            int valid = 0;

            string contents = File.ReadAllText(filename);
            string[] data = contents.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            string[] requiredFields = new string[7] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            foreach (string dat in data)
            {
                bool isValid = true;

                foreach (string field in requiredFields)
                {
                    if (!dat.Contains(field))
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    valid++;
                }
            }

            return valid;
        }
    }
}