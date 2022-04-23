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
        public Guid id { get; set; }
        public string product { get; set; }
        public string url { get; set; }
        public string selector { get; set; }
        public bool findSuccess { get; set; }
    }
}
