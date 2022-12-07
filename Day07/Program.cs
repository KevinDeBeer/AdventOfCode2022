using Day07;
using Day07.Models;
using Utilities;

const int systemSize = 70000000;
const int updateSize = 30000000;
const int folderSizeLimit = 100000;
int sumOfFoldersWithinSizeLimit = 0;

IEnumerable<string> input = InputHelper.GetTextInput("input.txt");

Folder root = new();
Folder currentFolder = root;
int fileSizeToDelete = systemSize;

do
{
    ProcessNextCommand();
} while (input.Any());

AnalyzeFolderData(root);

Console.WriteLine($"Sum of folders: {sumOfFoldersWithinSizeLimit}");
Console.WriteLine($"Folder size to delete: {fileSizeToDelete}");

void AnalyzeFolderData(Folder folder)
{
    foreach (Folder subFolder in folder.Folders)
    {
        AnalyzeFolderData(subFolder);
    }

    if (folder.Size < folderSizeLimit)
    {
        sumOfFoldersWithinSizeLimit += folder.Size;
    }

    if (systemSize - root.Size + folder.Size > updateSize && folder.Size < fileSizeToDelete)
    {
        fileSizeToDelete = folder.Size;
    }
}

void ProcessNextCommand()
{
    Command command = GetCommand(input.First());

    switch (command.CommandType)
    {
        case CommandType.CD:
            ProcessCD(command);
            break;
        case CommandType.LS:
            ProcessLS();
            break;
        default:
            throw new InvalidOperationException();
    }

    input = input.Skip(1);
}

void ProcessCD(Command command)
{
    switch (command.Value)
    {
        case "/":
            currentFolder = root;
            break;
        case "..":
            currentFolder = currentFolder.Parent!;
            break;
        default:
            currentFolder = currentFolder.Folders.First(f => f.Name == command.Value);
            break;
    }
}

void ProcessLS()
{
    IEnumerable<string> resultOfLS = input.Skip(1).TakeWhile(l => !(l[..1] == "$"));

    foreach (string line in resultOfLS)
    {
        string[] split = line.Split(" ");

        if (split[0] == "dir")
        {
            currentFolder.AddFolder(split[1]);
        }
        else
        {
            currentFolder.AddFile(split[1], int.Parse(split[0]));
        }
    }

    input = input.Skip(resultOfLS.Count());
}

Command GetCommand(string line)
{
    string[] split = line.Split(" ");
    CommandType commandType = split[1] == "cd" ? CommandType.CD : CommandType.LS;

    Command command = new()
    {
        CommandType = commandType,
        Value = commandType == CommandType.CD ? split[2] : string.Empty
    };

    return command;
}