using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;
using ExCSS;
using NSoup;
using NSoup.Nodes;

namespace API.Services
{
    public class NsoupServices
    {
        private HtmlDataModels parsedData = new HtmlDataModels();

        public string FetchScimagojr(string Url)
        {
               

                // Connecting & Fetching ...
                IConnection connection = NSoupClient.Connect(Url);
                connection.Timeout(30000);
                Document document = connection.Get();

                // Parsing ...
                parsedData.Clear();
                parsedData.Url = document.BaseUri;
                parsedData.RawHtml = document.Html();
                parsedData.Title = document.Title;
                foreach (Element meta in document.Select("meta")) parsedData.MetadataList.Add(meta.OuterHtml());
                foreach (Element image in document.Select("img")) parsedData.ImageList.Add(image.Attr("src"));
                foreach (Element anchor in document.Select("a")) parsedData.AnchorList.Add(anchor.Attr("href"));

            return parsedData.Url;


        }
    }

}