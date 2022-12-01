using Day01.Models;

IEnumerable<int> sums = Input.Data.Aggregate(new List<List<int>> { new List<int>() },
    (list, value) =>
    {
        if (int.TryParse(value, out int result))
        {
            list.Last().Add(result);
        }
        else
        {
            list.Add(new List<int>());
        }

        return list;
    }).Select(l => l.Sum());

Console.WriteLine($"Most Calories: {sums.Max()}");
Console.WriteLine($"Top 3 calories sum: {sums.OrderByDescending(s => s).Take(3).Sum()}");
