using System;
using System.Text.RegularExpressions;

namespace SimpleTools.Core.Base.Extensions
{
    public static class StringExtensions
    {
        // Answer by 41686d6564: https://stackoverflow.com/questions/7578857/how-to-check-whether-a-string-is-a-valid-http-url
        public static Uri AsURL(this string source)
        {
            //TODO: This method ought to be made to be better.
            if (source.Contains("."))
            {
                if (!Regex.IsMatch(source, @"^https?:\/\/", RegexOptions.IgnoreCase))
                {
                    source = "http://" + source;
                }

                if (Uri.TryCreate(source, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    return uri;
                }
            }

            return null;
        }

    }
}
