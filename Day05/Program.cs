using Utilities;
const bool moveInBatch = false;

IEnumerable<string> data = InputHelper.GetTextInput("input.txt");
IEnumerable<string> crateLines = data.TakeWhile(d => !string.IsNullOrWhiteSpace(d));
IEnumerable<string> instructionLines = data.Skip(crateLines.Count() + 1);
int nrOfStacks = crateLines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max();

string[][] stacks = new string[nrOfStacks][];

for (int i = 0; i < nrOfStacks; i++)
{
    stacks[i] = CreateStack(crateLines.SkipLast(1), i);
}

foreach (string line in instructionLines)
{
    string[] split = line.Split(' ');

    int nrToMove = int.Parse(split[1]);
    int moveFrom = int.Parse(split[2]);
    int moveTo = int.Parse(split[3]);

    MoveCrates(moveFrom, moveTo, nrToMove, moveInBatch);
}

Console.Write("Result: ");

foreach (string[] stack in stacks)
{
    Console.Write(stack.First().Replace("[", string.Empty).Replace("]", string.Empty));
}

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

void MoveCrates(int moveFrom, int moveTo, int nrToMove, bool moveInBatch = true)
{
    string[] StackToMoveFrom = stacks.ElementAt(moveFrom);
    List<string> StackToMoveTo = stacks.ElementAt(moveTo).ToList();
    IEnumerable<string> CratesToMove = StackToMoveFrom.Take(nrToMove);

    StackToMoveTo.InsertRange(0, moveInBatch ? CratesToMove.Reverse() : CratesToMove);

    stacks[moveTo] = StackToMoveTo.ToArray();
    stacks[moveFrom] = StackToMoveFrom.Skip(nrToMove).ToArray();
}