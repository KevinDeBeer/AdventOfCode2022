using Utilities;

const int UpperOffset = 26;
const int AlphabeticOffset = 32;
const int GroupSize = 3;

string[] data = InputHelper.GetTextInput("input.txt").ToArray();
(int itemsValue, int badgeValues) = GetItems(data);
Console.WriteLine($"Found items: {itemsValue}, badge values: {badgeValues}");

(int, int) GetItems(string[] input)
{
    int itemsValue = 0, badgeValue = 0;

    for (int i = 0; i < input.Length; i++)
    {
        char item = GetItem(input[i]);
        itemsValue += GetCharValue(item);

        if (i % 3 == 0)
        {
            IEnumerable<string> nextThreeItems = input.Skip(i).Take(GroupSize);
            badgeValue += GetBadgeValue(nextThreeItems);
        }
    }

    return (itemsValue, badgeValue);
}

int GetBadgeValue(IEnumerable<string> input)
{
    List<char> valuesInBoth = input.ElementAt(0).ToList();

    foreach (string item in input.Skip(1))
    {
        valuesInBoth = valuesInBoth.Intersect(item).ToList();
    }

    return GetCharValue(valuesInBoth.Single());
}

char GetItem(string item)
{
    string firstHalf = item[..(item.Length / 2)];
    string secondHalf = item[(item.Length / 2)..];

    return firstHalf.Intersect(secondHalf).Single();
}

int GetCharValue(char c)
{
    int value = c % AlphabeticOffset;
    value = char.IsUpper(c) ? value + UpperOffset : value;

    return value;
}