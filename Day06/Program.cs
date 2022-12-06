using Utilities;

const int packetOffset = 4;
const int messageOffset = 14;

IEnumerable<string> data = InputHelper.GetTextInput("input.txt");

foreach (string input in data)
{
    int marker = GetMarkerPosition(input, packetOffset);
    Console.WriteLine($"Packet: {input} - Marker at {marker}");

    marker = GetMarkerPosition(input, messageOffset);
    Console.WriteLine($"Message: {input} - Marker at {marker}");
}

static int GetMarkerPosition(string input, int offset)
{
    for (int i = 0; i < input.Length - offset; i++)
    {
        string cut = input.Substring(i, offset);

        if (cut.Distinct().Count() == offset)
        {
            return i + offset;
        }
    }

    throw new ArithmeticException();
}