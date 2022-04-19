using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebScrapping.Models
{
    [Table("Urls")]
    public class UrlModel
    {
        public string Product { get; set; }
        public string Url { get; set; }
        public string Selector { get; set; }
        public bool findSuccess { get; set; }
    }
}
