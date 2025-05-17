using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using BookScraperTool.Models;


namespace BookScraperTool.FileOutput;


public class ExportService
{
    public void SaveBooksToCsv(List<BookDetails> bookList, string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, config);
        csv.WriteRecords(bookList);

    }
}
