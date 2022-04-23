using System;
using PuppeteerSharp;
using WebScrapping.Models;

namespace WebScrapping
{
    internal class Program
    {
        static readonly string _browser = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

        static readonly object[] urls
            = {
                new UrlModel {
                    Product = "Bloqued Iphone",
                    Url = "https://www.amazon.com/iPhone-bloqueado-suscripci%C3%B3n-empresa-telefon%C3%ADa/dp/B09G9CX7DK/ref=sr_1_1_sspa?__mk_es_US=%C3%85M%C3%85%C5%BD%C3%95%C3%91&keywords=iphone+13&qid=1650322902&sr=8-1-spons&psc=1&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUExTE1VNVdORVA2OEFBJmVuY3J5cHRlZElkPUEwNjQyNTg5MkNVMUFINzlIQkdaTCZlbmNyeXB0ZWRBZElkPUEwNDkxMDY2MjJPR0U0Ukw1MTRCTSZ3aWRnZXROYW1lPXNwX2F0ZiZhY3Rpb249Y2xpY2tSZWRpcmVjdCZkb05vdExvZ0NsaWNrPXRydWU=",
                    Selector = "qa-availability-message"

                },
                new UrlModel
                {
                    Product = "Puppeteer article",
                    Url = "https://www.toptal.com/puppeteer/headless-browser-puppeteer-tutorial",
                    Selector = "qa-availability-message"

                }
            };

        static async Task Main(string[] args)
        {
            foreach (UrlModel e in urls)
            {
                var r = await existProduct(e.Url, e.Selector);
                Console.WriteLine("Exist: " + r);
            }
            Console.ReadLine();
        }

        static async Task<bool> existProduct(string url, string selector, bool isId = false)
        {
            bool result = false;
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = _browser
                }
                );
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(url);

            try
            {
                if (isId)
                    await page.WaitForSelectorAsync($"#{selector}");
                else
                    await page.WaitForSelectorAsync($".{selector}");

                string content = await page.GetContentAsync();

                if (content is not null)
                    if (content.Contains($"{selector}"))
                        result = true;
            }
            catch { }

            await browser.CloseAsync();
            return result;
        }
    }
}