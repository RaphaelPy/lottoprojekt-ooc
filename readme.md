# 🎯 Schulprojekt: Lotto-Programm mit GUI

**Fach:** Informatik  
**Thema:** Entwicklung eines Lotto-Programms mit grafischer Benutzeroberfläche  
**Sprache:** C#  
**Technologie:** Windows Forms / WPF  
**Zeitraum:** 13 Kalendertage 
**Teamgröße:** 3 Personen  

---

## 👥 Rollenverteilung

### 👤 Luca Pelka (A)– GUI-Entwicklung
- Umsetzung der Oberfläche mit Windows Forms / WPF  
- Layoutgestaltung: Fenster, Buttons, Eingabefelder, Ergebnisanzeige  
- Eventhandling: Ziehungs-Button, Reset-Funktion  
- Design der Benutzerführung  

### 👤 Alexander Schneider (B) – Lotto-Logik
- Ziehung von 6 zufälligen, eindeutigen Zahlen (1–49)  
- Vergleich der Benutzerzahlen mit den gezogenen Zahlen  
- Gewinnermittlung: Treffer zählen und Gewinnklasse zuordnen  
- Eingabeprüfung auf Format, Anzahl und Bereich  

### 👤 Raphael Kreisel (C)– Integration, Validierung & Testing
- Implementierung der Eingabevalidierung (z. B. keine Duplikate)  
- Mitentwicklung der Gewinnlogik & Testszenarien  
- Integration von GUI ↔ Logik (Datenaustausch prüfen)  
- Fehleranalyse & -dokumentation  
- Kommentierung & Refactoring des Codes  
- Erstellung der Doku und Präsentationsmaterialien  

---

## 🗓️ Zeitplan

| 📅 Tag           | 🕒 Zeit | Aufgaben |
|------------------|--------|----------|
| **Mi, 4. Juni**  | 3h     | Projektplanung, Rollenverteilung, erste Ziehungs-Logik (B), GUI-Konzept (A), Validierungsideen (C) |
| **Fr, 6. Juni**  | 1.5h   | Ziehung vervollständigt (B), GUI mit Buttons/Eingabe begonnen (A), Validierungscode + Tests gestartet (C) |
| **Mo, 9. Juni**  | 2h     | Gewinnermittlung programmiert (B), GUI-Ziehung implementiert (A), Eingabeprüfung und Testfälle umgesetzt (C) |
| **Mi, 11. Juni** | 3h     | Ergebnisanzeige + Reset (A), Gewinnklassen finalisiert (B), Integration & Fehlerprotokoll (C) |
| **Fr, 13. Juni** | 1h     | Bugfixing & Feinschliff (alle), Layout verbessern (A), Code kommentieren (B), Reset testen & Fehler sammeln (C) |


---

## ✅ Funktionsumfang

- Eingabe von 6 Zahlen (1–49) über die GUI  
- Ziehung von 6 eindeutigen Zufallszahlen  
- Sortierte Anzeige der Ziehung  
- Gewinnermittlung (2–6 Richtige)  
- Eingabevalidierung & Fehlermeldungen  
- Reset-Button zur Neueingabe  
- Benutzerfreundliche, stabile Oberfläche  

---

## 🐞 Fehler & Lösungen

| ❌ Fehler | 💡 Lösung |
|----------|-----------|
| ❌ Fehler                                                   | 💡 Lösung                                                                          |
| ---------------------------------------------------------- | ---------------------------------------------------------------------------------- |
| `Random` erzeugte gleiche Zahlen bei mehreren Ziehungen    | Eine einzige `Random`-Instanz in Klasse gespeichert, nicht in Methode neu erstellt |
| Projekt lässt sich auf anderem PC nicht starten            | `.NET-Version` in `.csproj` vereinheitlicht (z. B. `.NET 6.0`)                     |
| `ObservableCollection` wurde nicht aktualisiert            | `Clear()` vergessen → Zahlen hingen am alten Zustand                               |
| Button sieht auf dunklem Design nicht gut aus              | `Background` und `Foreground` manuell angepasst                                    |
| Schriftart wirkt komisch oder zu groß                      | Einheitliche `FontFamily` und `FontSize` für alle Controls gesetzt                 |
| Fehlermeldung zu allgemein („Fehler“)                      | Aussagekräftige Meldungen wie „Bitte nur Zahlen von 1–49 eingeben“ formuliert      |
| Zufallszahlen im Debugmodus immer gleich                   | `Random` mit Zeitstempel initialisiert (`new Random()`)                            |
| Fehlermeldung in MessageBox erscheint hinter Fenster       | `MessageBoxOptions.DefaultDesktopOnly` oder Topmost-Fenster verwendet              |
| Eingabe mit Punkt statt Komma gemacht                      | Hinweis eingefügt: „Bitte ohne Komma / Punkt“                                      |
| Eingaben können per Copy+Paste manipuliert werden          | `PreviewTextInput`-Event genutzt, um nur Ziffern zu erlauben                       |
| GUI-Layout verschiebt sich bei Fenstergröße                | Responsive Design mit `Grid` + `RowDefinition` / `ColumnDefinition` statt `Canvas` |
| Zahleneingabe mit Maus oder Tastatur uneinheitlich         | Nur Textfelder erlaubt, MouseWheel deaktiviert                                     |
| Zahl wird erst nach Fokusverlust verarbeitet               | Binding mit `UpdateSourceTrigger=PropertyChanged` gesetzt                          |
| Zuviel Code im CodeBehind                                  | ViewModel mit MVVM-Light erstellt und Datenbindung verwendet                       |
| ViewModel nicht mit GUI verbunden                          | `DataContext` im `MainWindow.xaml.cs` gesetzt                                      |
| Code schwer lesbar                                         | Methoden mit sprechenden Namen + XML-Doku-Kommentare eingeführt                    |
| Ziehung funktioniert nicht nach Reset                      | Zustandsvariable (z. B. `hasDrawn`) beim Reset vergessen zurückzusetzen            |
| Projekt startet im falschen Fenster                        | `StartupUri="MainWindow.xaml"` in `App.xaml` korrigiert                            |
| Ressourcen (Farben, Styles) nur lokal gesetzt              | `ResourceDictionary` in `App.xaml` eingebunden für globales Styling                |
| Teammitglied hat versehentlich `.vs/` gepusht              | `.gitignore` um `.vs/`, `*.suo`, `*.user` ergänzt                                  |
| Missverständnisse beim Funktionszustand                    | Kleine GUI-Indikatoren genutzt wie `TextBlock "Ziehung durchgeführt!"`             |
| Keine Bestätigung bei Reset                                | Abfrage mit `MessageBox.Show("Wirklich zurücksetzen?", YesNo)` eingebaut           |
| ViewModel-Update zu spät sichtbar                          | Binding-Probleme durch fehlendes `INotifyPropertyChanged` behoben                  |
| Kommentare inkonsistent oder auf Deutsch/Englisch gemischt | Gemeinsamer Stil (z. B. Deutsch in UI + Codekommentare auf Englisch)               |
| Unit Tests vergessen                                       | Mini-Testmethoden für Ziehung/Validierung mit `Debug.Assert()` hinzugefügt         |
| Teammitglied hat versehentlich Branch gelöscht             | GitHub-Einstellungen → „Branch protection rules“ gesetzt                           |
| Feature-Fertigstellung nicht angekündigt                   | Regel: Nach Fertigstellung → `pull request` + Slack/Teams-Nachricht                |
| Nicht erkannt, dass Eingabefeld leer blieb                 | Prüfung mit `string.IsNullOrWhiteSpace()` vor Verarbeitung                         |
| Git-Merge-Konflikt in `.csproj`-Datei                      | Konflikt händisch gelöst, dann Build getestet                                      |
| Animation ruckelt                                          | Übergänge mit `Storyboard` + `DoubleAnimation` flüssiger gestaltet                 |
| Nicht alle Fehler sichtbar                                 | `StatusBar` unten im Fenster für Statusmeldungen eingeführt                        |
| Eingabefeld wurde zu klein beim Text                       | `MinWidth` gesetzt oder automatische Breite aktiviert                              |
| Wichtige Codezeilen durch `Undo` gelöscht                  | Git-History oder `Ctrl+Z` mit VS Code Timeline genutzt                             |
| Projekt auf falschem Branch weiterentwickelt               | Branch gemerged & Rebase erklärt, danach Feature-Branches besser organisiert       |


---

## 📘 Fazit

> Durch dieses Projekt haben wir gelernt, wie ein vollständiges Softwareprojekt strukturiert aufgebaut wird – von der grafischen Oberfläche über die Logik bis hin zur Fehlerbehandlung.  
> Die Zusammenarbeit in getrennten Rollen hat es uns ermöglicht, parallel zu arbeiten und uns gegenseitig abzustimmen. Besonders hilfreich war das gezielte Testen und die Dokumentation der Fehler.  
> Am Ende haben wir ein stabiles, funktionales und optisch verständliches Lotto-Programm entwickelt.

---

## 📎 Screenshots & Präsentation

*(Hier könnt ihr Screenshots der GUI und einen Link zu eurer Präsentation einfügen.)*

---

## 📂 Dateistruktur (Beispiel für GitHub o.ä.)

