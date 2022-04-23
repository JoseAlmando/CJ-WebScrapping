using System;
using System.Threading.Tasks;
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
                    UrlLink = "https://www.amazon.com/iPhone-bloqueado-suscripci%C3%B3n-empresa-telefon%C3%ADa/dp/B09G9CX7DK/ref=sr_1_1_sspa?__mk_es_US=%C3%85M%C3%85%C5%BD%C3%95%C3%91&keywords=iphone+13&qid=1650322902&sr=8-1-spons&psc=1&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUExTE1VNVdORVA2OEFBJmVuY3J5cHRlZElkPUEwNjQyNTg5MkNVMUFINzlIQkdaTCZlbmNyeXB0ZWRBZElkPUEwNDkxMDY2MjJPR0U0Ukw1MTRCTSZ3aWRnZXROYW1lPXNwX2F0ZiZhY3Rpb249Y2xpY2tSZWRpcmVjdCZkb05vdExvZ0NsaWNrPXRydWU=",
                    Selector = "qa-availability-message"

                },
                new UrlModel
                {
                    Product = "Puppeteer article",
                    UrlLink = "https://www.toptal.com/puppeteer/headless-browser-puppeteer-tutorial",
                    Selector = "qa-availability-message"

                }
            };

        static async Task Main(string[] args)
        {
            var dbContext = new ApplicationDBContext();

            Console.WriteLine("----------------------");
            Console.WriteLine("Web Scrapping Project");
            Console.WriteLine("----------------------");
            Console.WriteLine("Menu:");
            Console.WriteLine("----------------------");
            Console.WriteLine("CRUD URLs");
            Console.WriteLine("----------------------");
            Console.WriteLine("Presione 1 para crear busqueda");
            Console.WriteLine("Presione 2 para actualizar busqueda");
            Console.WriteLine("Presione 3 para consultar busqueda");
            Console.WriteLine("Presione 4 para eliminar busqueda");
            Console.WriteLine("Presione 5 para listar las busquedas");

            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Producto:");
                    string product = Console.ReadLine();
                    Console.WriteLine("URL:");
                    string url = Console.ReadLine();
                    Console.WriteLine("Selector:");
                    string selector = Console.ReadLine();

                    var saveUrl = new UrlModel()
                    {
                        Id = new Guid(),
                        Product = product,
                        UrlLink = url,
                        Selector = selector,
                        FindSuccess = false
                    };

                    dbContext.UrlModel.Add(saveUrl);
                    dbContext.SaveChanges();
                    Console.WriteLine($"El {product} ha sido guardado correctamente!!");

                    break;
                case 2:
                    Console.WriteLine("Producto:");
                    string productUpd = Console.ReadLine();
                    var dataUpd = dbContext.UrlModel.Where(x => x.Id == new Guid(productUpd)).FirstOrDefault();
                    if (dataUpd == null) Console.WriteLine("Este producto no existe");
                    Console.WriteLine("URL:");
                    dataUpd.UrlLink = Console.ReadLine();
                    Console.WriteLine("Selector:");
                    dataUpd.Selector = Console.ReadLine();

                    dbContext.UrlModel.Update(dataUpd);
                    dbContext.SaveChanges();
                    break;
                case 3:
                    string s = Console.ReadLine();

                    var x = dbContext.UrlModel.Find(new Guid(s));

                    break;
                case 4:
                    Console.WriteLine("Producto:");
                    string productDel = Console.ReadLine();
                    var dataDel = dbContext.UrlModel.Find(new Guid(productDel));
                    dbContext.Remove(dataDel);
                    dbContext.SaveChanges();

                    break;
                case 5:
                    var data = dbContext.UrlModel.ToList();
                    foreach (var item in data)
                    {
                        Console.WriteLine(item.Product);
                    }
                    break;
                default:
                    Console.WriteLine("La opcion ingresada incorrecta");
                    break;

            }
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