using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebScrapping.Models;

namespace WebScrapping.Services
{
    public class WebScrappingService
    {
        readonly UrlService _urlService;

        public WebScrappingService(UrlService urlsServices)
        {
            _urlService = urlsServices;
        }

        public async Task Run()
        {
            List<UrlModel> urls;
            try
            {
                urls = await _urlService.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (UrlModel url in urls)
            {
                var exist = await ExistProduct(url.UrlLink, url.Selector, url.Id.ToString());
                if (exist)
                {
                    try
                    {
                        await _urlService.FindSuccessUpdate(url.Id.ToString());
                        Console.WriteLine($"Producto entontrado: {url.Product}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Producto no entontrado: {url.Product}");
                        throw ex;
                    }
                }
                else
                {
                    Console.WriteLine($"Producto no entontrado: {url.Product}");
                }
            }
        }

        async Task<bool> ExistProduct(string url, string selector, string id, bool isId = false)
        {
            string _browser = ConfigurationManager.AppSettings["chromePath"] ?? @"C:\Program Files\Google\Chrome\Application\chrome.exe";


            bool result = false;
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = _browser
                }
                );
            await using var page = await browser.NewPageAsync();

            try
            {
                await page.GoToAsync(url);
                if (isId)
                    await page.WaitForSelectorAsync($"#{selector}");
                else
                    await page.WaitForSelectorAsync($".{selector}");

                string content = await page.GetContentAsync();

                if (content is not null)
                    if (content.Contains($"{selector}"))
                    {
                        string stringPath =  @"C:\Images\";
                        System.IO.Directory.CreateDirectory(stringPath);
                        //await page.ScreenshotAsync(stringPath + id + ".png");
                        var screeshotBytes = await page.ScreenshotDataAsync(new ScreenshotOptions() { FullPage = true, Type= ScreenshotType.Png });
                        MemoryStream ms = new MemoryStream(screeshotBytes);
                        var image = Image.FromStream(ms);
                        image.Save(
                            stringPath + id + ".png");
                        await _urlService.ImgUpdate(id, screeshotBytes);

                        result = true;
                    }
            }
            catch { }

            await browser.CloseAsync();
            return result;
        }


    }
}
