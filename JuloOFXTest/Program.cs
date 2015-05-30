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
            using (System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\testdata\test.ofx"))
            {
                JuloOFX.TransactionReader tr = new JuloOFX.TransactionReader(file);
                h = tr.ReadOFXHeaders(file);
            }

            h.printHeaders();
            Console.ReadKey();
        }
    }
}
