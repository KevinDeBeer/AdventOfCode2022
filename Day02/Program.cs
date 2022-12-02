using Day02.Helpers;
using Utilities;

const int charOffset = 23;
int regularScore = 0;
int decideOutcomeScore = 0;

IEnumerable<string> input = InputHelper.GetInput("input.txt");

IEnumerable<char[]> games = input.Select(line =>
{
    string[] inputMoves = line.Split(' ');
    char[] mappedMoves = inputMoves.Select(m =>
    {
        char parsed = char.Parse(m);
        return parsed <= 67 ? parsed : (char)(parsed - charOffset);
    }).ToArray();

    return mappedMoves;
}).ToList();

foreach (char[] game in games)
{
    regularScore += ScoreHelper.GetGameScore(game);
    decideOutcomeScore += ScoreHelper.GetGameScore(game, true);
}

Console.WriteLine($"Regular score: {regularScore} \nDecided score: {decideOutcomeScore}");
