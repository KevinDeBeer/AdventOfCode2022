namespace Utilities
{
    public static class InputHelper
    {
        public static string GetSingleLine(string fileName)
        {
            string[] lines = GetLines(fileName);

            return lines.Single();
        }

        public static IEnumerable<string> GetTextInput(string fileName)
        {
            string[] lines = GetLines(fileName);

            return lines;
        }

        public static List<int> GetNumericInput(string fileName)
        {
            string[] lines = GetLines(fileName);

            List<int> result = lines.Select(int.Parse).ToList();

            return result;
        }

        private static string[] GetLines(string fileName)
        {
            string[] files = Directory.GetFiles("../../../Data/");
            string file = files.FirstOrDefault(f => f.Contains(fileName))!;

            if (file == null)
            {
                throw new FileNotFoundException();
            }

            string[] lines = File.ReadAllLines(file);

            return lines;
        }
    }
}