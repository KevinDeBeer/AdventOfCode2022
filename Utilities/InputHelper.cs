namespace Utilities
{
    public static class InputHelper
    {
        public static IEnumerable<string> GetInput(string fileName)
        {
            string[] files = Directory.GetFiles("../../../Data/");
            string file = files.FirstOrDefault(f => f.Contains(fileName))!;

            if (file == null)
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllLines(file).ToList();
        }
    }
}
