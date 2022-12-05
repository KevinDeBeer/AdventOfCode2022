using Utilities;
const bool moveInBatch = true;

IEnumerable<string> data = InputHelper.GetTextInput("input.txt");
IEnumerable<string> fileHeader = data.TakeWhile(d => !string.IsNullOrWhiteSpace(d));
IEnumerable<string> crates = fileHeader.SkipLast(1);
IEnumerable<string> instructions = data.Skip(fileHeader.Count() + 1);
int nrOfStacks = fileHeader.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max();

string[][] stacks = new string[nrOfStacks][];

for (int i = 0; i < nrOfStacks; i++)
{
    stacks[i] = CreateStack(crates, i);
}

foreach (string line in instructions)
{
    string[] split = line.Split(' ');
    MoveCrates(int.Parse(split[1]), int.Parse(split[3]) - 1, int.Parse(split[5]) - 1);
}

Console.Write($"Result: {string.Join("", stacks.Select(s => s.First().Replace("[", "").Replace("]", "")))}");

string[] CreateStack(IEnumerable<string> stackLines, int stackIndex)
{
    List<string> stacksInRow = new();

    foreach (string stackLine in stackLines)
    {
        string crate = stackLine.Substring(stackIndex * 4, 3);

        if (!string.IsNullOrWhiteSpace(crate))
        {
            stacksInRow.Add(crate);
        }
    }

    return stacksInRow.ToArray();
}

void MoveCrates(int nrToMove, int moveFrom, int moveTo)
{
    int nrOfMovingCrates = moveInBatch ? nrToMove : 1;

    for (int i = 0; i < (moveInBatch ? 1 : nrToMove); i++)
    {
        string[] StackToMoveFrom = stacks.ElementAt(moveFrom);
        List<string> StackToMoveTo = stacks.ElementAt(moveTo).ToList();
        IEnumerable<string> CratesToMove = StackToMoveFrom.Take(nrOfMovingCrates);

        StackToMoveTo.InsertRange(0, CratesToMove);

        stacks[moveTo] = StackToMoveTo.ToArray();
        stacks[moveFrom] = StackToMoveFrom.Skip(nrOfMovingCrates).ToArray();
    }
}