using Utilities;
const bool moveInBatch = true;

IEnumerable<string> data = InputHelper.GetTextInput("input.txt");
IEnumerable<string> fileHeader = data.TakeWhile(d => !string.IsNullOrWhiteSpace(d));
IEnumerable<string> instructions = data.Skip(fileHeader.Count() + 1);
string[] crateColumns = fileHeader.SkipLast(1).ToArray();
int nrOfStacks = fileHeader.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max();

string[][] stacks = new string[nrOfStacks][];

for (int i = 0; i < nrOfStacks; i++)
{
    List<string> stacksInRow = new();

    for (int j = 0; j < crateColumns.Length; j++)
    {
        string crate = crateColumns[j].Substring(i * 4, 3);

        if (!string.IsNullOrWhiteSpace(crate))
        {
            stacksInRow.Add(crate);
        }
    }

    stacks[i] = stacksInRow.ToArray();
}

foreach (string line in instructions)
{
    string[] split = line.Split(' ');
    MoveCrates(int.Parse(split[1]), int.Parse(split[3]) - 1, int.Parse(split[5]) - 1);
}

Console.Write($"Result: {string.Join("", stacks.Select(s => s.First().Replace("[", "").Replace("]", "")))}");

void MoveCrates(int nrToMove, int moveFrom, int moveTo)
{
    int nrOfMovingCrates = moveInBatch ? nrToMove : 1;

    for (int i = 0; i < (moveInBatch ? 1 : nrToMove); i++)
    {
        string[] StackToMoveFrom = stacks[moveFrom];
        List<string> StackToMoveTo = stacks[moveTo].ToList();

        StackToMoveTo.InsertRange(0, StackToMoveFrom.Take(nrOfMovingCrates));

        stacks[moveTo] = StackToMoveTo.ToArray();
        stacks[moveFrom] = StackToMoveFrom.Skip(nrOfMovingCrates).ToArray();
    }
}