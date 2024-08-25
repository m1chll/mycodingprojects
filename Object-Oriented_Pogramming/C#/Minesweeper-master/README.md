# Minesweeper Konsolenanwendung

Willkommen zum Minesweeper! Dieses Projekt wurde während meines Kurzpraktikums bei der Egeli Informatik AG in enger Zusammenarbeit mit @sophia entwickelt. Das Spiel bietet klassische Minesweeper-Action in der Konsole und enthält viele durchdachte Features, die das Spielerlebnis verbessern und erweitern.

## Hauptfunktionen

- **Anpassbare Schwierigkeitsgrade**: Wähle zwischen den Schwierigkeitsstufen leicht, mittel und schwer, um das Spiel individuell an deine Fähigkeiten anzupassen.
- **Highscore-Datenbank**: Deine besten Ergebnisse werden mithilfe des Entity Frameworks in einer SQL-Datenbank gespeichert. So kannst du deine Fortschritte beobachten und dich mit anderen Spielern vergleichen.
- **Detaillierter Timer**: Der Timer wurde besonders aufwendig entwickelt, da die Herausforderung darin bestand, den Cursor zwischen dem Timer und dem Eingabefeld hin- und herspringen zu lassen, ohne das Spielerlebnis zu beeinträchtigen.
- **Optimierte Spielfeld-Generierung**: Eine speziell entwickelte Datenstruktur sorgt dafür, dass das Spielfeld effizient und herausfordernd generiert wird, was die Spannung im Spiel erhöht.
- **Ingame-Sounds**: Um das Spielerlebnis noch spannender zu gestalten, wurden verschiedene Soundeffekte integriert, die das Spielgeschehen akustisch untermalen.

## Verwendete Technologien

- **C#** für die gesamte Spielentwicklung
- **.NET Core** für die Konsolenanwendung
- **Entity Framework Core** zur Verwaltung der Highscore-Datenbank
- **SQL-Datenbank** zur Speicherung der besten Spielergebnisse
