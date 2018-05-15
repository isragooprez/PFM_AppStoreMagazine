using System;
using System.Collections.Generic;
using System.Configuration;
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

        public List<NsoupMagazine> FetchScimagojr(string parameter)
        {
            List<NsoupMagazine> listNsoupMagazines = new List<NsoupMagazine>();
            // Connecting & Fetching ...
            IConnection connection = NSoupClient.Connect(ConfigurationManager.AppSettings.Get("BASE_URL") + parameter);
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

            foreach (Element elem in document.Select(".search_results").Select("a"))
            {
                NsoupMagazine nsoupMagazine = new NsoupMagazine();

                //var resul = document.Select(".search_results").Select("a").HasAttr("abs:href").ToString();

                nsoupMagazine.Url = elem.Attr("abs:href").ToString();
                nsoupMagazine.Name = elem.Select("span").Text;

                var removeSpan = elem.Select("span").ToString();

                var newhtml = elem.ToString().Replace(removeSpan, string.Empty).Replace("<br />", ",");
                Document doc = NSoup.NSoupClient.Parse(newhtml);
                nsoupMagazine.Country = doc.Select("a").Text.Split(',')[0];
                nsoupMagazine.Publisher = doc.Select("a").Text.Split(',')[1];
                listNsoupMagazines.Add(nsoupMagazine);
            }



            return listNsoupMagazines;

        }


        //nsoupMagazine.Url = document.Select(".search_results").Select("a").Attr("abs:href").ToString();
        //nsoupMagazine.Name = document.Select(".search_results").Select("a").Select("span").Text;

        //        var removeSpan = document.Select(".search_results").Select("a").Select("span").ToString();

        //var newhtml = document.Select(".search_results").Select("a").ToString().Replace(removeSpan, string.Empty).Replace("<br />", ",");
        //Document doc = NSoup.NSoupClient.Parse(newhtml);
        //nsoupMagazine.Country = doc.Select("a").Text.Split(',')[0];
        //        nsoupMagazine.Publisher = doc.Select("a").Text.Split(',')[1];
    }

}