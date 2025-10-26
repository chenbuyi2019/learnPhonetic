using CsvHelper;
using CsvHelper.Configuration;
using SearchPhonetic;
using System.Globalization;

internal class Program
{

    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Environment.CurrentDirectory = AppContext.BaseDirectory;

        Console.WriteLine($"请下载 https://github.com/skywind3000/ECDICT 然后解压 {edictFileName} 到当前文件夹");
        Console.WriteLine($"开始导入 {edictFileName} ...");
        var edictData = ImportData();
        Console.WriteLine($"导入完成。 数量： {edictData.Count}");

        var basicWords = File.ReadAllLines("basic_words.txt");
        Console.WriteLine($"导入 basic_words 完成。 数量： {basicWords.Length}");

        foreach (var n in edictData)
        {
            if (basicWords.Contains(n.word))
            {
                n.Tags.Add("basic");
            }
        }

        var outputPath = "data.ts";
        using var stream = File.Create(outputPath);
        using var writer = new StreamWriter(stream);
        writer.WriteLine("// 此文件是从 SearchPhonetic 生成的");
        writer.WriteLine();
        writer.WriteLine();
        writer.WriteLine("function ImportWordsData() {");
        int count = 0;
        foreach (var n in edictData)
        {
            if (n.Tags.Any(x => collectTags.Contains(x)))
            {
                writer.Flush();
                count += 1;
                writer.Write("AddWord( ");
                writer.Write(QuoteWord(n.word));
                writer.Write(", ");
                writer.Write(QuoteWord(n.phonetic));
                writer.Write(", ");
                writer.Write(QuoteWordArray(n.Tags));
                writer.Write(", ");
                writer.Write(QuoteWord(n.translation));
                writer.Write(") ");
                writer.WriteLine();
            }
        }
        writer.WriteLine("}");
        writer.Flush();
        Console.WriteLine($"输出的单词个数 {count} ");
        Console.WriteLine($"输出完成 {stream.Name}");
    }

    static readonly string[] collectTags = ["basic", "zk", "gk"];

    const string edictFileName = "ecdict.csv";

    static List<WordDictData> ImportData()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture);
        using var reader = new StreamReader(edictFileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<WordDictData>();
        var list = new List<WordDictData>();
        foreach (var n in records)
        {
            n.CheckData();
            list.Add(n);
            if (list.Count % 10000 == 0)
            {
                Console.Write($"{n.word}  ");
            }
        }
        Console.WriteLine();
        return list;
    }

    static string QuoteWord(string s)
    {
        return "`" + s.Replace('`', '_') + "`";
    }

    static string QuoteWordArray(IEnumerable<string> array)
    {
        var a = string.Join(", ", array.Select(x => { return QuoteWord(x); }));
        return $"[{a}]";
    }

}