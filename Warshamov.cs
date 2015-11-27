using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warshamov
{
    public partial class WarshamovEncode
    {
         public List<String> Cmatrix(int k, int d)
         {
            bool flag = true;
            int r = 2;
            
            
            double c = 0;
            while (flag)
            {
                c = 0;
                for (int i = 1; i <= (d - 2); i++)
                {
                    if (i > (k +r- 1 - i))
                        c += fact(k+r - 1, i) / fact(k +r- 1 - i, 1);
                    else
                        c += fact(k+r- 1, k+r - 1 - i) / fact(i, 1);
                }
                if (Math.Pow(2, r) > 1 + c)
                    flag = false;
                else
                    r++;
            }
         
           
            List<string> Cmas = new List<string>();
            string S1 = "";
            while (S1.Length<r-d+1)
            {
                S1 += "0";
            }
            for (int i = 0; i < d - 1; i++)
            {
                S1 += "1";
            }
            Cmas.Add(S1);
            int s = (int)Math.Pow(2, d-1) - 1;

            string BinaryS = "";
            int w = 0;
            int dmin=0;
            bool isG;
            while (Cmas.Count < k)
            {
                w = 0;
                s++;
                isG=true;
                BinaryS = Convert.ToString(s, 2);
                while (BinaryS.Length < r)
                {
                    BinaryS = "0" + BinaryS;
                }
                foreach (char item in BinaryS)
                {
                    if (item == '1')
                        w++;
                }
                if (w >= d - 1)
                {
                    foreach (var item in Cmas)
                    {
                        dmin = 0;
                        for(int i=0; i<item.Length;i++)
                        {
                            if (item[i] != BinaryS[i]) dmin++;
                        }
                        if (dmin < d - 2) isG = false;
                    }
                    if (isG) Cmas.Add(BinaryS);
                }
            }
            return Cmas;
         }

        public double fact(int x, int q)
        {
            double res=1;
            for (int i = q+1; i <= x; i++)
            {
                res *= i;
            }
            return res;
        }

        public int[,] BuildGmatrix(List<string> Cmatrix)
        {
            int[,] intWmatrix = new int[Cmatrix.Count, Cmatrix.Count + Cmatrix[0].Length];
            for (int i = 0; i < Cmatrix.Count; i++)
            {
                intWmatrix[i, i] = 1;
            }

            for (int i = 0; i < Cmatrix.Count; i++)
            {
                for (int j = 0; j < Cmatrix[0].Length; j++)
                {
                    intWmatrix[i, j + Cmatrix.Count] = (Cmatrix[i][j] - 48);
                }
            }
            return intWmatrix;
        }

        public int[] GetEncode(int[,] GMattrix, string input)
        {
            int[] res = new int[GMattrix.Length/input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1')
                {
                    for (int j = 0; j < (GMattrix.Length / input.Length); j++)
                    {
                        res[j] += GMattrix[i, j];
                        res[j] = res[j] % 2;
                    }
                }
            }
            return res;
        }
    }
}
