using System.IO;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            bool shouldWeContinue = true;
            while (shouldWeContinue)
            {
                // File name replacer
                // File name appender
                // File name remover

                // Params: FolderPath, FileExtension
                Console.WriteLine("File Manager. Type the option you want to use: \n1. Replace\n2. Prepend\n3. Remove");
                string? typedValue = Console.ReadLine();

                switch (typedValue)
                {
                    case "1":
                        Replace();
                        break;
                    case "2":
                        Prepend();
                        break;
                    case "3":
                        Remove();
                        break;
                    default:
                        Console.WriteLine($"Invalid option: ({typedValue})");
                        shouldWeContinue = false;
                        break;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Press any key to finish");
            Console.ReadLine();
        }
    }

    private static void Replace()
    {
        string folderPath = GetFolderPath();

        Console.WriteLine("Type files extension:");
        string fileExtension = GetCharacters();

        Console.WriteLine("Type characters to replace from:");
        string oldValue = GetCharacters();

        Console.WriteLine("Type characters to replace to:");
        string newValue = GetCharacters();

        var files = GetFiles(folderPath, $"*{fileExtension}");
        foreach (var file in files)
        {
            string? directory = Path.GetDirectoryName(file);
            if(string.IsNullOrEmpty(directory))
            {
                Console.WriteLine("There was an error getting the file directory");
                continue;
            }
            string oldFileName = Path.GetFileNameWithoutExtension(file);
            string newFileName = $"{oldFileName.Replace(oldValue, newValue)}{fileExtension}";
            string newFilePath = Path.Combine(directory, newFileName);

            File.Move(file, newFilePath);
            Console.WriteLine($"Renamed: {oldFileName} -> {newFileName}");
        }
    }

    private static void Prepend()
    {
        string folderPath = GetFolderPath();

        Console.WriteLine("Type files extension:");
        string fileExtension = GetCharacters();

        Console.WriteLine("Type characters to prepend:");
        string prependValue = GetCharacters();

        var files = GetFiles(folderPath, $"*{fileExtension}");
        foreach (var file in files)
        {
            string? directory = Path.GetDirectoryName(file);
            if (string.IsNullOrEmpty(directory))
            {
                Console.WriteLine("There was an error getting the file directory");
                continue;
            }
            string oldFileName = Path.GetFileNameWithoutExtension(file);
            string newFileName = $"{prependValue}{oldFileName}{fileExtension}";
            string newFilePath = Path.Combine(directory, newFileName);

            System.IO.File.Move(file, newFilePath);
            Console.WriteLine($"Renamed: {oldFileName} -> {newFileName}");
        }
    }

    private static void Remove()
    {
        string folderPath = GetFolderPath();

        Console.WriteLine("Type files extension:");
        string fileExtension = GetCharacters();

        Console.WriteLine("Type characters to remove:");
        string valueToRemove = GetCharacters();

        var files = GetFiles(folderPath, $"*{fileExtension}");
        foreach (var file in files)
        {
            string? directory = Path.GetDirectoryName(file);
            if (string.IsNullOrEmpty(directory))
            {
                Console.WriteLine("There was an error getting the file directory");
                continue;
            }
            string oldFileName = Path.GetFileNameWithoutExtension(file);
            string newFileName = $"{oldFileName.Replace(valueToRemove, string.Empty)}{fileExtension}";
            string newFilePath = Path.Combine(directory, newFileName);

            System.IO.File.Move(file, newFilePath);
            Console.WriteLine($"Renamed: {oldFileName} -> {newFileName}");
        }
    }

    private static string GetFolderPath()
    {
        Console.WriteLine("Type Folder Path:");
        string? folderPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            Console.WriteLine($"Invalid folder path: ({folderPath})");
            throw new ArgumentNullException(nameof(folderPath));
        }
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder {folderPath} doesn't exists.");
            throw new Exception($"Folder {folderPath} doesn't exists.");
        }
        return folderPath.ToString();
    }

    private static string GetCharacters()
    {
        string? characters = Console.ReadLine();
        if (characters is null)
        {
            Console.WriteLine($"Invalid characters: ({characters})");
            throw new ArgumentNullException(nameof(characters));
        }
        return characters.ToString();
    }

    private static string[] GetFiles(string folderPath, string fileExtension)
    {
        var files = Directory.GetFiles(folderPath, $"*{fileExtension}");
        if (files == null)
        {
            Console.WriteLine($"Files not found");
            Console.WriteLine($"Folder path: {folderPath}");
            Console.WriteLine($"File Extension: {fileExtension}");
            throw new Exception();
        }
        return files.ToArray();
    }
}