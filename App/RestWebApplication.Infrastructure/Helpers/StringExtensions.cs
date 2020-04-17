using System.Text.RegularExpressions;
using RestWebApplication.Common;

namespace RestWebApplication.Infrastructure.Helpers
{
    public static class StringExtensions
    {
        public static string LowerCaseLink(this string link)
        {
            ThrowHelper.ThrowIfNullEmptyOrWhitespace(link,nameof(link));
            
            var replaceSearchPattern = new Regex("(/[A-Z]{1})");
            var strToReplace = replaceSearchPattern.Match(link).Value;
            
            ThrowHelper.ThrowIfNullEmptyOrWhitespace(strToReplace,nameof(strToReplace));
                
            return link.Replace(strToReplace, strToReplace.ToLower());


        }
    }
}