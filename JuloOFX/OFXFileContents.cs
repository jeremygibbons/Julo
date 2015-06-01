using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Sgml;

namespace JuloOFX
{
    public class OFXFileContents
    {
        public XmlDocument xmlContents {get; set;}

        public OFXFileContents()
        {
            xmlContents = new XmlDocument();
        }

        public static OFXFileContents GetOFXContentsFromTextReader(System.IO.TextReader textReader) {
            OFXFileContents ofc = new OFXFileContents();
            Sgml.SgmlReader sgmlReader = new SgmlReader();

            sgmlReader.DocType = "OFX";
            sgmlReader.InputStream = textReader;

            System.IO.StringReader sr = new System.IO.StringReader(JuloOFX.Properties.Resources.ofx160);
            sgmlReader.Dtd = SgmlDtd.Parse((Uri)null, "OFX1.6.0", sr, (string)null, (string)null, sgmlReader.NameTable);

            XmlDocument doc = ofc.xmlContents;
            doc.PreserveWhitespace = true;
            doc.XmlResolver = null;
            doc.Load(sgmlReader);
            return ofc;
        }
    }
}
