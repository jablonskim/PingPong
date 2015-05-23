using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PingPong
{
    class PingPong
    {
        string in_file;
        string out_file;
        int[][,] matrix;
        int n;

        public PingPong(string input, string output)
        {
            in_file = input;
            out_file = output;
        }

        int[,] ReadInput()
        {
            string[] lines;

            try
            {
                lines = File.ReadAllLines(in_file).Where(x => x.Length > 1).ToArray();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Cannot open file '{0}'.\n{1}", in_file, e.Message));
            }

            n = lines.Length;

            int[,] tab = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                string[] vals = lines[i].Split(',');

                if (vals.Length != n)
                    throw new ArgumentException("Invalid data format.");

                for (int j = 0; j < n; ++j)
                {
                    tab[i, j] = int.Parse(vals[j].Trim());
                }
            }

            return tab;
        }

        void Create3D(int[,] input)
        {
            matrix = new int[2][,];

            matrix[0] = new int[n, n];
            matrix[1] = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    matrix[0][i, j] = input[i, j];
                    matrix[1][i, j] = 0;
                }
            }
        }

        void Multiply()
        {
            for (int u = 0; u < n; ++u)
            {
                for (int v = 0; v < n; ++v)
                {
                    for (int w = 0; w < n; ++w)
                    {
                        matrix[1][u, v] += matrix[0][u, w] * matrix[0][w, v];
                    }
                }
            }
        }

        List<int> FindSolution()
        {
            List<int> solutions = new List<int>();

            for (int r = 0; r < n; ++r)
            {
                bool ok = true;

                for (int c = 0; c < n; ++c)
                {
                    if (c != r && matrix[0][r, c] + matrix[1][r, c] == 0)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                    solutions.Add(r);
            }

            return solutions;
        }

        void SaveResults(List<int> r)
        {
            using (StreamWriter sw = new StreamWriter(out_file))
            {
                foreach (int i in r)
                {
                    Console.Write("{0}, ", i + 1);
                    sw.WriteLine(i + 1);
                }
            }

            Console.WriteLine();
        }

        public bool Run()
        {
            try
            {
                int[,] IN = ReadInput();
                Create3D(IN);
                Strassen.Multiply(matrix);
                List<int> r = FindSolution();
                SaveResults(r);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: PingPong.exe <input> <output>");
                return;
            }

            PingPong pong = new PingPong(args[0], args[1]);
            pong.Run();
        }
    }
}
