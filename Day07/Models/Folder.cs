namespace Day07.Models
{
    internal class Folder
    {
        public string? Name { get; set; } = "/";
        public int Size { get; private set; }
        public Folder? Parent { get; set; }
        public List<Folder> Folders { get; set; } = new();
        public List<File> Files { get; set; } = new();

        public void AddFolder(string name)
        {
            Folders.Add(new()
            {
                Name = name,
                Parent = this
            });
        }

        public void AddFile(string name, int size)
        {
            Files.Add(new File()
            {
                Name = name,
                Size = size,
            });

            AddFileSize(size);
        }

        private void AddFileSize(int size)
        {
            Size += size;
            Parent?.AddFileSize(size);
        }
    }
}
