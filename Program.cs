using BookScraperTool;
using BookScraperTool.FileOutput;

class EntryPoint
{
    static void Main(string[] args)
    {
        var scraper = new BookCollector();
        var collectedBooks = scraper.CollectBookData();

        Console.WriteLine($"Total books gathered: {collectedBooks.Count}");

        var saver = new CsvWriterService();
        saver.SaveBooksToCsv(collectedBooks, "scraped_books.csv");

        Console.WriteLine("Data saved in 'scraped_books.csv'. Process complete.");
    }
}