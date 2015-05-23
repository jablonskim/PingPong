using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InputGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: InputGenerator <players count> <filename>");
                return;
            }

            using (StreamWriter sw = new StreamWriter(args[1]))
            {
                int n = int.Parse(args[0]);

                int[,] tab = new int[n, n];

                Random r = new Random();

                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < i; ++j)
                    {
                        int x = r.Next(0, 2);

                        tab[i, j] = x;
                        tab[j, i] = 1 - x;
                    }
                }

                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        sw.Write(tab[i, j]);
                        if (j != n - 1)
                            sw.Write(", ");
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
