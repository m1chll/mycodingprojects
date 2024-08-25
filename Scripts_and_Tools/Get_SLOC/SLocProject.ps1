<#
Get-SLOC
Autor: Michael Epper
Datum 14.06.2023
Version: 1.0
Beispiel: Get-SLOC -location "C:\test"
#>

<#
.SYNOPSIS
Zaehlt die Anzahl der Source Lines of Code (SLOC) im angegebenen Ordner.
.DESCRIPTION
Die Funktion Get-SLOC durchsucht einen Ordner und seine Unterordner nach Dateien mit den Dateitypen .c, .h und .cpp. Danach werden die Codezeilen gezaehlt und das Ergebnis ausgegeben.
.PARAMETER location
Der Parameter location erwartet einen gültigen Ordnerpfad als Eingabe.
.EXAMPLE
Get-SLOC -location "C:\Users\micha\OneDrive - Kt. SG BLD\KSB\Informatik Module\M122\test"
Die Funktion wird aufgerufen, um die Anzahl der Codezeilen im Ordner "C:\Users\micha\OneDrive - Kt. SG BLD\KSB\Informatik Module\M122\test" zu berechnen.
.EXAMPLE
Get-SLOC -location "C:\test"
Die Funktion wird aufgerufen, um die Anzahl der Codezeilen im Ordner "C:\test" zu berechnen.
#>


# Definition der Funktion Get-SLOC
function Get-SLOC {
    [CmdletBinding()]
    param (
        # Der Parameter location erwartet einen gültigen Ordnerpfad als Eingabe | Quelle: ChatGPT 3.5
        [Parameter(Mandatory = $true, Position = 0)]
        [ValidateScript({Test-Path $_ -PathType 'Container'})]
        [string]$location
    )

    # Liste der Dateitypen
    $dateitypen = @('.c', '.h', '.cpp')

    # Initialisierung der codezeilen Variable zum Zählen der Zeilen
    $codezeilen = 0

    # Durchsuchen aller Dateien im angegebenen Ordner und in allen Unterverzeichnissen
    $dateien = Get-ChildItem -Path $location -Recurse -File | Where-Object {$_.Extension -in $dateitypen}

    # Überprüfen jeder Datei mit den richtigen Dateierweiterungen
    foreach ($datei in $dateien) {
        # Datei öffnen und Inhalt in der Variable inhalt speichern
        $inhalt = Get-Content -Path $datei.FullName

        # Durchlaufen jeder Zeile 
        foreach ($zeile in $inhalt) {
            # Entfernen von führenden und abschliessenden Leerzeichen in der aktuellen Zeile
            $trimmedLine = $zeile.Trim()

            # Überprüfen ob die Zeile eine Codezeile oder leere Zeile ist | Quelle: ChatGPT 3.5
            if (-not $trimmedLine.StartsWith('//') -and
                -not $trimmedLine.StartsWith('/*') -and
                -not $trimmedLine.EndsWith('*/') -and
                -not [string]::IsNullOrWhiteSpace($trimmedLine))
            {
                # Codezeile um 1 erhöhen
                $codezeilen++
            }
        }
    }

    # Ergebnis ausgeben
    Write-Output "Im Projekt wurden $codezeilen Source Lines of Code (SLOC) gezaehlt."
}
