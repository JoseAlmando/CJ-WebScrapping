using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using WebScrapping.Models;

namespace WebScrapping
{
    internal class Program
    {
        static async Task Main(string[] args)
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

            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    break;
                default:
                    Console.WriteLine("La opcion ingresada incorrecta");
                    break;

            }
        }

        
    }
}