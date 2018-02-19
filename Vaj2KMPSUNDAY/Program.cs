using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaj2KMPSUNDAY
{
    class Program
    {
        static int[] kmpNextArray(string niz)
        {
            int i = 0;
            int j = -1;
            int[] kmpNext = new int[niz.Length + 1]; // -1 0 0 0 1...
            kmpNext[0] = j;
            while(i < niz.Length)
            {
                while(j >=0 && niz[i] != niz[j])
                {
                    j = kmpNext[j];
                }
                i++;
                j++;
                kmpNext[i] = j;
            }


            return kmpNext;
        }
        static int[] kmpSearch(string iskaniNiz, string celotenNiz, int[] kmpNext)
        {
            List<int> tmp = new List<int>();
            int i = 0;
            int j = 0;
            int m = iskaniNiz.Length;
            int n = celotenNiz.Length;
            
            while(j < n)
            {
                if (celotenNiz[i + j] != iskaniNiz[i])
                {
                    i = 0;
                    j += i - kmpNext[i];
                }
                if (j == n)
                    break;
                if (celotenNiz[i + j] == iskaniNiz[i])
                    i++;
                if (i == m)
                {
                    i = 0;
                    tmp.Add(j - i);
                    j += m;

                }
            }
            return tmp.ToArray();
        }
        static int[] BCHArray(string niz)
        {
            //int i = 0;
            int m = niz.Length;
            int[] BCH = new int[123];

            for (int i = 0; i < BCH.Length; i++)
            {
                BCH[i] = m +1;
            }
            for(int i = 0; i < niz.Length; i++)
            {
                BCH[niz[i]] = niz.Length - i;
            }

            return BCH; 
        }
        static int[] sundayAlgoritm(string niz, string besedilo, int[] BCH)
        {
            bool pomoc = false;
            List<int> tmp = new List<int>();
            for (int j = 0; j < besedilo.Length; j++)
            {
                for (int i = 0; i < niz.Length; i++)
                {
                    if (j + niz.Length >= besedilo.Length)
                    {
                        break;
                    }
                    if (niz[i] != besedilo[j + i])
                    {
                        j += BCH[besedilo[j + niz.Length]];
                        i = 0;
                        pomoc = false;
                    }
                    else
                    {
                        pomoc = true;
                    }
                }
                if (pomoc)
                {
                    tmp.Add(j);
                     j += BCH[besedilo[j + 1]];
                }
            }
            return tmp.ToArray();
        }
        static void Main(string[] args)
        {
            string iskaniNiz = args[1];
            string datoteka = args[2];
            int[] koncniR;

            //Console.WriteLine("Iskani niz: ");
            //for (int i = 0; i < iskaniNizArray.Length; i++)
            //{
            //    Console.Write(iskaniNizArray[i]);
            //}
            //Console.WriteLine();

            string fileContent = File.ReadAllText(datoteka);
            //Console.WriteLine("Celoten niz: " + fileContent);
            

            if (int.Parse(args[0]) == 0)
            {
                //KMP
                int[] kmpNext = kmpNextArray(iskaniNiz);
                //Console.WriteLine("KmpNext: ");
                //for (int i = 0; i < kmpNext.Length; i++)
                //{
                //    Console.Write(kmpNext[i] + " ");
                //}
                koncniR = kmpSearch(iskaniNiz, fileContent, kmpNext);
                izpis(koncniR);
                zapisVDaoteko(koncniR);
                

            }
            if (int.Parse(args[0]) == 1)
            {
                //SUNDAY
                int[] BCH = BCHArray(iskaniNiz);
                koncniR = sundayAlgoritm(iskaniNiz, fileContent, BCH);
                izpis(koncniR);
                zapisVDaoteko(koncniR);
            }

        }
        private static void izpis(int[] rez)
        {
            Console.WriteLine("Rez: ");
            foreach (int i in rez)
            {
                Console.Write(i + " ");
            }
        }
        private static void zapisVDaoteko(int[] B)
        {
            for (int i = 0; i < B.Length; i++)
            {
                using (FileStream fs = new FileStream("out.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(B[i] + " ");
                }
            }
        }
        private static void kmp(string str, string pat)
        {
            int m = pat.Length;
            int n = str.Length;


            
        }
    }
}
