# Get-SLOC PowerShell Script

Das `Get-SLOC` PowerShell-Skript zählt die Anzahl der Source Lines of Code (SLOC) in einem angegebenen Verzeichnis und dessen Unterverzeichnissen. Es durchsucht Dateien mit den Erweiterungen `.c`, `.h`, und `.cpp` und gibt die Gesamtanzahl der Codezeilen aus.

## Autor

- **Michael Epper**
- **Datum:** 14.06.2023
- **Version:** 1.0

## Funktionalität

### Synopsis

Das Skript zählt die Anzahl der Source Lines of Code (SLOC) in den angegebenen Ordnern und Unterordnern.

### Beschreibung

Die Funktion `Get-SLOC` durchsucht einen Ordner und seine Unterordner nach Dateien mit den Dateitypen `.c`, `.h`, und `.cpp`. Anschließend werden die Codezeilen in diesen Dateien gezählt, wobei leere Zeilen und Kommentare ignoriert werden.

### Parameter

- **location**: Der Pfad zu dem Ordner, in dem die Codezeilen gezählt werden sollen. Dieser Parameter ist erforderlich.

### Beispiele

1. **Beispiel 1:**
   ```powershell
   Get-SLOC -location "C:\Users\micha\OneDrive - Kt. SG BLD\KSB\Informatik Module\M122\test"

2. **Beispiel 2:**
   ```powershell
    Get-SLOC -location "C:\test"


