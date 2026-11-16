using System;
using System.Collections.Generic;
using System.IO;

class Poem
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }
    public string Text { get; set; } = "";
    public string Theme { get; set; } = "";
}

class Reports
{
    public static string MakeReport(List<Poem> poems,
        string reportType, //тип записи
        string searchWord)
    {
        List<Poem> result = new List<Poem>();

        if (reportType == "4")
        {
            for (int i = 0; i < poems.Count; i++)
            {
                if (poems[i].Text.IndexOf(
                    searchWord,
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(poems[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < poems.Count; i++)
            {
                result.Add(poems[i]);
            }
            
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < result.Count - 1 - i; j++)
                {
                    if (ComparePoems(result[j], result[j + 1], reportType) > 0)
                    {
                        Poem temporaryPoem = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temporaryPoem;
                    }
                }
            }
        }
        
        string report = "";

        for (int i = 0; i < result.Count; i++)
        {
            report += "Title: " + result[i].Title + "\n";
            report += "Author: " + result[i].Author + "\n";
            report += "Year: " + result[i].Year + "\n";
            report += "Theme: " + result[i].Theme + "\n";
            report += "Word count: " + GetWordCount(result[i].Text) + "\n";
            report += "Text: " + result[i].Text + "\n";
            report += "-----------------------------\n";
        }

        if (result.Count == 0)
        {
            report = "No poems were found.";
        }

        return report;
    }

    private static int ComparePoems(
        Poem firstPoem,
        Poem secondPoem,
        string reportType)
    {
        if (reportType == "1")
        {
            return String.Compare(
                firstPoem.Title,
                secondPoem.Title,
                StringComparison.OrdinalIgnoreCase);
        }
        else if (reportType == "2")
        {
            return String.Compare(
                firstPoem.Author,
                secondPoem.Author,
                StringComparison.OrdinalIgnoreCase);
        }
        else if (reportType == "3")
        {
            return String.Compare(
                firstPoem.Theme,
                secondPoem.Theme,
                StringComparison.OrdinalIgnoreCase);
        }
        else if (reportType == "5")
        {
            return firstPoem.Year.CompareTo(secondPoem.Year);
        }
        else if (reportType == "6")
        {
            int firstLength = GetWordCount(firstPoem.Text);
            int secondLength = GetWordCount(secondPoem.Text);

            return firstLength.CompareTo(secondLength);
        }

        return 0;
    }

    public static int GetWordCount(string text)
    {
        char[] separators =
        {
            ' ', '\n', '\r', '\t', ',', '.', '!', '?', ':', ';'
        };

        string[] words = text.Split(
            separators,
            StringSplitOptions.RemoveEmptyEntries);

        return words.Length;
    }

    public static void ShowOrSave(string report)
    {
        Console.WriteLine("1. Show the report on screen");
        Console.WriteLine("2. Save the report to a file");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine() ?? "";

        if (choice == "1")
        {
            Console.WriteLine();
            Console.WriteLine(report);
        }
        else if (choice == "2")
        {
            Console.Write("Enter the file name, for example report.txt: ");
            string fileName = Console.ReadLine() ?? "";

            File.WriteAllText(fileName, report);
            Console.WriteLine("The report was saved.");
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }
}

class Program
{
    static void Main()
    {
        //2
        List<Poem> poems = new List<Poem>
        {
            new Poem
            {
                Title = "The Road Not Taken",
                Author = "Robert Frost",
                Year = 1916,
                Theme = "Choices",
                Text = "Two roads diverged in a yellow wood."
            },
            new Poem
            {
                Title = "Hope",
                Author = "Emily Dickinson",
                Year = 1861,
                Theme = "Hope",
                Text = "Hope is the thing with feathers."
            }
        };

        Console.WriteLine("Choose a report:");
        Console.WriteLine("1. By poem title");
        Console.WriteLine("2. By author");
        Console.WriteLine("3. By theme");
        Console.WriteLine("4. By a word in the poem text");
        Console.WriteLine("5. By year written");
        Console.WriteLine("6. By poem length");
        Console.Write("Your choice: ");

        string reportType = Console.ReadLine() ?? "";
        string searchWord = "";

        if (reportType == "4")
        {
            Console.Write("Enter a word to search for: ");
            searchWord = Console.ReadLine() ?? "";
        }

        string report = Reports.MakeReport(poems, reportType, searchWord);
        Reports.ShowOrSave(report);
    }
}