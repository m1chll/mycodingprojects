# Testkonzept Minesweeper

## Testkonzept für die GetDifficulty-Methode in Minesweeper:

1. **Ziel des Tests:**
   - Sicherstellen, dass die GetDifficulty-Methode korrekt auf die Benutzereingabe reagiert und die Schwierigkeitsstufe entsprechend zurückgibt.

2. **Testfälle:**
   a. **GetDifficulty_EasyInput_ReturnsEasy:**
      - Beschreibung: Überprüfen, ob die Methode korrekt "E" zurückgibt, wenn der Benutzer "E" für Easy eingibt.
   b. **GetDifficulty_MediumInput_ReturnsMedium:**
      - Beschreibung: Überprüfen, ob die Methode korrekt "M" zurückgibt, wenn der Benutzer "M" für Medium eingibt.
   c. **GetDifficulty_HardInput_ReturnsHard:**
      - Beschreibung: Überprüfen, ob die Methode korrekt "H" zurückgibt, wenn der Benutzer "H" für Hard eingibt.
   d. **GetDifficulty_InvalidInput_NothingHappens:**
      - Beschreibung: Überprüfen, ob die Methode keine weiteren Aktionen ausführt, wenn der Benutzer eine ungültige Eingabe tätigt.

3. **Testumgebung:**
   - Verwendung einer Unit-Test-Bibliothek (z. B. NUnit).
   - Mocking der Console-Ein- und -Ausgabe für die Simulation von Benutzereingaben und Ausgaben.
   - Einrichtung einer StringReader- und StringWriter-Kombination für die Simulation von Benutzereingaben und -ausgaben.

4. **Testdurchführung:**
   - Jeder Testfall wird unabhängig voneinander durchgeführt.
   - Vor jedem Testfall wird die Console-Ein- und -Ausgabe umgeleitet, um die Benutzereingaben und -ausgaben zu simulieren.
   - Nach jedem Testfall wird die Umleitung der Console-Ein- und -Ausgabe zurückgesetzt.
   - Die erwarteten Ergebnisse werden mit den tatsächlichen Ergebnissen verglichen und bei Übereinstimmung wird der Testfall als erfolgreich markiert.

5. **Berichterstattung:**
   - Die Tests wurden erfolgreich ausgeführt!


## Testkonzept für die GetDifficulty-Methode in Minesweeper:

1. **Ziel des Tests:**
   - Überprüfen, ob die Anzahl der Felder und Bomben korrekt ist, wenn der Benutzer "E", "M" oder "H" für Easy, Medium oder Hard drückt.

2. **Testfälle:**
   a. **GetDifficulty_EasyInput_ReturnsCorrectBoardAndBombCount:**
      - Beschreibung: Überprüfen, ob die Methode korrekt ein Spielfeld mit den spezifizierten Abmessungen für die leichte Schwierigkeitsstufe zurückgibt und die Anzahl der Bomben korrekt ist.
   b. **GetDifficulty_MediumInput_ReturnsCorrectBoardAndBombCount:**
      - Beschreibung: Überprüfen, ob die Methode korrekt ein Spielfeld mit den spezifizierten Abmessungen für die mittlere Schwierigkeitsstufe zurückgibt und die Anzahl der Bomben korrekt ist.
   c. **GetDifficulty_HardInput_ReturnsCorrectBoardAndBombCount:**
      - Beschreibung: Überprüfen, ob die Methode korrekt ein Spielfeld mit den spezifizierten Abmessungen für die schwierige Schwierigkeitsstufe zurückgibt und die Anzahl der Bomben korrekt ist.

3. **Testumgebung:**
   - Verwendung einer Unit-Test-Bibliothek (z. B. NUnit).
   - Mocking der Console-Ein- und -Ausgabe für die Simulation von Benutzereingaben und -ausgaben.
   - Einrichtung einer StringReader- und StringWriter-Kombination für die Simulation von Benutzereingaben und -ausgaben.

4. **Testdurchführung:**
   - Jeder Testfall wird unabhängig voneinander durchgeführt.
   - Vor jedem Testfall wird die Console-Ein- und -Ausgabe umgeleitet, um die Benutzereingaben und -ausgaben zu simulieren.
   - Nach jedem Testfall wird die Umleitung der Console-Ein- und -Ausgabe zurückgesetzt.
   - Die erwarteten Ergebnisse werden mit den tatsächlichen Ergebnissen verglichen und bei Übereinstimmung wird der Testfall als erfolgreich markiert.

5. **Berichterstattung:**
   - Die Tests wurden erfolgreich ausgeführt!


## Testkonzept für die RevealingAllNonBombFieldsWinsGame-Methode in Minesweeper:

1. **Ziel des Tests:**
   - Überprüfen, ob das Spiel als "Gewonnen" markiert wird, wenn alle nicht-Bomben-Felder aufgedeckt wurden.

2. **Testfälle:**
   a. **RevealingAllNonBombFieldsWinsGame_GameWon:**
      - Beschreibung: Überprüfen, ob das Spielstatus als "Gewonnen" markiert wird, nachdem alle nicht-Bomben-Felder aufgedeckt wurden.

3. **Testumgebung:**
   - Verwendung einer Unit-Test-Bibliothek (z. B. NUnit).
   - Instanziierung einer Game- und Gameboard-Objekte für die Durchführung des Tests.
   - Simulieren des Aufdeckens aller nicht-Bomben-Felder durch entsprechende Aufrufe der UpdateFields-Methode.

4. **Testdurchführung:**
   - Die UpdateFields-Methode wird aufgerufen, um alle nicht-Bomben-Felder aufzudecken.
   - Der Spielstatus wird überprüft, um sicherzustellen, dass das Spiel als "Gewonnen" markiert ist, nachdem alle nicht-Bomben-Felder aufgedeckt wurden.

5. **Berichterstattung:**
   - Die Tests wurden erfolgreich ausgeführt!

## Testkonzept für die Minesweeper-Spielmechanik:

1. **Ziel des Tests:**
   - Überprüfen, ob die Spielmechanik korrekt funktioniert und die verschiedenen Aktionen wie das Aufdecken von Feldern, das Platzieren und Entfernen von Flaggen ordnungsgemäß durchgeführt werden.

2. **Testfälle:**
   a. **TestRevealField_SuccessfulReveal:**
      - Beschreibung: Überprüfen, ob das Spiel den richtigen Status zurückgibt, nachdem ein Feld aufgedeckt wurde.
   b. **TestPlaceFlag_FlagPlacedSuccessfully:**
      - Beschreibung: Überprüfen, ob das Spielbrett eine Flagge erfolgreich platziert, wenn die entsprechende Aktion ausgeführt wird.
   c. **TestRemoveFlag_FlagRemovedSuccessfully:**
      - Beschreibung: Überprüfen, ob das Spielbrett eine Flagge erfolgreich entfernt, wenn die entsprechende Aktion ausgeführt wird.

3. **Testumgebung:**
   - Verwendung einer Unit-Test-Bibliothek (z. B. NUnit).
   - Instanziierung eines Gameboard-Objekts für die Durchführung der Tests.
   - Erstellung eines Spielbretts mit unterschiedlichen Konfigurationen für jeden Testfall.

4. **Testdurchführung:**
   - Für jeden Testfall wird die entsprechende Aktion ausgeführt (Aufdecken eines Feldes, Platzieren oder Entfernen einer Flagge).
   - Das Ergebnis der Aktion wird überprüft, um sicherzustellen, dass das erwartete Verhalten vorliegt.

5. **Berichterstattung:**
   - Die Tests wurden erfolgreich ausgeführt und das Spielverhalten wurde wie erwartet validiert.

