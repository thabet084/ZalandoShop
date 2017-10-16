using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Models
{
    /// <summary>
    /// Article search class that build on user search criteria 
    /// </summary>
    public class ArticleSearch
    {
        /// <summary>
        /// Search keyword
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gender either male or female
        /// </summary>
        public Gender Gender { get; set; }
    }
}
