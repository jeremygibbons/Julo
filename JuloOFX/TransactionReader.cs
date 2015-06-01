using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sgml;

namespace JuloOFX
{
    public class TransactionReader
    {
        SgmlReader sgmlReader;

        public TransactionReader(System.IO.TextReader textReader)
        {
            sgmlReader = new Sgml.SgmlReader();
            sgmlReader.DocType = "OFX";
            sgmlReader.InputStream = textReader;
            sgmlReader.SystemLiteral = "ofxdtd\\ofx160.dtd";
        }

        public OFXFileHeader ReadOFXHeaders(System.IO.TextReader textReader)
        {
            return OFXFileHeader.GetOFXHeadersFromTextReader(textReader);
        }

        public OFXFileContents ReadOFXContents(System.IO.TextReader textReader)
        {
            return OFXFileContents.GetOFXContentsFromTextReader(textReader);
        }

    }

    public class OFXFileHeader
    {
        static HashSet<string> HeaderKeys = new HashSet<string> { "OFXHEADER", "DATA", "VERSION", "SECURITY",
            "OLDFILEUID", "NEWFILEUID", "ENCODING", "COMPRESSION", "CHARSET" };
        Dictionary<string, string> headers;

        public static OFXFileHeader GetOFXHeadersFromTextReader(System.IO.TextReader textReader)
        {
            bool endOfHeadersReached = false;
            OFXFileHeader ofh = new OFXFileHeader();
            ofh.headers = new Dictionary<string, string>();
            while (endOfHeadersReached == false)
            {
                string line = textReader.ReadLine();
                if (line == "")
                {
                    endOfHeadersReached = true;
                    continue;
                }
                string[] elts = line.Split(':');
                if (elts.Length == 0)
                    break;
                if (elts.Length == 2)
                {
                    if (HeaderKeys.Contains(elts[0]) == false)
                        break; //should be throw something
                    ofh.headers.Add(elts[0], elts[1]);
                }
                else
                {
                    if (HeaderKeys.Contains(elts[0]) == false)
                        break; //should be throw something
                }
            }
            return ofh;
        }

        public void printHeaders() {
            foreach(var head in headers) {
                Console.WriteLine(head.Key + " " + head.Value);
            }
        }
    }
}
