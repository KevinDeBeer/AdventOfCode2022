using Day08.Models;
using System.Data;
using Utilities;

IEnumerable<string> input = InputHelper.GetTextInput("testinput.txt");
List<List<Tree>> trees = ParseTrees();

SetTreeVisibilities();

int nrOfVisibleTrees = 0;

foreach (IEnumerable<Tree> tree in trees)
{
    nrOfVisibleTrees += tree.Where(t => t.IsVisible).Count();
}

Console.WriteLine($"counted: {nrOfVisibleTrees} trees");

void SetTreeVisibilities()
{
    for (int i = 1; i < trees.Count - 1; i++)
    {
        for (int j = 1; j < trees.ElementAt(i).Count - 1; j++)
        {
            IEnumerable<Tree> treeline = trees.ElementAt(i);
            Tree tree = trees.ElementAt(i).ElementAt(j);

            SetTreeVisibility(j, tree, treeline);

            if (!tree.IsVisible)
            {
                treeline = trees.Select(t => t.ElementAt(j));
                SetTreeVisibility(i, tree, treeline);
            }
        }
    }
}

void SetTreeVisibility(int position, Tree tree, IEnumerable<Tree> treeLine)
{
    IEnumerable<Tree> before = treeLine.Take(position);

    for (int i = 0; i < before.Count(); i++)
    {
        int heightBefore = before.ElementAt(i).Height;
        tree.IsVisible = heightBefore < tree.Height;

        if (!tree.IsVisible)
        {
            break;
        }
    }

    if (tree.IsVisible)
    {
        return;
    }

    IEnumerable<Tree> after = treeLine.Skip(position + 1);
    for (int i = 0; i < after.Count(); i++)
    {
        int heightAfter = after.ElementAt(i).Height;
        tree.IsVisible = heightAfter < tree.Height;
        if (!tree.IsVisible)
        {
            break;
        }
    }
}

List<List<Tree>> ParseTrees()
{
    List<List<Tree>> trees = new();

    for (int i = 0; i < input.Count(); i++)
    {
        List<Tree> row = input.ElementAt(i).Select(i => new Tree() { Height = int.Parse(i.ToString()) }).ToList();
        trees.Add(row);
    }

    return trees;
}