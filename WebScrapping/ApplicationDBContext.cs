using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapping.Models;

using System.Configuration;
namespace WebScrapping
{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<UrlModel> UrlModel { get; set; }

        private readonly string stringConnection = ConfigurationManager.AppSettings["stringConnection"] ?? "Server=.;Database=WebScrappingDB;Trusted_Connection=True;";

        public ApplicationDBContext()
        {
            if (string.IsNullOrEmpty(stringConnection)) throw new Exception("Debe especificar una cadena de conexion en el App.config");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(stringConnection);
        }
    }
}
