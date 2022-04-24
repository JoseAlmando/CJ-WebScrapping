using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using WebScrapping.Models;
using WebScrapping.Services;

using System.Configuration;
namespace WebScrapping
{
    internal class Program
    {
        static ApplicationDBContext context = new();
        static UrlService _urlService = new(context);
        static WebScrappingService _webScrappingService = new(_urlService);

        static async Task Main(string[] args)
        {
            UrlModel urlModel;
            bool nextContinue = false;

            do
            {
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
                Console.WriteLine("Presione 6 para hacer WebScrapping");

                int option = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (option)
                    {
                        case 1:
                            urlModel = new();

                            Console.WriteLine("Product: ");
                            urlModel.Product = Console.ReadLine() ?? "";

                            Console.WriteLine("Url link: ");
                            urlModel.UrlLink = Console.ReadLine() ?? "";

                            Console.WriteLine("Selector: ");
                            urlModel.Selector = Console.ReadLine() ?? "";

                            await _urlService.SaveUrl(urlModel);

                            Console.WriteLine("Exito");
                            break;
                        case 2:
                            await ListarUrls();

                            urlModel = new();

                            Console.WriteLine("Id: ");
                            string id = Console.ReadLine();
                            if (string.IsNullOrEmpty(id)) throw new Exception("Debe seleccionar un ID: ");

                            urlModel.Id = new Guid(id);

                            Console.WriteLine("Product: ");
                            urlModel.Product = Console.ReadLine() ?? urlModel.Product;

                            Console.WriteLine("Url link: ");
                            urlModel.UrlLink = Console.ReadLine() ?? urlModel.UrlLink;

                            Console.WriteLine("Selector: ");
                            urlModel.Selector = Console.ReadLine() ?? urlModel.Selector;
                            Console.WriteLine("Exito");
                            break;
                        case 3:
                            await ListarUrls();

                            Console.WriteLine("Id: ");
                            string _id = Console.ReadLine() ?? "";
                            if (string.IsNullOrEmpty(_id)) throw new Exception("Debe seleccionar un ID: ");

                            urlModel = await _urlService.GetUrlById(_id);
                            printUrlData(urlModel);

                            Console.WriteLine("Exito");
                            break;
                        case 4:
                            await ListarUrls();

                            Console.WriteLine("Id: ");
                            string dId = Console.ReadLine() ?? "";
                            if (string.IsNullOrEmpty(dId)) throw new Exception("Debe seleccionar un ID: ");

                            await _urlService.Delete(dId);
                            Console.WriteLine("Exito");
                            break;
                        case 5:
                            await ListarUrls();
                            Console.WriteLine("Exito");
                            break;
                        case 6:
                            await _webScrappingService.Run();
                            Console.WriteLine("Exito");
                            break;
                        default:
                            Console.WriteLine("La opcion ingresada incorrecta");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                Console.WriteLine("¿Continuar? 1.- Si. Otra tecla no.");
                nextContinue = Console.ReadLine() == "1" ? true : false;

            } while (nextContinue == true);
        }

        private static async Task ListarUrls()
        {
            var urls = await _urlService.GetAll();
            if (urls is null)
            {
                Console.WriteLine("No Urls para listar");
                return;
            }

            foreach (var url in urls)
            {
                printUrlData(url);
            }
        }

        private static void printUrlData(UrlModel url)
        {
            Console.WriteLine($"ID: {url.Id.ToString()} - Product: {url.Product} - Link: {url.UrlLink} Selector: {url.Selector}");
        }

    }
}