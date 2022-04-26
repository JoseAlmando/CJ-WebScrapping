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
        //[Key]
        public Guid Id { get; set; } = new Guid();
        public string Product { get; set; }
        public string UrlLink { get; set; }
        public string Selector { get; set; }
        public bool FindSuccess { get; set; } = false;
        public byte[]? Img { get; set; }
    }
}
