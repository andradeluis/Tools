namespace FileContentModifier;
public static class LineModifier
{
    public static List<string> AddStringToEnd(List<string> lines, string toAdd) =>
        lines.Select(line => line + toAdd).ToList();

    public static List<string> AddStringToBeginning(List<string> lines, string toAdd) =>
        lines.Select(line => toAdd + line).ToList();

    public static List<string> AddStringAfter(List<string> lines, string target, string toAdd) =>
        lines.Select(line => line.Contains(target) ? line.Replace(target, target + toAdd) : line).ToList();

    public static List<string> AddStringBefore(List<string> lines, string target, string toAdd) =>
        lines.Select(line => line.Contains(target) ? line.Replace(target, toAdd + target) : line).ToList();

    public static List<string> DeleteNCharsFromBeginning(List<string> lines, int count) =>
        lines.Select(line => line.Length > count ? line.Substring(count) : "").ToList();

    public static List<string> DeleteNCharsFromEnd(List<string> lines, int count) =>
        lines.Select(line => line.Length > count ? line.Substring(0, line.Length - count) : "").ToList();

    public static List<string> InsertNCharsAtBeginning(List<string> lines, char c, int count) =>
        lines.Select(line => new string(c, count) + line).ToList();

    public static List<string> InsertNCharsAtEnd(List<string> lines, char c, int count) =>
        lines.Select(line => line + new string(c, count)).ToList();

    public static List<string> TrimLines(List<string> lines) =>
        lines.Select(line => line.Trim()).ToList();

    public static List<string> RemoveEmptyLines(List<string> lines) =>
        lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

    public static List<string> ReplaceString(List<string> lines, string oldValue, string newValue) =>
        lines.Select(line => line.Replace(oldValue, newValue)).ToList();
}