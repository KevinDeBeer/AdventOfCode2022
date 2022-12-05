using System.Diagnostics;
using Utilities;

List<string> data = InputHelper.GetTextInput("input.txt").ToList();

HandleData(false);
HandleData(true);

void HandleData(bool moveInBatch = false)
{
    Stopwatch stopwatch = Stopwatch.StartNew();

    List<string> fileHeader = data.TakeWhile(d => !string.IsNullOrWhiteSpace(d)).ToList();
    List<string> crateColumns = fileHeader.SkipLast(1).ToList();
    int nrOfStacks = fileHeader.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max();

    List<List<string>> stacks = new();

    for (int i = 0; i < nrOfStacks; i++)
    {
        List<string> column = new();

        foreach (string crateColumn in crateColumns)
        {
            string crate = crateColumn.Substring(i * 4, 3);

            if (!string.IsNullOrWhiteSpace(crate))
            {
                column.Add(crate);
            }
        }

        stacks.Add(column);
    }

    foreach (string instruction in data.Skip(fileHeader.Count + 1).ToList())
    {
        string[] split = instruction.Split(' ');
        MoveCrates(stacks, int.Parse(split[1]), int.Parse(split[3]) - 1, int.Parse(split[5]) - 1, moveInBatch);
    }

    stopwatch.Stop();
    Console.Write($"Result: {string.Join("", stacks.Select(s => s.First().Replace("[", "").Replace("]", "")))}, in {stopwatch.Elapsed}\n");
}

void MoveCrates(List<List<string>> stacks, int nrToMove, int moveFrom, int moveTo, bool moveInBatch)
{
    int nrOfMovingCrates = moveInBatch ? nrToMove : 1;

    for (int i = 0; i < (moveInBatch ? 1 : nrToMove); i++)
    {
        IEnumerable<string> StackToMoveFrom = stacks[moveFrom];
        List<string> StackToMoveTo = stacks[moveTo];

        StackToMoveTo.InsertRange(0, StackToMoveFrom.Take(nrOfMovingCrates));

        stacks[moveTo] = StackToMoveTo;
        stacks[moveFrom] = StackToMoveFrom.Skip(nrOfMovingCrates).ToList();
    }
}