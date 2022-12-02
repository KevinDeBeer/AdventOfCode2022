using Utilities;

IEnumerable<string> input = InputHelper.GetInput("input.txt");

IEnumerable<int> sums = input.Aggregate(new List<List<int>> { new List<int>() },
    (elves, calories) =>
    {
        if (int.TryParse(calories, out int result))
        {
            elves.Last().Add(result);
        }
        else
        {
            elves.Add(new List<int>());
        }

        return elves;
    })
    .Select(l => l.Sum())
    .OrderByDescending(s => s);

Console.WriteLine($"Most Calories: {sums.First()}");
Console.WriteLine($"Top 3 calories sum: {sums.Take(3).Sum()}");
