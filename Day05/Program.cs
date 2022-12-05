using System.Diagnostics;
using Utilities;

IEnumerable<string> data = InputHelper.GetTextInput("input.txt");

HandleData(false);
HandleData(true);

void HandleData(bool moveInBatch)
{
    Stopwatch stopwatch = Stopwatch.StartNew();

    IEnumerable<string> fileHeader = data.TakeWhile(d => !string.IsNullOrWhiteSpace(d));
    int nrOfStacks = fileHeader.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max();

    string[][] stacks = new string[nrOfStacks][];

    for (int i = 0; i < nrOfStacks; i++)
    {
        List<string> column = new();

        foreach (string crateColumn in fileHeader.SkipLast(1))
        {
            string crate = crateColumn.Substring(i * 4, 3);

            if (!string.IsNullOrWhiteSpace(crate))
            {
                column.Add(crate);
            }
        }

        stacks[i] = column.ToArray();
    }

    foreach (string instruction in data.Skip(fileHeader.Count() + 1))
    {
        string[] split = instruction.Split(' ');
        MoveCrates(stacks, int.Parse(split[1]), int.Parse(split[3]) - 1, int.Parse(split[5]) - 1, moveInBatch);
    }

    stopwatch.Stop();
    Console.Write($"Result: {string.Join("", stacks.Select(s => s.First().Replace("[", "").Replace("]", "")))}, in {stopwatch.Elapsed}\n");
}

void MoveCrates(string[][] stacks, int nrToMove, int moveFrom, int moveTo, bool moveInBatch)
{
    int nrOfMovingCrates = moveInBatch ? nrToMove : 1;

    for (int i = 0; i < (moveInBatch ? 1 : nrToMove); i++)
    {
        stacks[moveTo] = stacks[moveFrom][..nrOfMovingCrates].Concat(stacks[moveTo]).ToArray();
        stacks[moveFrom] = stacks[moveFrom][nrOfMovingCrates..];
    }
}