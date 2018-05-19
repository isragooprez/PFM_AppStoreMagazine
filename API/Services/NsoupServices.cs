using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
            foreach (Element elem in document.Select(".search_results").Select("a"))
            {
                NsoupMagazine nsoupMagazine = new NsoupMagazine();
                nsoupMagazine.Url = elem.Attr("abs:href").ToString();
                nsoupMagazine.Name = elem.Select("span").Text;
                var removeSpan = elem.Select("span").ToString();
                var newhtml = elem.ToString().Replace(removeSpan, string.Empty).Replace("<br />", ",");
                Document doc = NSoup.NSoupClient.Parse(newhtml);
                nsoupMagazine.Country = doc.Select("a").Text.Split(',')[0];
                nsoupMagazine.Publisher = doc.Select("a").Text.Split(',')[1];
                nsoupMagazine.Image = GetResquestImageByIssn(nsoupMagazine.Url);
                nsoupMagazine.Checked = false;
                listNsoupMagazines.Add(nsoupMagazine);
            }
            return listNsoupMagazines;
        }
        public Magazine GedDataMagazine(string ulrMagazine)
        {
            Magazine magazine = new Magazine();
            // Connecting & Fetching ...
            IConnection connection = NSoupClient.Connect(ConfigurationManager.AppSettings.Get("BASE_URL") + urlDecode(ulrMagazine));
            connection.Timeout(30000);
            Document document = connection.Get();
            // Parsing ...
            parsedData.Clear();
            parsedData.Url = document.BaseUri;
            parsedData.RawHtml = document.Html();
            parsedData.Title = document.Title;
            magazine.Name = document.Select(".journaldescription").Select("h1").Text;
            magazine.Imagen = GetResquestImageByIssn(parsedData.Url);
            magazine.Url = parsedData.Url;
            foreach (Element elem in document.Select(".journaldescription").Select("table").Select("tbody").Select("tr"))
            {
                var holasj = elem.Select("td").ToString();
                switch (elem.Select("td")[0].Text())
                {
                    case "Country":
                        magazine.Country = elem.Select("td")[1].Text();
                        magazine.Hindexnumber = elem.Select("td")[2].Select("div")[0].Text();
                        magazine.Hindexlegend = elem.Select("td")[2].Select("div")[1].Text();
                        break;
                    case "Subject Area and Category":
                        foreach (Element a in elem.Select("td").Select("a"))
                        {
                            magazine.Subjec += (a.HasAttr("style")) ? "-" + a.Text() : a.Text() + " ";
                        }
                        break;
                    case "Publisher":
                        foreach (Element a in elem.Select("td").Select("a"))
                        {
                            magazine.Publisher += (a.HasAttr("style")) ? "-" + a.Text() : a.Text() + " ";
                        }
                        break;
                    case "Publication type":
                            magazine.PublicationType += elem.Select("td")[1].Text();
                        break;
                    case "ISSN":
                        magazine.ISSN += elem.Select("td")[1].Text();
                        break;
                    case "Coverage":
                        magazine.Coverage += elem.Select("td")[1].Text();
                        break;
                    case "Scope":
                        magazine.Scope += elem.Select("td")[1].Text();
                        break;
                    default:
                        break;
                }
            }
            return magazine;
        }
        public string urlEnCode(string url)
        {
            var url_code = string.Empty;
            if (url != string.Empty)
            {
                url_code = url.Replace('&', ',');
            }
            return url_code;
        }

        public string urlDecode(string url)
        {
            var url_decode = string.Empty;
            if (url != string.Empty)
            {
                url_decode = url.Replace(',', '&');
            }
            return url_decode;
        }

        public string GetResquestImageByIssn(string ulrMagazine)
        {
            string url_validate = "https://image.freepik.com/free-vector/business-brochure-with-colorful-hexagonal-shapes_1017-3145.jpg";
            IConnection connection = NSoupClient.Connect(urlDecode(ulrMagazine));
            connection.Timeout(30000);
            Document document = connection.Get();
            // Parsing ...
            parsedData.Clear();
            parsedData.Url = document.BaseUri;
            parsedData.RawHtml = document.Html();
            parsedData.Title = document.Title;
            Element issns = document.Select(".journaldescription").Select("table").Select("tbody").Select("tr")[4].Select("td")[1];
            if (issns != null)
            {
                foreach (string url in issns.Text().Trim().Split(','))
                {
                    string url_img = "https://ars.els-cdn.com/content/image/X" + url + ".jpg";
                    try
                    {
                        //Creating the HttpWebRequest
                        HttpWebRequest request = WebRequest.Create(url_img) as HttpWebRequest;
                        //Setting the Request method HEAD, you can also use GET too.
                        request.Method = "HEAD";
                        //Getting the Web Response.
                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        //Returns TRUE if the Status code == 200
                        response.Close();
                        if (response.StatusCode == HttpStatusCode.OK) url_validate = url_img;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return url_validate;
        }

    }

}