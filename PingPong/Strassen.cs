using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    static class Strassen
    {
        private static int[,] Sum(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            int[,] res = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    res[i, j] = a[i, j] + b[i, j];
                }
            }

            return res;
        }

        private static int[,] Sub(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            int[,] res = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    res[i, j] = a[i, j] - b[i, j];
                }
            }

            return res;
        }

        private static int[,] Mul(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            int[,] c = new int[n, n];

            if (n == 1)
            {
                c[0, 0] = a[0, 0] * b[0, 0];
                return c;
            }

            int new_n = n / 2;

            int[][,] na = new int[4][,]
            {
                new int[new_n, new_n],
                new int[new_n, new_n],
                new int[new_n, new_n],
                new int[new_n, new_n]
            };

            int[][,] nb = new int[4][,]
            {
                new int[new_n, new_n],
                new int[new_n, new_n],
                new int[new_n, new_n],
                new int[new_n, new_n]
            };

            int[][,] nc = new int[4][,];

            int[][,] m = new int[7][,];

            for (int i = 0; i < new_n; ++i)
            {
                for (int j = 0; j < new_n; ++j)
                {
                    na[0][i, j] = a[i, j];
                    na[1][i, j] = a[i, j + new_n];
                    na[2][i, j] = a[i + new_n, j];
                    na[3][i, j] = a[i + new_n, j + new_n];

                    nb[0][i, j] = b[i, j];
                    nb[1][i, j] = b[i, j + new_n];
                    nb[2][i, j] = b[i + new_n, j];
                    nb[3][i, j] = b[i + new_n, j + new_n];
                }
            }

            m[0] = Mul(Sum(na[0], na[3]), Sum(nb[0], nb[3]));
            m[1] = Mul(Sum(na[2], na[3]), nb[0]);
            m[2] = Mul(na[0], Sub(nb[1], nb[3]));
            m[3] = Mul(na[3], Sub(nb[2], nb[0]));
            m[4] = Mul(Sum(na[0], na[1]), nb[3]);
            m[5] = Mul(Sub(na[2], na[0]), Sum(nb[0], nb[1]));
            m[6] = Mul(Sub(na[1], na[3]), Sum(nb[2], nb[3]));

            nc[0] = Sub(Sum(Sum(m[0], m[3]), m[6]), m[4]);
            nc[1] = Sum(m[2], m[4]);
            nc[2] = Sum(m[1], m[3]);
            nc[3] = Sub(Sum(Sum(m[0], m[2]), m[5]), m[1]);

            for (int i = 0; i < new_n; ++i)
            {
                for (int j = 0; j < new_n; ++j)
                {
                    c[i, j] = nc[0][i, j];
                    c[i, j + new_n] = nc[1][i, j];
                    c[i + new_n, j] = nc[2][i, j];
                    c[i + new_n, j + new_n] = nc[3][i, j];
                }
            }

            return c;
        }

        public static void Multiply(int[][,] matrix)
        {
            int n = matrix[0].GetLength(0);
            int str_n = 1;

            while (str_n < n)
                str_n *= 2;

            int[,] mtx = new int[str_n, str_n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    mtx[i, j] = matrix[0][i, j];
                }
            }

            int[,] solution = Strassen.Mul(mtx, mtx);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    matrix[1][i, j] = solution[i, j];
                }
            }
        }
    }
}
