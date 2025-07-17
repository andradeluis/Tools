using System.Text.RegularExpressions;

namespace XMLModifier;

class Program
{
	static void Main(string[] args)
	{
		// Params: FolderPath, FileExtension
		Console.WriteLine("Following program allows you to modify XML need for Nagarro");

		// string inputPath = "input.xml"; // Change to your file path

		Console.WriteLine("Type the file path:");
		string? filePath = Console.ReadLine();
		if (string.IsNullOrEmpty(filePath))
		{
			Console.WriteLine($"Invalid file path: ({filePath})");
			throw new ArgumentNullException(nameof(filePath));
		}

		Console.WriteLine("Type the file name:");
		string? fileName = Console.ReadLine();
		if (string.IsNullOrEmpty(fileName))
		{
			Console.WriteLine($"Invalid file path: ({fileName})");
			throw new ArgumentNullException(nameof(fileName));
		}

		string inputPath = Path.Combine(filePath, fileName);

		string[] lines = File.ReadAllLines(inputPath);
		List<string> outputLines = new();

		Regex referenceRegex = new(@"<Reference Include=""(?<name>[^,""]+), Version=(?<version>[^,""]+),.*?>", RegexOptions.Compiled);
		bool insideReference = false;

		foreach (var line in lines)
		{
			if (!insideReference)
			{
				var match = referenceRegex.Match(line);
				if (match.Success)
				{
					string name = match.Groups["name"].Value;
					string version = match.Groups["version"].Value;
					outputLines.Add($"<PackageReference Include=\"{name}\" Version=\"{version}\" />");
					insideReference = true; // Skip next lines until </PackageReference>
				}
				else
				{
					outputLines.Add(line);
				}
			}
			else if (line.Trim().Contains("</PackageReference>"))
			{
				insideReference = false; // Resume adding lines after skipping block
			}
		}

		string outputPath = Path.Combine(filePath, $"new{fileName}");
		// Overwrite file
		File.WriteAllLines(outputPath, outputLines);

		Console.WriteLine("Conversion completed. Type any key to finish.");
		Console.ReadLine();
	}

}
