using System;
using System.Threading.Tasks;

namespace RestWebApplication.Common
{
    public static class ThrowHelper
    {
        public static void ThrowIfNull(object param,string paramName)
        {
            if (param==null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
        
        public static void ThrowIfNullEmptyOrWhitespace(string param,string paramName)
        {
            if (string.IsNullOrEmpty(param)||string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}