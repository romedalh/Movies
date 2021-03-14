using System;
using System.Text.RegularExpressions;

namespace Movies.Logic.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex UrlRegex = new Regex("films/([0-9]*)/", RegexOptions.Compiled);
        public static int GetMovieIdFromUrl(this string url)
        {
            if(UrlRegex.IsMatch(url))
                return int.Parse(UrlRegex.Match(url).Groups[1].Value);
            throw new ArgumentException("String is not in an url");
        }
    }
}