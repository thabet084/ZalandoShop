using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Models
{
    public class Content
    {
        public string id { get; set; }
        public string modelId { get; set; }
        public string name { get; set; }
        public string shopUrl { get; set; }
        public string color { get; set; }
        public bool available { get; set; }
        public string season { get; set; }
        public string seasonYear { get; set; }
        public DateTime activationDate { get; set; }
        public List<object> additionalInfos { get; set; }
        public List<string> genders { get; set; }
        public List<string> ageGroups { get; set; }
        public Brand brand { get; set; }
        public List<string> categoryKeys { get; set; }
        public List<Attribute> attributes { get; set; }
        public List<Unit> units { get; set; }
        public Media media { get; set; }
        public List<object> tags { get; set; }
    }
}
