using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSearcher;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter directory path: ");
        var directory = Console.ReadLine() ?? throw new ArgumentNullException("Directory path is required");

        Console.Write("Enter search words (comma-separated): ");
        var wordsInput = Console.ReadLine() ?? "";
        var searchWords = wordsInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        Console.Write("Enter file extensions (comma-separated, optional): ");
        var extensionsInput = Console.ReadLine() ?? "";
        var extensions = extensionsInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                        .Select(ext => ext.StartsWith('.') ? ext : "." + ext)
                                        .ToList();

        if (!Directory.Exists(directory))
        {
            Console.WriteLine("Directory does not exist.");
            return;
        }

        var results = SearchFiles(directory, searchWords, extensions);

        Console.WriteLine("\nMatches found:");
        foreach (var result in results)
        {
            Console.WriteLine($"{result.FilePath} [Line {result.LineNumber}]: {result.LineText}");
        }

        Console.WriteLine($"\nTotal matches: {results.Count}");
    }

    static List<SearchResult> SearchFiles(string directory, string[] searchWords, List<string> extensions)
    {
        var matchedLines = new List<SearchResult>();

        var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            if (extensions.Count > 0 && !extensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                continue;

            try
            {
                var lines = File.ReadLines(file);
                int lineNumber = 1;
                foreach (var line in lines)
                {
                    if (searchWords.All(word => line.Contains(word, StringComparison.OrdinalIgnoreCase)))
                    {
                        matchedLines.Add(new SearchResult
                        {
                            FilePath = file,
                            LineNumber = lineNumber,
                            LineText = line.Trim()
                        });
                    }
                    lineNumber++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not read file {file}: {ex.Message}");
            }
        }

        return matchedLines;
    }

    class SearchResult
    {
        public string FilePath { get; set; } = "";
        public int LineNumber { get; set; }
        public string LineText { get; set; } = "";
    }
}

