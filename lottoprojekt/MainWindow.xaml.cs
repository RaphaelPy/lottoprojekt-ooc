using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LottoProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            lstTreffer.Items.Clear();
            int[] meineTreffer = new int[7];
            int meingeld = 2000;
            Random userRandom = new Random();
            int[] meineZahlen = new int[6];

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < meineZahlen.Length; j++)
                {
                    int meineZahl = userRandom.Next(1, 50);
                    while (meineZahlen.Take(j).Contains(meineZahl))
                    {
                        meineZahl = userRandom.Next(1, 50);
                    }
                    meineZahlen[j] = meineZahl;
                }

                Lotto meinlotto = new Lotto(meineZahlen);
                meingeld -= 5;

                switch (meinlotto.anzahlTreffer)
                {
                    case 3: meingeld += 10; break;
                    case 4: meingeld += 42; break;
                    case 5: meingeld += 3508; break;
                    case 6: meingeld += 1000000; break;
                    default: meingeld -= 5; break;
                }

                meineTreffer[meinlotto.anzahlTreffer]++;
            }

            for (int i = 0; i < meineTreffer.Length; i++)
            {
                lstTreffer.Items.Add($"{i:00} Treffer: {meineTreffer[i]} Mal");
            }

            txtGeld.Text = $"Endguthaben: {meingeld} €";
        }
    }

    public class Lotto
    {
        public int[] gezogen { get; private set; } = new int[6];
        public int anzahlTreffer { get; private set; }

        public Lotto(int[] tipp)
        {
            Ziehen();
            TrefferBerechnen(tipp);
        }

        private void Ziehen()
        {
            Random rnd = new Random();
            for (int i = 0; i < gezogen.Length; i++)
            {
                int zahl = rnd.Next(1, 50);
                while (gezogen.Take(i).Contains(zahl))
                {
                    zahl = rnd.Next(1, 50);
                }
                gezogen[i] = zahl;
            }
        }

        private void TrefferBerechnen(int[] tipp)
        {
            anzahlTreffer = tipp.Count(z => gezogen.Contains(z));
        }
    }
}
