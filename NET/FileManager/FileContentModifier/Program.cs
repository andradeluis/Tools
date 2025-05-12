using System.Runtime.CompilerServices;

namespace FileContentModifier;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            Console.WriteLine("Enter full file path:");
            var fullFilePath = Console.ReadLine();

            var (filePath, fileName) = GetFolderAndFilenames(fullFilePath);

            var lines = File.ReadAllLines(fullFilePath).ToList();
            var running = true;

            while (running)
            {
                Console.WriteLine("\nChoose an operation:");
                Console.WriteLine("1. Add string to end of each line");
                Console.WriteLine("2. Add string to beginning of each line");
                Console.WriteLine("3. Add string after a specific string");
                Console.WriteLine("4. Add string before a specific string");
                Console.WriteLine("5. Delete N chars from beginning of each line");
                Console.WriteLine("6. Delete N chars from end of each line");
                Console.WriteLine("7. Insert N chars at beginning of each line");
                Console.WriteLine("8. Insert N chars at end of each line");
                Console.WriteLine("9. Trim each line");
                Console.WriteLine("10. Remove emtpy lines");
                Console.WriteLine("11. Replace string");
                Console.Write("Enter option (1-11): ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Enter string to add at end: ");
                        var end = Console.ReadLine();
                        lines = LineModifier.AddStringToEnd(lines, end);
                        WriteFile(filePath, lines);
                        break;
                    case "2":
                        Console.Write("Enter string to add at beginning: ");
                        var start = Console.ReadLine();
                        lines = LineModifier.AddStringToBeginning(lines, start);
                        WriteFile(filePath, lines);
                        break;
                    case "3":
                        Console.Write("Enter target string: ");
                        var targetAfter = Console.ReadLine();
                        Console.Write("Enter string to insert after target: ");
                        var after = Console.ReadLine();
                        lines = LineModifier.AddStringAfter(lines, targetAfter, after);
                        WriteFile(filePath, lines);
                        break;
                    case "4":
                        Console.Write("Enter target string: ");
                        var targetBefore = Console.ReadLine();
                        Console.Write("Enter string to insert before target: ");
                        var before = Console.ReadLine();
                        lines = LineModifier.AddStringBefore(lines, targetBefore, before);
                        WriteFile(filePath, lines);
                        break;
                    case "5":
                        Console.Write("Enter number of characters to delete from beginning: ");
                        int.TryParse(Console.ReadLine(), out int delStart);
                        lines = LineModifier.DeleteNCharsFromBeginning(lines, delStart);
                        break;
                    case "6":
                        Console.Write("Enter number of characters to delete from end: ");
                        int.TryParse(Console.ReadLine(), out int delEnd);
                        lines = LineModifier.DeleteNCharsFromEnd(lines, delEnd);
                        WriteFile(filePath, lines);
                        break;
                    case "7":
                        Console.Write("Enter character to insert: ");
                        var charBeg = Console.ReadLine()[0];
                        Console.Write("Enter number of times to insert: ");
                        int.TryParse(Console.ReadLine(), out int nBeg);
                        lines = LineModifier.InsertNCharsAtBeginning(lines, charBeg, nBeg);
                        WriteFile(filePath, lines);
                        break;
                    case "8":
                        Console.Write("Enter character to insert: ");
                        var charEnd = Console.ReadLine()[0];
                        Console.Write("Enter number of times to insert: ");
                        int.TryParse(Console.ReadLine(), out int nEnd);
                        lines = LineModifier.InsertNCharsAtEnd(lines, charEnd, nEnd);
                        WriteFile(filePath, lines);
                        break;
                    case "9":
                        lines = LineModifier.TrimLines(lines);
                        Console.WriteLine("Lines trimmed.");
                        WriteFile(filePath, lines);
                        break;
                    case "10":
                        lines = LineModifier.RemoveEmptyLines(lines);
                        Console.WriteLine("Empty lines removed.");
                        WriteFile(filePath, lines);
                        break;
                    case "11":
                        Console.Write("Enter string to replace: ");
                        var oldVal = Console.ReadLine();
                        Console.Write("Enter replacement string: ");
                        var newVal = Console.ReadLine();
                        lines = LineModifier.ReplaceString(lines, oldVal, newVal);
                        Console.WriteLine("String replacement completed.");
                        WriteFile(filePath, lines);
                    break;
                    default:
                        Console.WriteLine("Invalid option.");
                        running = false;
                        break;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static void WriteFile(string filePath, List<string> lines)
    {
        var outputFilename = "OutputFile.txt";
        var outputFilePath = Path.Combine(filePath, outputFilename);
        File.WriteAllLines(outputFilePath, lines);
        Console.WriteLine($"Modified file saved as: {outputFilePath}");
    }

    private static (string, string) GetFolderAndFilenames(string? fullFilePath)
    {
        if (string.IsNullOrWhiteSpace(fullFilePath))
        {
            Console.WriteLine($"Invalid full File Path path: ({fullFilePath})");
            throw new ArgumentNullException(nameof(fullFilePath));
        }

        if (!File.Exists(fullFilePath))
        {
            Console.WriteLine($"File {fullFilePath} doesn't exists.");
            throw new Exception($"File {fullFilePath} doesn't exists.");
        }

        var folderPath = Path.GetDirectoryName(fullFilePath);
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            Console.WriteLine($"Invalid folder path: ({folderPath})");
            throw new ArgumentNullException(nameof(folderPath));
        }
        
        var fileName = Path.GetFileName(fullFilePath);
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine($"Invalid file name: ({fileName})");
            throw new ArgumentNullException(nameof(fileName));
        }

        return (folderPath.ToString(), fileName.ToString());
    }
}
