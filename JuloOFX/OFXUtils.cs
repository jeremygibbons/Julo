using System;
using System.Globalization;

namespace JuloOFX
{
    public class OFXUtils
    {
        public static DateTime ConvertOFXDateTimetoSysDateTime(string ofxDateTime)
        {
            string[] formats = { "yyyyMMddHHmmss.FFF", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMdd"};
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(ofxDateTime, formats, provider, DateTimeStyles.None);
        }
    }
}
