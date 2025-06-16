using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LottoSpiel
{
    public partial class MainWindow : Window
    {
        // Konstante Werte
        private const int MaxZahl = 49;         // Höchste Zahl im Spielfeld
        private const int MaxAuswahl = 6;       // Spieler darf 6 Zahlen wählen
        private const decimal TicketPreis = 15; // Preis pro Runde

        // Speichert ausgewählte Zahlen
        private Queue<int> ausgewaehlt = new();

        // Button-Zuordnung für Spielfeld und Superzahl
        private Dictionary<int, Button> buttonMap = new();
        private Dictionary<int, Button> superzahlButtons = new();

        // Wenn der Spieler eine Superzahl wählt
        private int? superzahlManuell = null;

        public MainWindow()
        {
            InitializeComponent();
            ErzeugeSpielfeld();
            ErzeugeSuperzahlFeld();
        }

        // Erstellt das Zahlenfeld 1–49
        private void ErzeugeSpielfeld()
        {
            for (int i = 1; i <= MaxZahl; i++)
            {
                var btn = new Button
                {
                    Content = i.ToString(),
                    Tag = i,
                    Margin = new Thickness(2)
                };
                btn.Click += Zahl_Click;
                ButtonGrid.Children.Add(btn);
                buttonMap[i] = btn;
            }
        }

        // Erstellt Buttons für Superzahl 0–9
        private void ErzeugeSuperzahlFeld()
        {
            for (int i = 0; i <= 9; i++)
            {
                var btn = new Button
                {
                    Content = i.ToString(),
                    Tag = i,
                    Width = 30,
                    Margin = new Thickness(2)
                };
                btn.Click += Superzahl_Click;
                SuperzahlGrid.Children.Add(btn);
                superzahlButtons[i] = btn;
            }
        }

        // Wenn ein Superzahl-Button geklickt wird
        private void Superzahl_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            int zahl = (int)btn.Tag;

            superzahlManuell = zahl;

            // Nur 1 Button markieren
            foreach (var b in superzahlButtons.Values)
                b.ClearValue(Button.BackgroundProperty);

            btn.Background = Brushes.Orange;
        }

        // Wenn eine Zahl (1–49) geklickt wird
        private void Zahl_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            int zahl = (int)btn.Tag;

            if (ausgewaehlt.Contains(zahl))
            {
                ausgewaehlt = new Queue<int>(ausgewaehlt.Where(x => x != zahl));
                btn.ClearValue(Button.BackgroundProperty);
            }
            else
            {
                if (ausgewaehlt.Count >= MaxAuswahl)
                {
                    int entfernt = ausgewaehlt.Dequeue();
                    buttonMap[entfernt].ClearValue(Button.BackgroundProperty);
                }

                ausgewaehlt.Enqueue(zahl);
                btn.Background = Brushes.LightGreen;
            }
        }

        // Zufällige Auswahl von 6 Zahlen
        private void ZufallAuswahl_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            var zahlen = Enumerable.Range(1, MaxZahl).OrderBy(_ => rnd.Next()).Take(MaxAuswahl).ToList();

            foreach (var z in ausgewaehlt)
                buttonMap[z].ClearValue(Button.BackgroundProperty);

            ausgewaehlt.Clear();

            foreach (var z in zahlen)
            {
                ausgewaehlt.Enqueue(z);
                buttonMap[z].Background = Brushes.LightGreen;
            }
        }

        // Wenn „Ziehung starten“ gedrückt wird
        private void ZiehungStarten_Click(object sender, RoutedEventArgs e)
        {
            if (ausgewaehlt.Count != MaxAuswahl)
            {
                MessageBox.Show("Bitte genau 6 Zahlen auswählen!");
                return;
            }

            if (!int.TryParse(TxtRunden.Text, out int runden) || runden < 1)
            {
                MessageBox.Show("Gib eine gültige Rundenzahl ein!");
                return;
            }

            var userZahlen = ausgewaehlt.ToList();
            decimal gesamtGewinn = 0;
            string ergebnisse = $"Gespielte Zahlen: {string.Join(", ", userZahlen.OrderBy(n => n))}\n";

            if (superzahlManuell.HasValue)
                ergebnisse += $"Superzahl: {superzahlManuell.Value}\n\n";
            else
                ergebnisse += "Keine Superzahl gewählt.\n\n";

            var statistik = new Dictionary<(int Treffer, bool Super), int>();

            for (int i = 1; i <= runden; i++)
            {
                var gezogen = Ziehung(out int superzahl);
                var treffer = userZahlen.Intersect(gezogen).ToList();
                bool hatSuper = superzahlManuell.HasValue && superzahl == superzahlManuell.Value;
                decimal gewinn = GewinnRegeln.BerechneGewinn(treffer.Count, hatSuper);
                gesamtGewinn += gewinn;

                var key = (treffer.Count, hatSuper);
                if (!statistik.ContainsKey(key))
                    statistik[key] = 0;
                statistik[key]++;

                ergebnisse += $"Runde {i}: {treffer.Count} Treffer{(hatSuper ? " + Superzahl" : "")} – Gewinn: {gewinn:C}\n";
            }

            decimal kosten = runden * TicketPreis;
            decimal netto = gesamtGewinn - kosten;

            // Trefferstatistik ausgeben
            ergebnisse += "\nTrefferstatistik:\n";
            foreach (var kv in statistik.OrderByDescending(k => k.Key.Treffer))
            {
                string zeile = $"{kv.Value}× {kv.Key.Treffer} Treffer";
                if (kv.Key.Super) zeile += " + Superzahl";
                ergebnisse += zeile + "\n";
            }

            // Ergebnis anzeigen
            TxtGewinn.Text = gesamtGewinn > 0
                ? $" Gesamtgewinn: {gesamtGewinn:C}"
                : "Leider kein Gewinn";

            TxtGesamtGewinn.Text = $"Kosten: {kosten:C} – Netto: {(netto >= 0 ? "+" : "")}{netto:C}";
            TxtErgebnisseBox.Text = ergebnisse;
            TxtErgebnisseBox.ScrollToEnd();
        }

        // Eine Lotto-Ziehung durchführen
        private List<int> Ziehung(out int superzahl)
        {
            var rnd = new Random();
            var alle = Enumerable.Range(1, MaxZahl).ToList();
            var gezogen = new List<int>();

            while (gezogen.Count < MaxAuswahl)
            {
                int index = rnd.Next(alle.Count);
                gezogen.Add(alle[index]);
                alle.RemoveAt(index);
            }

            superzahl = rnd.Next(0, 10); // 0–9
            gezogen.Sort();
            return gezogen;
        }

        // Alles zurücksetzen
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            foreach (var z in ausgewaehlt)
                buttonMap[z].ClearValue(Button.BackgroundProperty);

            foreach (var b in superzahlButtons.Values)
                b.ClearValue(Button.BackgroundProperty);

            ausgewaehlt.Clear();
            superzahlManuell = null;
            TxtGewinn.Text = "";
            TxtGesamtGewinn.Text = "";
            TxtErgebnisseBox.Text = "";
        }
    }

    // Gewinnregeln für Lotto
    public static class GewinnRegeln
    {
        public static decimal BerechneGewinn(int treffer, bool super)
        {
            return (treffer, super) switch
            {
                (2, true) => 6,
                (3, false) => 11,
                (3, true) => 21,
                (4, false) => 50,
                (4, true) => 190,
                (5, false) => 4000,
                (5, true) => 12000,
                (6, false) => 1000000,
                (6, true) => 12000000,
                _ => 0
            };
        }
    }
}
