using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebScrapping.Models;

namespace WebScrapping.Services
{
    public class WebScrappingService
    {
        readonly UrlService _urlService;
        static List<UrlModel> urls;



        public WebScrappingService(UrlService urlsServices)
        {
            _urlService = urlsServices;
        }

        public async  void Run()
        {
            List<UrlModel> urls;
            try
            {
                urls = await _urlService.GetAll();
            }
            catch (Exception ex){
                throw ex;
            }
            
            foreach (UrlModel url in urls)
            {
                var exist = await ExistProduct(url.UrlLink, url.Selector);
                if (exist)
                {
                    try
                    {
                        await _urlService.FindSuccessUpdate(url.Id.ToString());
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        }

        static async Task<bool> ExistProduct(string url, string selector, bool isId = false)
        {
            string _browser = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

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
