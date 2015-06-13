using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using JuloOFX;

namespace JuloOFXTest
{
    class Program
    {
        static void Main(string[] args)
        {
            JuloOFX.OFXFileHeader h;
            JuloOFX.OFXFileContents c;
            using (System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\testdata\test.ofx"))
            {
                JuloOFX.TransactionReader tr = new JuloOFX.TransactionReader(file);
                h = tr.ReadOFXHeaders(file);
                c = tr.ReadOFXContents(file);
            }

            h.printHeaders();
            //c.xmlContents.Save(Console.Out);
            XmlNode sonrs = c.xmlContents.SelectSingleNode("/OFX/SIGNONMSGSRSV1/SONRS");
            XmlNode dtserver = sonrs.SelectSingleNode("DTSERVER");
            string dtserverstr = dtserver.InnerText;
            Console.WriteLine("DTSERVER " + dtserverstr);
            DateTime d = JuloOFX.OFXUtils.ConvertOFXDateTimetoSysDateTime(dtserverstr);
            Console.WriteLine(d.ToString());
            XmlNode stmttrnrs = c.xmlContents.SelectSingleNode("/OFX/BANKMSGSRSV1/STMTTRNRS");
            string transactionID = stmttrnrs.SelectSingleNode("TRNUID").InnerText;
            Console.WriteLine("TRNUID " + transactionID);
            XmlNodeList stmTrnList = c.xmlContents.SelectNodes("/OFX/BANKMSGSRSV1/STMTTRNRS/STMTRS/BANKTRANLIST/STMTTRN");
            foreach (XmlNode stmTrn in stmTrnList)
            {
                Console.WriteLine("Transaction with " + stmTrn.SelectSingleNode("NAME").InnerText + " for amount " + stmTrn.SelectSingleNode("TRNAMT").InnerText);
            }

            Console.ReadKey();
        }
    }
}
