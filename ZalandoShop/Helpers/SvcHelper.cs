using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using ZalandoShop.Models;

namespace ZalandoShop.Helpers
{
    public class SvcHelper
    {
        private ResourceLoader resourceLoader;

        /// <summary>
        /// Return responce from Zalando api 
        /// </summary>
        /// <param name="keyword">Search keyword</param>
        /// <param name="gender">Male or female</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Size of page</param>
        /// <returns></returns>
        private  async Task<string> GetResponse(string keyword, Gender gender, int page, int pageSize)
        {
            HttpClient client = new HttpClient();
            resourceLoader = new ResourceLoader();
            string apiBaseURI = resourceLoader.GetString("ApiUrl");
            client.BaseAddress = new Uri(apiBaseURI);


            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("If-None-Match", "304");
            client.DefaultRequestHeaders.Add("Accept-Language", "de-DE");
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            string fields = "&fields=name,color,media.images.mediumUrl,units.price,units.size";

            string requestUri = "articles?fullText=" + keyword  + "&page=" + page + "&pageSize=" + pageSize;

            
                requestUri += "&gender=" + gender.ToString();

            string popularity = "&sort=popularity";


            requestUri += fields + popularity;

            HttpResponseMessage response = await client.GetAsync(requestUri);


            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                using (var streamReader = new StreamReader(decompressedStream))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
            return null;

        }
        /// <summary>
        /// Get articles that match passed criteria
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="gender"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<Article>> GetArticles(string keyword, Gender gender, int page, int pageSize)
        {
            var responce = await GetResponse(keyword, gender, page, pageSize);
            if (responce != null)
                return Map(JsonConvert.DeserializeObject<RootObject>(responce));

            return null;
        }

        /// <summary>
        /// Map returned object collections from zalando API to article object that displayed at view
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private  List<Article> Map(RootObject root)
        {
            List<Article> articles = new List<Article>();
            foreach(Content content in root.content)
            {
                Article article = new Article();
                article.ImageUrl = content.media.images[0].mediumUrl;
                article.Name = content.name;
                article.Price = content.units[0].price.value;
                article.Currency = content.units[0].price.currency;
                article.Size = "Size: "+ content.units[0].size;

                articles.Add(article);
            }
            return articles;
        }
    }
}
