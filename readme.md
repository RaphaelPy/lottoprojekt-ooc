# 🎯 Schulprojekt: Lotto-Programm mit GUI

**Fach:** Informatik  
**Thema:** Entwicklung eines Lotto-Programms mit grafischer Benutzeroberfläche  
**Sprache:** C#  
**Technologie:** Windows Forms / WPF  
**Zeitraum:** 13 Kalendertage 
**Teamgröße:** 3 Personen  

---

## 👥 Rollenverteilung

### 👤 Luca Pelka (A) – GUI-Entwicklung
- Umsetzung der Oberfläche mit Windows Forms / WPF  
- Layoutgestaltung: Fenster, Buttons, Eingabefelder, Ergebnisanzeige  
- Eventhandling: Ziehungs-Button, Reset-Funktion  
- Design der Benutzerführung  

### 👤 Alexander Schneider (B) – Lotto-Logik
- Ziehung von 6 zufälligen, eindeutigen Zahlen (1–49)  
- Vergleich der Benutzerzahlen mit den gezogenen Zahlen  
- Gewinnermittlung: Treffer zählen und Gewinnklasse zuordnen  
- Eingabeprüfung auf Format, Anzahl und Bereich  

### 👤 Raphael Kreisel (C) – Integration, Validierung & Testing
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
| **Mo, 16. Juni** | 1.5h   | Dokumentation & Präsentation vorbereitet (alle), Code sauber übergeben (alle) |

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

| Nr | ❌ Fehlerbeschreibung | 💡 Lösung |
|----|------------------------|-----------|
| 1  | Strings wurden übereinander angezeigt | Vor jeder neuen Ziehung wird `TxtErgebnisseBox.Clear();` aufgerufen, um alte Texte zu löschen. |
| 2  | Buttons überlappten sich in der GUI | Wir haben ein `UniformGrid` für Zahlen und `WrapPanel` für Steuerungselemente genutzt, was Überlappungen verhindert. |
| 3  | Es wurde „Super“ als Gewinnklasse angezeigt, obwohl dies nicht vorgesehen war | Wir haben die Bedingung zur Anzeige der Superzahl überarbeitet und zeigen diese nur bei aktiver Auswahl an. |
| 4  | GitHub funktionierte bei einem Teammitglied nicht | Wir haben lokal weitergearbeitet und Dateien manuell synchronisiert. Später wurden alle Änderungen erneut gepusht. |
| 5  | Zufallszahlen funktionierten nicht korrekt (z. B. doppelt oder gleich) | `Random` wird nun als Klassenvariable gespeichert, um Wiederholungen bei schneller Ausführung zu vermeiden. |
| 6  | Nach Zahlenauswahl wurde der Hintergrund weiß | Der Hintergrund wird nun gezielt mit `Brushes.LightGreen` gefärbt, Weiß wurde entfernt. |
| 7  | Gewinn-Textfeld konnte nicht gescrollt werden | Das Textfeld `TxtErgebnisseBox` hat `VerticalScrollBarVisibility="Auto"` erhalten und ist auf `IsReadOnly="True"` gesetzt. |
| 8  | Visual Studio funktionierte bei einer Person nicht mehr | Die IDE wurde neu installiert/repariert. Danach konnte das Projekt wieder geladen werden. |
| 9  | GitHub setzte den Code auf eine ältere Version zurück | Wir haben die Versionen verglichen, Konflikte manuell gelöst und danach neu gepusht. |
| 10 | Dateien konnten nicht mehr von GitHub geladen werden | Repository wurde lokal neu geklont, wichtige Dateien vorher manuell gesichert und übertragen. |


---

