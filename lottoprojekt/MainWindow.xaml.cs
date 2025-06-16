using System;                         // Grundlegende Typen und Methoden
using System.Collections.Generic;     // Queue<T>, Dictionary<K,V>
using System.Linq;                    // LINQ-Erweiterungsmethoden
using System.Windows;                 // WPF-Basisklassen (Window, MessageBox)
using System.Windows.Controls;        // WPF-Steuerelemente (Button, TextBox)
using System.Windows.Media;           // Brushes

namespace LottoSpielfeld
{
    // Code-Behind für das Hauptfenster
    public partial class MainWindow : Window
    {
        private const int MaxZahl = 49;           // Höchste Zahl im Lotto
        private const int MaxAuswahl = 6;         // Maximal auswählbare Zahlen
        private const decimal TicketPreis = 15m;  // Preis pro Tippfeld

        private readonly Queue<int> ausgewaehlt = new();          // Warteschlange für Auswahl
        private readonly Dictionary<int, Button> buttonMap = new(); // Map Zahl → Button
        private readonly Random rnd = new();                      // Zufallszahlengenerator

        private int? superzahlManuell = null;  // Optional manuell gesetzte Superzahl

        // Konstruktor: initialisiert Komponenten und erzeugt das Spielfeld
        public MainWindow()
        {
            InitializeComponent();  // Lädt XAML-Definition
            ErzeugeSpielfeld();     // Baut die Zahl-Buttons dynamisch
        }

        // Erzeugt 49 Buttons mit Zahl und rundem Style
        private void ErzeugeSpielfeld()
        {
            var style = (Style)FindResource("NumberButtonStyle"); // Holt Style aus XAML

            for (int i = 1; i <= MaxZahl; i++)
            {
                var btn = new Button
                {
                    Content = i.ToString(),    // Anzeigen der Zahl
                    Tag = i,                   // Speichern der Zahl
                    Style = style              // Anwenden des runden Styles
                };
                btn.Click += Zahl_Click;        // Klick-Eventhandler
                ButtonGrid.Children.Add(btn);   // Einfügen ins UniformGrid
                buttonMap[i] = btn;             // In Map speichern
            }
        }

        // Klick auf eine Zahl: auswäh-len bzw. abwählen
        private void Zahl_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;           // Angeclickter Button
            int zahl = (int)btn.Tag;            // Gelesene Zahl aus Tag

            if (ausgewaehlt.Contains(zahl))
            {
                // Ist bereits ausgewählt → entfernen
                var neueQueue = new Queue<int>(ausgewaehlt.Where(x => x != zahl));
                ausgewaehlt.Clear();
                foreach (var z in neueQueue) ausgewaehlt.Enqueue(z);
                btn.ClearValue(Button.BackgroundProperty); // Hintergrund zurücksetzen
            }
            else
            {
                // Noch nicht ausgewählt, aber schon 6 in der Queue?
                if (ausgewaehlt.Count >= MaxAuswahl)
                {
                    int entfernt = ausgewaehlt.Dequeue();       // Älteste Zahl entfernen
                    buttonMap[entfernt].ClearValue(Button.BackgroundProperty);
                }

                ausgewaehlt.Enqueue(zahl);              // Neue Zahl hinzufügen
                btn.Background = Brushes.LightGreen;    // Markierungs-Farbe
            }
        }

        // Zufällige Auswahl von 6 verschiedenen Zahlen
        private void ZufallAuswahl_Click(object sender, RoutedEventArgs e)
        {
            // Alte Markierungen entfernen
            foreach (var z in ausgewaehlt)
                buttonMap[z].ClearValue(Button.BackgroundProperty);
            ausgewaehlt.Clear();

            // 6 Zufallszahlen aus 1–49
            var zahlen = Enumerable.Range(1, MaxZahl)
                                   .OrderBy(_ => rnd.Next())
                                   .Take(MaxAuswahl);

            // In Queue packen und optisch markieren
            foreach (var z in zahlen)
            {
                ausgewaehlt.Enqueue(z);
                buttonMap[z].Background = Brushes.LightGreen;
            }
        }

        // Öffnet InputBox für manuelle Superzahl
        private void BtnSuperzahl_Click(object sender, RoutedEventArgs e)
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Gib eine Superzahl (1–49) ein oder leer lassen:",
                "Superzahl", "");

            if (int.TryParse(input, out int s) && s >= 1 && s <= MaxZahl)
            {
                superzahlManuell = s;               // Manuell gesetzte Superzahl speichern
                MessageBox.Show($"Superzahl gesetzt: {s}");
            }
            else if (string.IsNullOrWhiteSpace(input))
            {
                superzahlManuell = null;            // Keine Superzahl
                MessageBox.Show("Keine Superzahl gewählt.");
            }
            else
            {
                MessageBox.Show("Ungültige Eingabe."); // Fehlerhinweis
            }
        }

        // Setzt alles zurück auf Startzustand
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            // Entfernt Markierungen aller ausgewählten Buttons
            foreach (var z in ausgewaehlt)
                buttonMap[z].ClearValue(Button.BackgroundProperty);

            ausgewaehlt.Clear();       // Queue leeren
            superzahlManuell = null;   // Manuelle Superzahl löschen
            TxtGewinn.Text = "";       // Anzeige leeren
            TxtGesamtGewinn.Text = "";
        }

        // Startet die Ziehung über die angegebene Rundenanzahl
        private void ZiehungStarten_Click(object sender, RoutedEventArgs e)
        {
            // Validierung: exakt 6 Zahlen ausgewählt?
            if (ausgewaehlt.Count != MaxAuswahl)
            {
                MessageBox.Show("Bitte genau 6 Zahlen auswählen!");
                return;
            }

            // Validierung: gültige Rundenanzahl
            if (!int.TryParse(TxtRunden.Text, out int runden) || runden < 1)
            {
                MessageBox.Show("Bitte eine gültige Rundenanzahl eingeben!");
                return;
            }

            var userZahlen = ausgewaehlt.ToList();  // Liste der Tip-Zahlen
            decimal gesamtGewinn = 0;
            string ergebnisse = $"Gespielte Zahlen: {string.Join(", ", userZahlen.OrderBy(n => n))}\n\n";

            // Schleife über alle Runden
            for (int i = 1; i <= runden; i++)
            {
                var gezogen = Ziehung(out int superzahl); // Neue Ziehung
                if (superzahlManuell.HasValue)
                    superzahl = superzahlManuell.Value;    // Manuelle Superzahl überschreiben

                var treffer = userZahlen.Intersect(gezogen).ToList(); // Schnittmengen zählen
                bool hatSuper = userZahlen.Contains(superzahl);       // Superzahl-Treffer?
                decimal gewinn = GewinnRegeln.BerechneGewinn(treffer.Count, hatSuper);

                gesamtGewinn += gewinn;   // aufsummieren
                ergebnisse += $"Runde {i}: {treffer.Count} Treffer" +
                              $"{(hatSuper ? " mit Superzahl" : "")} – Gewinn: {gewinn:C}\n";
            }

            decimal kosten = runden * TicketPreis;                     // Gesamtkosten
            decimal netto = gesamtGewinn - kosten;                     // Nettobilanz

            // Anzeige in den Textblöcken aktualisieren
            TxtGewinn.Text = gesamtGewinn > 0
                ? $"🏆 Gesamtgewinn: {gesamtGewinn:C}"
                : "Leider kein Gewinn 😢";

            TxtGesamtGewinn.Text = $"Kosten: {kosten:C} – Netto: {(netto >= 0 ? "+" : "")}{netto:C}";

            // Ausführliches Ergebnis-Popup
            MessageBox.Show(ergebnisse +
                            $"\nGesamtgewinn: {gesamtGewinn:C}\nKosten: {kosten:C}\nNetto: {netto:C}");
        }

        // Führt eine einzelne Lotto-Ziehung inkl. Superzahl durch
        private List<int> Ziehung(out int superzahl)
        {
            var alle = Enumerable.Range(1, MaxZahl).ToList(); // Alle Zahlen 1–49
            var gezogen = new List<int>();

            // 6 eindeutige Zahlen ziehen
            while (gezogen.Count < MaxAuswahl)
            {
                int idx = rnd.Next(alle.Count);
                gezogen.Add(alle[idx]);
                alle.RemoveAt(idx);
            }

            // Superzahl aus dem Rest ziehen
            superzahl = alle[rnd.Next(alle.Count)];
            gezogen.Sort();  // Sortieren für bessere Lesbarkeit
            return gezogen;
        }
    }

    // Hilfsklasse für Gewinnberechnung anhand Treffer und Superzahl
    public static class GewinnRegeln
    {
        public static decimal BerechneGewinn(int treffer, bool superTreffer) =>
            (treffer, superTreffer) switch
            {
                (2, true)  =>   6m,
                (3, false) =>  11m,
                (3, true)  =>  21m,
                (4, false) =>  50m,
                (4, true)  => 190m,
                (5, false) => 4000m,
                (5, true)  =>12000m,
                (6, false) =>1000000m,
                (6, true)  =>12000000m,
                _          =>    0m,
            };
    }
}
