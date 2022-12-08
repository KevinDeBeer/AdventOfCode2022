namespace Day08.Models
{
    internal class Tree
    {
        public int Height { get; set; } = 0;
        public bool IsVisible { get; set; } = true;

        public override string ToString()
        {
            return $"H: {Height}, V: {IsVisible}";
        }
    }
}
