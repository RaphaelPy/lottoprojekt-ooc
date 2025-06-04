using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LottoProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int[] meineZahlen = { 1, 12, 34, 45, 7, 11 };

            //Lotto meinlotto = new Lotto(meineZahlen);
            //meinlotto.Ziehen();
            //meinlotto.Treffer(meineZahlen, meinlotto.gezogen);
            int[] meineTreffer = new int[7];
            int meingeld = 2000;

            Random userRandom = new Random();
            for (int i = 0; i < 1000; i++)
            {

                for (int j = 0; j < meineZahlen.Length; j++)
                {
                    int meineZahl = userRandom.Next(1, 50);
                    while (meineZahlen.Contains(meineZahl))
                    {
                        meineZahl = userRandom.Next(1, 50);
                    }
                    meineZahlen[j] = meineZahl;
                }

                Lotto meinlotto = new Lotto(meineZahlen);
                meingeld = meingeld - 5;

                switch (meinlotto.anzahlTreffer)
                {
                    case 3:
                        meingeld += 10;
                        break;
                    case 4:
                        meingeld += 42;
                        break;
                    case 5:
                        meingeld += 3508;
                        break;
                    case 6:
                        meingeld += 1000000;
                        break;
                    default:
                        meingeld -= 5;
                        break;

                }
                meineTreffer[meinlotto.anzahlTreffer]++;

            }
            for (int i = 0; i < meineTreffer.Length; i++)
            {
                Console.WriteLine($"{i:00} Treffer: {meineTreffer[i]} Mal");
            }
            Console.WriteLine($"Du hast noch {meingeld}");
        }
    }
}
