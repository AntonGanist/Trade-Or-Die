using System.Collections.Generic;

public class DialogParser
{
    public class ParseResult
    {
        public Dictionary<string, List<string>> Dialogs { get; }
        public Dictionary<string, List<string>> Options { get; }

        public ParseResult(Dictionary<string, List<string>> dialogs, Dictionary<string, List<string>> options)
        {
            Dialogs = dialogs;
            Options = options;
        }
    }

    public ParseResult Parse(string fileContent)
    {
        var dialogDictionary = new Dictionary<string, List<string>>();
        var optionDictionary = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(fileContent))
            return new ParseResult(dialogDictionary, optionDictionary);

        string[] lines = fileContent.Split('\n');

        string currentKnot = string.Empty;
        var currentDialogLines = new List<string>();
        var currentOptionLines = new List<string>();

        bool insideDialogBlock = false;

        foreach (string rawLine in lines)
        {
            if (rawLine == null)
                continue;

            string line = rawLine.Trim();

            if (string.IsNullOrEmpty(line) || line.StartsWith("//"))
                continue;

            if (line.StartsWith("%"))
            {
                SaveCurrentKnot(currentKnot, currentDialogLines, currentOptionLines, dialogDictionary, optionDictionary);

                currentKnot = line.Substring(1).Trim();
                currentDialogLines = new List<string>();
                currentOptionLines = new List<string>();
                insideDialogBlock = false;

                continue;
            }

            if (line.StartsWith("["))
            {
                insideDialogBlock = true;
                continue;
            }

            if (line.StartsWith("]"))
            {
                insideDialogBlock = false;
                continue;
            }

            if (insideDialogBlock)
            {
                if (line.StartsWith("=>"))
                {
                    currentOptionLines.Add(line.Substring(2).Trim());
                }
                else
                {
                    currentDialogLines.Add(line);
                }
            }
        }

        SaveCurrentKnot(currentKnot, currentDialogLines, currentOptionLines, dialogDictionary, optionDictionary);

        return new ParseResult(dialogDictionary, optionDictionary);
    }

    void SaveCurrentKnot(string knot, List<string> dialogLines, List<string> optionLines, 
        Dictionary<string, List<string>> dialogDictionary, Dictionary<string, List<string>> optionDictionary)
    {
        if (string.IsNullOrEmpty(knot))
            return;

        dialogDictionary[knot] = new List<string>(dialogLines);
        optionDictionary[knot] = new List<string>(optionLines);
    }
}