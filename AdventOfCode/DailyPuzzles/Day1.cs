using System.Text.RegularExpressions;

namespace AdventOfCode2023.DailyPuzzles;

internal static class Day1
{
    public class Word
    {
        public Word(int index, string word)
        {
            this.index = index;
            this.word = word;
        }

        public int index { get; set; }
        public string word { get; set; }
    }

    public static void Part1(List<string> lines)
    {
        int total = 0;
        foreach (var line in lines)
        {
            total += GetValueIntsOnlyImproved(line);
        }
        Console.WriteLine(total);
    }

    public static void Part2(List<string> lines)
    {
        int total = 0;
        foreach (var line in lines)
        {
            int value = GetValueStringsToo(line);
            total += value;
        }
        Console.WriteLine(total);
    }

    private static int GetValueStringsToo(string line)
    {
        Word left = null;
        Word right = null;
        List<string> numbers = new List<string>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        foreach (var number in numbers)
        {
            List<int> allOccurences = FindAllOccurrences(line, number);
            if (allOccurences.Count == 0) continue;

            if (left is null || left.index > allOccurences[0])
            {
                left = new Word(allOccurences[0], number);
            }

            int lastOccurence = allOccurences.Last();
            if (right is null || right.index < lastOccurence + number.Length)
            {
                right = new Word(lastOccurence + number.Length - 1, number);
            }
        }

        int leftDigit = -1;
        int rightDigit = -1;
        int leftIndex = 0;
        int rightIndex = line.Length - 1;

        while (leftIndex < (left?.index ?? line.Length))
        {
            var success = int.TryParse(line[leftIndex].ToString(), out int result);
            if (success)
            {
                leftDigit = result;
                break;
            }
            leftIndex++;
        }

        leftDigit = leftDigit == -1 ? numbers.IndexOf(left.word) + 1 : leftDigit;

        while (rightIndex >= (right?.index ?? 0))
        {
            var success = int.TryParse(line[rightIndex].ToString(), out int result);
            if (success)
            {
                rightDigit = result;
                break;
            }
            rightIndex--;
        }

        rightDigit = rightDigit == -1 ? numbers.IndexOf(right.word) + 1 : rightDigit;

        return 10 * leftDigit + rightDigit;
    }

    private static List<int> FindAllOccurrences(string input, string substring)
    {
        List<int> occurrences = new List<int>();
        Regex regex = new Regex(Regex.Escape(substring));
        foreach (Match match in regex.Matches(input))
        {
            occurrences.Add(match.Index); // Capture each position
        }
        return occurrences;
    }
    private static int GetValueIntsOnlyImproved(string line)
    {
        int left = line.FirstOrDefault(char.IsDigit) - '0';
        int right = line.LastOrDefault(char.IsDigit) - '0';
        return 10 * left + right;
    }

    #region Deprecated
    private static int GetValueIntsOnly(string line)
    {
        int leftDigit = -1;
        int rightDigit = -1;
        int leftIndex = 0;
        int rightIndex = line.Length - 1;
        while (true)
        {
            var success = int.TryParse(line[leftIndex].ToString(), out int result);
            if (success)
            {
                leftDigit = result;
                break;
            }
            leftIndex++;
        }

        while (rightIndex >= leftIndex)
        {
            var success = int.TryParse(line[rightIndex].ToString(), out int result);
            if (success)
            {
                rightDigit = result;
                break;
            }
            rightIndex--;
        }
        return 10 * leftDigit + rightDigit;
    }
    #endregion
}
