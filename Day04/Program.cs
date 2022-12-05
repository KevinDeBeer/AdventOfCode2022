using Utilities;

IEnumerable<string> data = InputHelper.GetTextInput("input.txt");
int[][] elves = new int[data.Count()][];
CreateElves();
CountSectionOverlap();

void CreateElves()
{
    for (int i = 0; i < data.Count(); i++)
    {
        string[] splitElves = data.ElementAt(i).Split(',');
        string[] firstElf = splitElves[0].Split("-");
        string[] secondElf = splitElves[1].Split("-");

        elves[i] = new int[] {
            int.Parse(firstElf[0]),
            int.Parse(firstElf[1]),
            int.Parse(secondElf[0]),
            int.Parse(secondElf[1]),
        };
    }
}

void CountSectionOverlap()
{
    int containedSections = 0, totalOverlapping = 0;

    foreach (int[] pair in elves)
    {
        if (pair[1] <= pair[3] && pair[0] >= pair[2] || pair[3] <= pair[1] && pair[2] >= pair[0])
        {
            containedSections += 1;
        }

        totalOverlapping = Math.Max(0, Math.Min(pair[1], pair[3]) - Math.Max(pair[0], pair[2]) + 1) > 0 ? totalOverlapping + 1 : totalOverlapping;
    }

    Console.WriteLine($"Contained sections count: {containedSections} \nTotal overlapping: {totalOverlapping}");
}