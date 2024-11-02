using AdventOfCode2023.DailyPuzzles;

public class Program
{

    public static void Main(string[] args)
    {
        var lines = getInput();
        //Day1.Part1(lines);
        //Day1.Part2(lines);
        //Day2.Part1(lines);
        Day2.Part2(lines);
    }

    private static List<string> getInput()
    {
        var lines = new List<string>();
        string input;

        while(true)
        {
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) break;
            lines.Add(input);
        }
        return lines;
    }
}