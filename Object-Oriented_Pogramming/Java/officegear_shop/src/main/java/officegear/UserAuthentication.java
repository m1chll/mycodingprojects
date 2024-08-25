package officegear;

import java.io.*;
import java.util.HashMap;
import java.util.Map;

public class UserAuthentication {
    // Pfad zur Datei, die die Benutzerdaten speichert
    private static final String USER_FILE = "src/main/java/officegear/users.txt";

    // Map zum Speichern von Benutzername, Passwort und E-Mail
    private final Map<String, String[]> users;

    // Konstruktor
    public UserAuthentication() {
        // Initialisiere die Map
        users = new HashMap<>();
        // Lade existierende Benutzer aus der Datei
        loadUsers();
    }

    // Methode zum Laden der Benutzer aus der Datei
    private void loadUsers() {
        File file = new File(USER_FILE);
        try (BufferedReader reader = new BufferedReader(new FileReader(file))) {
            String line;
            // Lese die Datei Zeile für Zeile
            while ((line = reader.readLine()) != null) {
                // Teile jede Zeile in Benutzername, Passwort und E-Mail
                String[] parts = line.split(":");
                // Füge die Daten zur Map hinzu
                users.put(parts[0], new String[]{parts[1], parts[2]}); // [passwort, email]
            }
        } catch (IOException e) {
            // Fehlerbehandlung bei Datei-Leseproblemen
            throw new Error(e);
        }
    }

    // Methode zum Speichern der Benutzer in die Datei
    private void saveUsers() {
        File file = new File(USER_FILE);
        System.out.println(file.getAbsolutePath());
        try (BufferedWriter writer = new BufferedWriter(new FileWriter(file))) {
            // Schreibe jeden Benutzer in die Datei
            for (Map.Entry<String, String[]> entry : users.entrySet()) {
                writer.write(entry.getKey() + ":" + entry.getValue()[0] + ":" + entry.getValue()[1]);
                writer.newLine();
            }
        } catch (IOException e) {
            // Fehlerbehandlung bei Datei-Schreibproblemen
            throw new Error(e);
        }
    }

    // Methode zur Registrierung eines neuen Benutzers
    public boolean register(String username, String password, String email) {
        // Prüfe, ob der Benutzername bereits existiert
        if (!users.containsKey(username)) {
            // Validiere das Passwort
            if (!PasswordValidator.isValid(password)) {
                System.out.println("Password does not meet security requirements.");
                return false;
            }
            // Füge den neuen Benutzer hinzu
            users.put(username, new String[]{password, email});
            // Speichere die Benutzerliste
            saveUsers();
            return true;
        }
        return false;
    }

    // Methode zum Einloggen eines Benutzers
    public boolean login(String username, String password) {
        // Prüfe, ob der Benutzer existiert und das Passwort korrekt ist
        return users.containsKey(username) && users.get(username)[0].equals(password);
    }

    // Methode zum Abrufen des Passworts eines Benutzers
    public String retrievePassword(String username) {
        // Prüfe, ob der Benutzer existiert
        if (users.containsKey(username)) {
            // Gib das Passwort zurück
            return users.get(username)[0];
        }
        return null;
    }
}
