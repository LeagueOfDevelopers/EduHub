using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace EduHub.Models.ValidationAttributes
{
    public class ContactCheck : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            const string pattern =
                @"^((http|https)://(vk.com|behance.net|twitter.com|ok.ru|facebook.com|instagram.com|github.com)/[0-9A-Za-z]+)$";
            const string patternTumblr = @"^((https|http)://([0-9A-Za-z]+).tumblr.com)$";
            const string mobilePattern = @"^(\d{11})$";
            var multiPattern = $"({pattern})|({mobilePattern})|({patternTumblr})";
            if (value is IList<string> list)
                return list.ToList().All(s => Regex.IsMatch(s, multiPattern, RegexOptions.IgnoreCase));

            return false;
        }
    }
}