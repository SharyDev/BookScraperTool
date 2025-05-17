using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using BookScraperTool.Models;

namespace BookScraperTool.FileOutput;

public class CsvWriterService
{
    public void SaveBooksToCsv(List<BookDetails> bookList, string fileLocation)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using var file = new StreamWriter(fileLocation);
        using var csv = new CsvWriter(file, csvConfig);
        csv.WriteRecords(bookList);
    }
}
