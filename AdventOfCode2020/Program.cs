using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Console.WriteLine(String.Format("Day 4 p. 2: {0}", GetValidPassportsNew()));

            // Day 5
            Console.WriteLine(String.Format("Day 5 p. 1: {0}", GetMaxBoardingPass()));
            Console.WriteLine(String.Format("Day 5 p. 2: {0}", GetSeatID()));

            // Day 6
            Console.WriteLine(String.Format("Day 6 p. 1: {0}", GetCustomsQuestionSum()));
            Console.WriteLine(String.Format("Day 6 p. 2: {0}", GetCustomsQuestionSum(true)));

            // Day 7
            Console.WriteLine(String.Format("Day 7 p. 1: {0}", GetValidBags("shiny gold")));
            Console.WriteLine(String.Format("Day 7 p. 2: {0}", GetBagContents("shiny gold bag")));

            // Day 8
            Console.WriteLine(String.Format("Day 8 p. 1: {0}", GetAccumulator(File.ReadAllLines("day8inputs.txt"))));
            Console.WriteLine(String.Format("Day 8 p. 2: {0}", GetAccumulator(FixBootCode())));

            // Day 9G
            Console.WriteLine(String.Format("Day 9 p. 1: {0}", GetXMASInvalidNum())); 
            Console.WriteLine(String.Format("Day 9 p. 2: {0}", FindXMASEncryptionWeakness()));
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

        static int CheckValidPasswords(bool isnew = false)
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

                if ((!isnew && count >= min && count <= max) || (isnew && ((dat[2][min - 1] == c && dat[2][max - 1] != c) || (dat[2][max - 1] == c && dat[2][min - 1] != c))))
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

        static int GetValidPassportsNew()
        {
            string filename = "day4inputs.txt";
            int valid = 0;

            string contents = File.ReadAllText(filename);
            string[] data = contents.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            string[] eyeCols = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            Dictionary<string, int[]> requiredFields = new Dictionary<string, int[]>();
            requiredFields.Add("byr", new int[3] { 1920, 2002, 4 });
            requiredFields.Add("iyr", new int[3] { 2010, 2020, 4 });
            requiredFields.Add("eyr", new int[3] { 2020, 2030, 4 });
            requiredFields.Add("hgt", new int[3] { 0, 0, 0 });
            requiredFields.Add("hcl", new int[3] { 0, 0, 0 });
            requiredFields.Add("ecl", new int[3] { 0, 0, 0 });
            requiredFields.Add("pid", new int[3] { 0, 0, 9 });

            foreach (string dat in data)
            {
                bool isValid = true;

                foreach (KeyValuePair<string, int[]> field in requiredFields)
                {
                    if (!dat.Contains(field.Key))
                    {
                        isValid = false;
                    }
                    else
                    {
                        string fieldData = dat.Split(field.Key + ":")[1].Split(new string[] { " ", "\n" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        fieldData = Regex.Escape(fieldData).Replace("\\r", "");
                        fieldData = Regex.Escape(fieldData).Replace("\\", "");

                        for (int pos = 0; pos < 3; pos++)
                        {
                            int i = field.Value[pos];
                            if (i > 0 && isValid)
                            {
                                isValid = (pos == 0) ? (i <= Convert.ToInt32(fieldData)) : ((pos == 1) ? (i >= Convert.ToInt32(fieldData)) : fieldData.Length == i);
                            }
                        }

                        if (field.Key == "hgt" && (isValid))
                        {
                            bool cm = fieldData.Contains("cm");
                            isValid = (fieldData.Contains("cm") || fieldData.Contains("in")) && (Convert.ToInt32(fieldData.Split(cm ? "cm" : "in")[0]) >= (cm ? 150 : 59) && Convert.ToInt32(fieldData.Split(cm ? "cm" : "in")[0]) <= (cm ? 193 : 76));
                        }
                        else if (field.Key == "hcl" && (isValid))
                        {
                            isValid = fieldData[0] == '#' && Regex.Split(fieldData.Split('#')[1], @"[a-f\d]").Length == 7;
                        }
                        else if (field.Key == "ecl" && (isValid))
                        {
                            isValid = (Array.IndexOf(eyeCols, fieldData) > -1);
                        }
                    }
                }

                if (isValid)
                {
                    valid++;
                }
            }

            return valid;
        }

        static List<int> GetAllPassIDs()
        {
            string filename = "day5inputs.txt";
            List<int> passids = new List<int>();

            string[] contents = File.ReadAllLines(filename);
            foreach (string line in contents)
            {
                string parsedRow = line.Substring(0, 7).Replace('F', '0').Replace('B', '1');
                int row = Convert.ToInt32(parsedRow, 2);

                string parsedCol = line.Substring(7).Replace('L', '0').Replace('R', '1');
                int col = Convert.ToInt32(parsedCol, 2);

                int id = row * 8 + col;
                passids.Add(id);
            }

            return passids;
        }

        static int GetMaxBoardingPass()
        {
            List<int> ids = GetAllPassIDs();
            int passid = 0;

            foreach (int i in ids)
            {
                passid = (i > passid) ? i : passid;
            }

            return passid;
        }

        static int GetSeatID()
        {
            List<int> ids = GetAllPassIDs();
            ids.Sort();

            foreach (int i in ids)
            {
                if ((!ids.Contains(i + 1)) && ids.Contains(i + 2))
                {
                    return i + 1;
                }
            }

            return 0;
        }

        static List<int> GetCustomsQuestions()
        {
            string filename = "day6inputs.txt";

            string contents = File.ReadAllText(filename);
            string[] data = contents.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<int> questions = new List<int>();

            foreach (string dat in data)
            {
                List<char> answered = new List<char>();

                foreach (string line in dat.Split("\n"))
                {
                    foreach (char c in line)
                    {
                        if (!answered.Contains(c) && Regex.IsMatch(c.ToString(), @"\w"))
                        {
                            answered.Add(c);
                        }
                    }
                }

                questions.Add(answered.Count);
            }

            return questions;
        }

        static List<int> GetAllAnsweredCustomsQuestions()
        {
            string filename = "day6inputs.txt";

            string contents = File.ReadAllText(filename);
            string[] data = contents.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<int> questions = new List<int>();

            foreach (string dat in data)
            {
                List<char> answered = new List<char>();
                List<char> answeredall = new List<char>();
                string[] lines = dat.Split("\n");

                foreach (string line in lines)
                {
                    foreach (char c in line)
                    {
                        if (Regex.IsMatch(c.ToString(), @"\w"))
                        {
                            answered.Add(c);
                        }
                    }
                }

                foreach (char c in answered)
                {

                    int count = 0;
                    foreach (char c2 in answered)
                    {
                        if (c2 == c)
                        {
                            count++;
                        }
                    }

                    if (count == lines.Length && (!answeredall.Contains(c)))
                    {
                        answeredall.Add(c);
                    }
                }

                questions.Add(answeredall.Count);
            }

            return questions;
        }

        static int GetCustomsQuestionSum(bool all = false)
        {
            int sum = 0;

            foreach (int i in all ? GetAllAnsweredCustomsQuestions() : GetCustomsQuestions())
            {
                sum += i;
            }

            return sum;
        }

        static string GetBagRow(string bag)
        {
            string filename = "day7inputs.txt";
            string[] contents = File.ReadAllLines(filename);

            foreach (string line in contents)
            {
                if (line.Split(" bags contain")[0].Contains(bag))
                {
                    return line;
                }
            }

            return "";
        }

        static int GetValidBags(string bag)
        {
            string filename = "day7inputs.txt";
            string[] contents = File.ReadAllLines(filename);

            List<string> bags = new List<string>() { bag };
            int i = 0;

            while (i < bags.Count)
            {
                foreach (string line in contents)
                {
                    if (line.Contains(bags[i]))
                    {
                        string[] splitbag = line.Split(' ');
                        string addBag = String.Format("{0} {1}", splitbag[0], splitbag[1]);
                        if (!bags.Contains(addBag))
                        {
                            bags.Add(addBag);
                        }
                    }
                }

                i++;
            }

            return bags.Count - 1;
        }

        static int GetBagContents(string bag)
        {
            string filename = "day7inputs.txt";
            string[] contents = File.ReadAllLines(filename);

            Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();
            foreach (string line in contents)
            {
                MatchCollection bagsContents = Regex.Matches(line, @"(\w* \w*) bag");
                string initial = bagsContents[0].Value.Trim(' ');
                Dictionary<string, int> contentVals = new Dictionary<string, int>();

                bool skip = true;
                foreach (Match m in bagsContents)
                {
                    if (skip) { skip = false; continue; }

                    string[] splitstr = line.Split(m.Value)[0].Trim().Split(' ');
                    string str = splitstr[splitstr.Length - 1];
                    if (str != "contain")
                    {
                        contentVals.Add(m.Value, Convert.ToInt32(str));
                    }
                }

                bags[initial] = contentVals;
            }

            int BagContents(string bag)
            {
                int tot = 1;
                if (bags.ContainsKey(bag))
                {
                    foreach (KeyValuePair<string, int> kv in bags[bag])
                    {
                        tot += kv.Value * BagContents(kv.Key);
                    }
                }

                return tot;
            }

            return BagContents(bag) - 1;
        }

        static int GetAccumulator(string[] contents)
        {
            int acc = 0;

            List<int> executed = new List<int>();
            int nextExecute = 0;

            while ((!executed.Contains(nextExecute)) && (nextExecute < contents.Length))
            {
                string cmd = contents[nextExecute].Substring(0, 3);
                int val = Convert.ToInt32(contents[nextExecute].Split(' ')[1].Trim(' '));
                executed.Add(nextExecute);

                if (cmd == "acc")
                {
                    acc += val;
                    nextExecute++;
                }
                else if (cmd == "jmp")
                {
                    nextExecute += val;
                }
                else
                {
                    nextExecute++;
                }
            }

            return acc;
        }

        static bool CheckTerminates(string[] contents)
        {
            List<int> executed = new List<int>();
            int nextExecute = 0;
            bool terminates = false;

            while ((!executed.Contains(nextExecute)) && (nextExecute < contents.Length))
            {
                string cmd = contents[nextExecute].Substring(0, 3);
                int val = Convert.ToInt32(contents[nextExecute].Split(' ')[1].Trim(' '));
                executed.Add(nextExecute);

                if (cmd == "jmp")
                {
                    nextExecute += val;
                }
                else
                {
                    nextExecute++;
                }
            }

            if (nextExecute >= contents.Length)
            {
                terminates = true;
            }

            return terminates;
        }

        static string[] FixBootCode()
        {
            string filename = "day8inputs.txt";
            string[] contents = File.ReadAllLines(filename);
            string[] curContents = contents;
            int lastChanged = -1;

            while (!CheckTerminates(curContents))
            {
                curContents = (string[])contents.Clone();

                int curline = 0;
                bool changed = false;
                foreach (string line in curContents)
                {
                    if (changed) { continue; }
                    string cmd = contents[curline].Substring(0, 3);
                    string val = contents[curline].Split(' ')[1].Trim(' ');

                    if (curline > lastChanged && (cmd == "jmp" || cmd == "nop"))
                    {
                        lastChanged = curline;
                        string newcmd = (cmd == "jmp") ? "nop" : "jmp";
                        string newline = String.Format("{0} {1}", newcmd, val);
                        curContents[curline] = newline;
                        changed = true;
                    }

                    curline++;
                }
            }

            return curContents;
        }

        static int GetXMASInvalidNum()
        {
            string filename = "day9inputs.txt";
            string[] contents = File.ReadAllLines(filename);

            for (int i = 25; i < contents.Length - 1; i++)
            {
                bool valid = false;
                int num = Convert.ToInt32(contents[i]);
                for (int x = (i - 25); x < i; x++)
                {
                    for (int y = (i - 25); y < i; y++)
                    {
                        if (Convert.ToInt32(contents[x]) + Convert.ToInt32(contents[y]) == num)
                        {
                            valid = true;
                        }
                    }
                }

                if (!valid) { return num; }
            }

            return 0;
        }

        static int FindXMASEncryptionWeakness()
        {
            int requiredsum = GetXMASInvalidNum();

            string filename = "day9inputs.txt";
            string[] contents = File.ReadAllLines(filename);

            List<long> intvals = new List<long>();
            foreach (string content in contents)
            {
                intvals.Add(Convert.ToInt64(content));
            }

            for (int i = 0; i < intvals.Count - 2; i++)
            {
                for (int x = 1; x < intvals.Count - i; x++)
                {
                    long sum = 0;
                    List<long> vals = intvals.GetRange(i, x);

                    foreach (long val in vals)
                    {
                        sum += val;
                    }
                    
                    if (sum == requiredsum)
                    {
                        vals.Sort();
                        return (int)(vals[0] + vals[vals.Count-1]);
                    }
                }
            }

            return 0;
        }
    }
}