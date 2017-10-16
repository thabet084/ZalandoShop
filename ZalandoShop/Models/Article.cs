using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Models
{
    public class Article
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Size { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
