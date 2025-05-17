using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BookScraperTool.Models;

namespace BookScraperTool;

public class BookCollector
{
    private ChromeDriver browser;
    private List<BookDetails> results = new();

    public BookCollector()
    {
        var setup = new ChromeOptions();
        setup.AddArgument("--headless");
        setup.AddArgument("--disable-gpu");

        browser = new ChromeDriver(setup);
    }

    public List<BookDetails> CollectBookData()
    {
        string pagePrefix = "https://books.toscrape.com/catalogue/page-";
        int pageNumber = 1;

        while (true)
        {
            string currentPage = $"{pagePrefix}{pageNumber}.html";
            browser.Navigate().GoToUrl(currentPage);

            var productCards = browser.FindElements(By.CssSelector(".product_pod"));
            if (productCards.Count == 0)
            {
                Console.WriteLine($"Page {pageNumber} has no items. Ending loop.");
                break;
            }

            foreach (var card in productCards)
            {
                var titleElement = card.FindElement(By.TagName("h3"));
                var priceElement = card.FindElement(By.ClassName("price_color"));
                var stockElement = card.FindElement(By.ClassName("availability"));
                var linkElement = card.FindElement(By.TagName("a"));

                string title = titleElement?.Text ?? "Unknown";
                string price = priceElement?.Text ?? "$0.00";
                string stock = stockElement?.Text.Trim() ?? "Unknown";
                string link = linkElement?.GetAttribute("href") ?? "#";

                results.Add(new BookDetails
                {
                    Title = title,
                    Cost = price,
                    StockInfo = stock,
                    DetailLink = link
                });
            }

            Console.WriteLine($"Scraped page {pageNumber} with {productCards.Count} books.");
            pageNumber++;
        }

        browser.Quit();
        return results;
    }
}