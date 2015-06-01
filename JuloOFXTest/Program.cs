using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Console.Out.WriteLine(c.xmlContents.ToString());
            Console.ReadKey();
        }
    }
}
