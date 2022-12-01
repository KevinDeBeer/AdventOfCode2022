// See https://aka.ms/new-console-template for more information
using Day01.Models;

AnalyzeData();

void AnalyzeData()
{
    IEnumerable<string> input = Input.Data.ToList();
    List<int> sums = new();
    int listIndex = 0;

    do
    {
        int totalCalories = GetCaloriesForGnome(input, out listIndex);

        sums.Add(totalCalories);

        input = input.Skip(listIndex + 1);
    } while (input.Any() && listIndex > 0);

    int mostCalories = sums.Max();
    int top3Calories = sums.OrderByDescending(s => s).Take(3).Sum();
}

int GetCaloriesForGnome(IEnumerable<string> toSearch, out int newIndex)
{
    newIndex = toSearch.ToList().IndexOf(string.Empty);

    IEnumerable<string> calories = toSearch.Take(newIndex);

    int totalCalories = calories.Select(x => Convert.ToInt32(x)).Sum();

    return totalCalories;
}