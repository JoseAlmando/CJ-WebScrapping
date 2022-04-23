using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapping.Models;

namespace WebScrapping
{
    class ApplicationDBContext: DbContext
    {
        public DbSet<UrlModel> urlModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=WebScrappingDB;Trusted_Connection=True;");
        }
    }
}
