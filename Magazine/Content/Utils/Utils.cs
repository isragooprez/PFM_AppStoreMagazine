using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Magazine.Content.Utils
{
    public static class Utils
    {

        public static string ClearTagsHtml(string html)
        {
            HtmlDocument dom = new HtmlDocument();
            dom.LoadHtml(html);
            return Regex.Replace(dom.DocumentNode.InnerText, "[^a-zA-Z]+", string.Empty, RegexOptions.Compiled); ;
        }

        public static string OnlyCaractersSpain(string cadena)
        {
            string result = string.Empty;
            string[] stringArray = cadena.ToCharArray().Select(c => c.ToString()).ToArray();
            foreach (string texto in stringArray)
            {
                result = Regex.Replace(texto, @"[^\p{IsBasicLatin}áéíóúüñ¿¡]+", string.Empty, RegexOptions.IgnoreCase);
            }
            return result;
        }
    }
}