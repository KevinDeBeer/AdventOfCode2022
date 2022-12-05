using Day05.Models;
using Utilities;

IEnumerable<string> data = InputHelper.GetTextInput("input.txt");
string[][] stacks = Array.Empty<string[]>();
List<Instruction> instructions = new();

InstantiateData();
MoveCrates(moveInBatch: false);

string result = string.Empty;

foreach (string[] stack in stacks)
{
    result += stack.FirstOrDefault().Replace("[", string.Empty).Replace("]", string.Empty);
}

Console.WriteLine($"Result: {result}");

void InstantiateData()
{
    IEnumerable<string> currentCrateLines = data.TakeWhile(d => !string.IsNullOrWhiteSpace(d));
    IEnumerable<string> instructionLines = data.Skip(currentCrateLines.Count() + 1);
    int nrOfStacks = currentCrateLines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Max();

    stacks = new string[nrOfStacks][];

    for (int i = 0; i < nrOfStacks; i++)
    {
        string[] stack = GetStack(currentCrateLines.SkipLast(1), i);
        stacks[i] = stack;
    }

    foreach (string line in instructionLines)
    {
        string[] split = line.Split(' ');

        Instruction instruction = new()
        {
            NrToMove = int.Parse(split[1]),
            From = int.Parse(split[3]) - 1,
            To = int.Parse(split[5]) - 1
        };

        instructions.Add(instruction);
    }
}

string[] GetStack(IEnumerable<string> stackLines, int index)
{
    List<string> stacksInRow = new();

    foreach (string stackLine in stackLines)
    {
        string crate = string.Concat(stackLine.Skip(index * 4).Take(3));

        if (!string.IsNullOrWhiteSpace(crate))
        {
            stacksInRow.Add(crate);
        }
    }

    return stacksInRow.ToArray();
}

void MoveCrates(bool moveInBatch = true)
{
    foreach (Instruction instruction in instructions)
    {
        string[] StackToMoveFrom = stacks.ElementAt(instruction.From);
        List<string> StackToMoveTo = stacks.ElementAt(instruction.To).ToList();
        IEnumerable<string> CratesToMove = StackToMoveFrom.Take(instruction.NrToMove);

        StackToMoveTo.InsertRange(0, moveInBatch ? CratesToMove.Reverse() : CratesToMove);

        stacks[instruction.To] = StackToMoveTo.ToArray();
        stacks[instruction.From] = StackToMoveFrom.Skip(instruction.NrToMove).ToArray();
    }
}