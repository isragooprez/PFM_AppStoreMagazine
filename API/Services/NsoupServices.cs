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
using Newtonsoft.Json;

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
            IConnection connection = NSoupClient.Connect(ConfigurationManager.AppSettings.Get("BASE_URL") + UrlDecode(ulrMagazine));
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
            //Quartiles
            magazine.Quartiles = ToJsonQuartiles(document);
            //SJR
            magazine.SJR = ToJsonSJR(document);
            //CitationsPerDocument
            magazine.CitationsPerDocument = ToJsonCitationsPerDocument(document);
            //TotalCites
            magazine.TotalCites = ToJsonTotalCites(document);
            //CitesPerDoc
            magazine.Cites = ToJsonCites(document);
            //InternationalCollaboration
            magazine.InternationalCollaboration = ToJsonInternationalCollaboration(document);
            //DocumentsNoncitable
            magazine.DocumentsNoncitable = ToJsonDocumentsNoncitable(document);
            //DocumentsUncited
            magazine.DocumentsUncited = ToJsonDocumentsUncited(document);

            return magazine;
        }

        public string ToJsonDocumentsUncited(Document document)
        {
            List<DocumentsUncitedModels> lst_DocumentsUncitable = new List<DocumentsUncitedModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell1x1")[5].Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                DocumentsUncitedModels _documUnCit = new DocumentsUncitedModels();
                _documUnCit.Documents = elem.Select("td")[0].Text();
                _documUnCit.Year = elem.Select("td")[1].Text();
                _documUnCit.Value = elem.Select("td")[2].Text();
                lst_DocumentsUncitable.Add(_documUnCit);
            }
            return JsonConvert.SerializeObject(lst_DocumentsUncitable);
        }

        public string ToJsonDocumentsNoncitable(Document document)
        {
            List<DocumentsNoncitableModels> lst_DocumentsNoncitable = new List<DocumentsNoncitableModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell1x1")[4].Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                DocumentsNoncitableModels _documNonCit = new DocumentsNoncitableModels();
                _documNonCit.Documents = elem.Select("td")[0].Text();
                _documNonCit.Year = elem.Select("td")[1].Text();
                _documNonCit.Value = elem.Select("td")[2].Text();
                lst_DocumentsNoncitable.Add(_documNonCit);
            }
            return JsonConvert.SerializeObject(lst_DocumentsNoncitable);
        }

        public string ToJsonInternationalCollaboration(Document document)
        {
            List<InternationalCollaborationModels> lst_InternationalCollaboratio = new List<InternationalCollaborationModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell1x1")[3].Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                InternationalCollaborationModels _interColl = new InternationalCollaborationModels();
                _interColl.Year = elem.Select("td")[0].Text();
                _interColl.InternationalCollaboration = elem.Select("td")[1].Text();
                lst_InternationalCollaboratio.Add(_interColl);
            }
            return JsonConvert.SerializeObject(lst_InternationalCollaboratio);
        }

        public string ToJsonCites(Document document)
        {
            List<CitesPerDocModels> lst_CitesModels = new List<CitesPerDocModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell1x1")[2].Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                CitesPerDocModels _citesPerDocModels = new CitesPerDocModels();
                _citesPerDocModels.Cites = elem.Select("td")[0].Text();
                _citesPerDocModels.Year = elem.Select("td")[1].Text();
                _citesPerDocModels.Value = elem.Select("td")[2].Text();
                lst_CitesModels.Add(_citesPerDocModels);
            }
            return JsonConvert.SerializeObject(lst_CitesModels);
        }

        public string ToJsonTotalCites(Document document)
        {
            List<TotalCitesModels> lst_TotalCitesModels = new List<TotalCitesModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell1x1")[1].Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                TotalCitesModels _totalCites = new TotalCitesModels();
                _totalCites.Cites = elem.Select("td")[0].Text();
                _totalCites.Year = elem.Select("td")[1].Text();
                _totalCites.Value = elem.Select("td")[2].Text();
                lst_TotalCitesModels.Add(_totalCites);
            }
            return JsonConvert.SerializeObject(lst_TotalCitesModels);
        }

        public string ToJsonCitationsPerDocument(Document document)
        {
            List<CitationsPerDocumentModels> lst_citationsPerDocumentModels = new List<CitationsPerDocumentModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell1x2").Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                CitationsPerDocumentModels _citationsPerDocumentModels = new CitationsPerDocumentModels();
                _citationsPerDocumentModels.CitesPerDocument = elem.Select("td")[0].Text();
                _citationsPerDocumentModels.Year = elem.Select("td")[1].Text();
                _citationsPerDocumentModels.Value = elem.Select("td")[2].Text();
                lst_citationsPerDocumentModels.Add(_citationsPerDocumentModels);
            }
            return JsonConvert.SerializeObject(lst_citationsPerDocumentModels);
        }

        public string ToJsonSJR(Document document)
        {
            List<SjrModels> lst_Jsr = new List<SjrModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell1x1")[0].Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                SjrModels _sjrModels = new SjrModels();
                _sjrModels.Year = elem.Select("td")[0].Text();
                _sjrModels.SJR = elem.Select("td")[1].Text();
                lst_Jsr.Add(_sjrModels);
            }
            return JsonConvert.SerializeObject(lst_Jsr);
        }

        public string ToJsonQuartiles(Document document)
        {
            List<QuartilesModels> lst_quartiles = new List<QuartilesModels>();
            var elem_Quartiles = document.Select(".dashboard").Select(".cell2x1").Select(".cellcontent:has(table)");
            var hh = elem_Quartiles.Select("p").Text;
            foreach (Element elem in elem_Quartiles.Select("table").Select("tbody").Select("tr"))
            {
                QuartilesModels _quartiles = new QuartilesModels();
                _quartiles.Category = elem.Select("td")[0].Text();
                _quartiles.Year = elem.Select("td")[1].Text();
                _quartiles.Quartile = elem.Select("td")[2].Text();
                lst_quartiles.Add(_quartiles);
            }

            return JsonConvert.SerializeObject(lst_quartiles);
        }

        public string UrlEnCode(string url)
        {
            var url_code = string.Empty;
            if (url != string.Empty)
            {
                url_code = url.Replace('&', ',');
            }
            return url_code;
        }

        public string UrlDecode(string url)
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
            IConnection connection = NSoupClient.Connect(UrlDecode(ulrMagazine));
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