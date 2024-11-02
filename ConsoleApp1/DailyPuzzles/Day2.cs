namespace AdventOfCode2023.DailyPuzzles;

internal class Day2
{
    private static Dictionary<string, int> MarbleLimits = new Dictionary<string, int>()
    {
        {"red", 12 },
        {"green", 13 },
        {"blue", 14 }
    };

    public static void Part1(List<string> lines)
    {
        int total = 0;

        foreach (var line in lines)
        {
            total += GetGameIndexIfPossibleElseZero(line);
        }

        Console.WriteLine(total);
    }

    public static void Part2(List<string> lines)
    {
        int total = 0;

        foreach (var line in lines)
        {
            total += GetCubePower(line);
        }

        Console.WriteLine(total);
    }

    private static int GetCubePower(string line)
    {
        var firstSplit = line.Split(':');
        var games = firstSplit[1].Split(';');
        Dictionary<string, int> minValues = new Dictionary<string, int>();

        foreach (var game in games)
        {
            var cubeCount = game.Split(",");
            foreach (var cube in cubeCount)
            {
                var count = cube.Split(" ");
                int.TryParse(count[1], out int numOfCubes);
                if (minValues.ContainsKey(count[2]))
                {
                    minValues[count[2]] = Math.Max(numOfCubes, minValues[count[2]]);
                }
                else
                {
                    minValues[count[2]] = numOfCubes;
                }
            }
        }
        return minValues.Values.Aggregate(1, (acc, val) => acc * val);
    }
    private static int GetGameIndexIfPossibleElseZero(string line)
    {
        var firstSplit = line.Split(':');
        int.TryParse(firstSplit[0].Split(' ')[1], out int index);
        var games = firstSplit[1].Split(';');
        foreach (var game in games)
        {
            var cubeCount = game.Split(",");
            foreach (var cube in cubeCount)
            {
                var count = cube.Split(" ");
                int.TryParse(count[1], out int numOfCubes);
                if (numOfCubes > MarbleLimits[count[2]]) return 0;
            }
        }
        return index;
    }
}
