//server.js
require('dotenv').config();

const express = require('express'); // Importieren des Express-Frameworks (Routing, einfachere Einbindung von Template-Engines...)
const app = express(); // Erstellen einer Express-App-Instanz 
const bcrypt = require('bcrypt'); // Importieren des bcrypt-Moduls zur Passwort-Hashing
const passport = require('passport'); // Importieren des Passport-Moduls für die Authentifizierung
const flash = require('express-flash'); // Importieren des express-flash-Moduls zur Anzeige von Flash-Nachrichten
const session = require('express-session'); // Importieren des express-session-Moduls zur Sitzungsverwaltung
const methodOverride = require('method-override'); // Importieren des method-override-Moduls zur Unterstützung von HTTP-Methoden wie PUT und DELETE
const mongoose = require('mongoose'); // Importieren des mongoose-Moduls zur Verbindung mit MongoDB
const User = require('./models/user'); // Importieren des Benutzermodells
const Habit = require('./models/habit'); // Importieren des Habit-Modells
const initializePassport = require('./passport-config'); // Importieren der Passport-Konfiguration


// Initialisieren der Passport-Authentifizierung mit benutzerdefinierten Funktionen zum Suchen von Benutzern nach E-Mail und ID
initializePassport(
  passport,
  async (email) => await User.findOne({ email: email }),
  async (id) => await User.findById(id)
);

// Konfigurieren der Express-App
app.set('view engine', 'ejs'); // Festlegen des View-Motors auf EJS (Embedded JavaScript)
app.use(express.urlencoded({ extended: false })); // Middleware zum Parsen von URL-kodierten Anforderungskörpern
app.use(express.json()); // Middleware zum Parsen von JSON-Anforderungskörpern
app.use(flash()); // Middleware zum Anzeigen von Flash-Nachrichten
app.use(
  session({
    secret: process.env.SESSION_SECRET, // Geheimer Schlüssel für die Sitzungsverwaltung
    resave: false,
    saveUninitialized: false,
  })
);
app.use(passport.initialize()); // Initialisieren von Passport
app.use(passport.session()); // Verwenden von Passport für die Sitzungsauthentifizierung
app.use(methodOverride('_method')); // Middleware zum Unterstützen von PUT- und DELETE-Anfragen

// Verbindung zur MongoDB-Datenbank
mongoose
  .connect(
    'mongodb+srv://michaelepper:michael2005@clusterme.sqnnabo.mongodb.net/?retryWrites=true&w=majority&appName=Clusterme'
  )
  .then(() => console.log('DB Connection successful!'))
  .catch(err => console.error('DB Connection error:', err));

// Middleware-Funktionen zur Überprüfung der Authentifizierung
function checkAuthenticated(req, res, next) {
  if (req.isAuthenticated()) {
    return next(); // Weiterleiten der Anfrage, wenn der Benutzer authentifiziert ist
  }
  res.redirect('/login'); // Umleitung zur Anmeldeseite, wenn der Benutzer nicht authentifiziert ist
}

function checkNotAuthenticated(req, res, next) {
  if (req.isAuthenticated()) {
    return res.redirect('/'); // Umleitung zur Startseite, wenn der Benutzer bereits authentifiziert ist
  }
  next();
}

// Routen für Anmeldeseite und Anmeldeprozess
app.get('/login', checkNotAuthenticated, (req, res) => {
  res.render('login.ejs'); // Rendern der Anmeldeseite
});

app.post('/login', checkNotAuthenticated, passport.authenticate('local', {
  successRedirect: '/', // Umleitung zur Startseite bei erfolgreicher Anmeldung
  failureRedirect: '/login', // Umleitung zur Anmeldeseite bei fehlgeschlagener Anmeldung
  failureFlash: true
}));

// Routen für Registrierungsseite und Registrierungsprozess
app.get('/register', checkNotAuthenticated, (req, res) => {
  res.render('register.ejs'); // Rendern der Registrierungsseite
});

app.post('/register', checkNotAuthenticated, async (req, res) => {
  try {
    // Hashen des Passworts vor dem Speichern des Benutzers in die Datenbank
    const hashedPassword = await bcrypt.hash(req.body.password, 10);
    const user = new User({
      name: req.body.name,
      email: req.body.email,
      password: hashedPassword,
    });
    await user.save(); // Speichern des Benutzers in der Datenbank
    res.redirect('/login'); // Umleitung zur Anmeldeseite nach erfolgreicher Registrierung
  } catch (error) {
    console.error(error);
    res.redirect('/register'); // Umleitung zur Registrierungsseite bei Fehlern
  }
});

// Route zum Abmelden
app.delete('/logout', (req, res) => {
  req.logout(() => {
    res.redirect('/login'); // Umleitung zur Anmeldeseite nach Abmeldung
  });
});

// Funktion, um die Daten der aktuellen Woche zu erhalten

// Funktion zur Berechnung der Daten der aktuellen Woche
function getWeekDates(baseDate = new Date()) {
  const dates = [];
  const today = new Date(baseDate); // Verwenden Sie baseDate
  const currentDayOfWeek = today.getDay();
  const differenceToMonday = currentDayOfWeek === 0 ? 6 : currentDayOfWeek - 1;

  today.setDate(today.getDate() - differenceToMonday);

  for (let i = 0; i < 7; i++) {
    let day = new Date(today);
    day.setDate(today.getDate() + i);
    dates.push(day.toLocaleDateString('de-DE'));
  }
  return dates;
}



app.get('/', checkAuthenticated, async (req, res) => {
  let baseDate = new Date();
  if (req.query.date) {
    baseDate = new Date(req.query.date);
  }
  const weekDates = getWeekDates(baseDate);
  const habits = await Habit.find({ user: req.user._id });
  res.render('index.ejs', { name: req.user.name, habits: habits, weekDates: weekDates, baseDate: baseDate.toISOString() });
});


app.post('/habits', checkAuthenticated, async (req, res) => {
  const { name } = req.body;
  const newHabit = new Habit({
    name,
    user: req.user._id, // Stelle sicher, dass der Habit dem angemeldeten Benutzer zugeordnet ist
    checks: [] // Initialisiere mit einem leeren Array
  });
  try {
    await newHabit.save();
    res.redirect('/'); // Leite den Benutzer zurück zur Hauptseite, um die aktualisierte Liste anzuzeigen
  } catch (error) {
    console.error(error);
    res.status(500).send('Fehler beim Erstellen der Gewohnheit');
  }
});



// Habit checkboxen
app.post('/habit/check', checkAuthenticated, async (req, res) => {
  const { habitId, date, checked } = req.body;

  try {
    // Debugging: Überprüfen der empfangenen Daten
    console.log("Received data:", req.body);

    // Überprüfen, ob das empfangene Datum in ein gültiges Date-Objekt umgewandelt werden kann
    const parsedDate = new Date(date);
    if (isNaN(parsedDate.valueOf())) {
      throw new Error('Invalid date format.');
    }

    // Setze die Uhrzeit des Date-Objekts auf Mitternacht, um Konsistenz zu gewährleisten
    parsedDate.setHours(0, 0, 0, 0);

    // Aktualisieren der Habit-Daten in der Datenbank
    const habit = await Habit.findById(habitId);
    if (!habit) {
      return res.status(404).json({ success: false, message: "Habit not found." });
    }

    if (checked === true) {
      // Wenn das Kästchen angekreuzt wurde, füge das Datum zum Array hinzu
      habit.checks.push(parsedDate);
    } else {
      // Wenn das Kästchen deaktiviert wurde, entferne das Datum aus dem Array
      const index = habit.checks.findIndex(check => check.toDateString() === parsedDate.toDateString());
      if (index !== -1) {
        habit.checks.splice(index, 1);
      }
    }

    // Speichern der aktualisierten Habit-Daten in der Datenbank
    await habit.save();

    // Debugging: Überprüfen, ob die Habit-Daten ordnungsgemäß aktualisiert wurden
    console.log("Updated habit:", habit);

    // Erfolgreiche Antwort senden
    res.json({ success: true, message: "Habit check status updated." });
  } catch (error) {
    console.error("Error updating habit check status:", error);
    res.status(500).json({ success: false, message: "Error updating habit check status." });
  }
});

app.post('/habit/update-name', checkAuthenticated, async (req, res) => {
  const { habitId, newName } = req.body;
  try {
    const habit = await Habit.findById(habitId);
    if (!habit) {
      // Sendet eine Antwort zurück, dass das Habit nicht gefunden wurde
      return res.status(404).json({ success: false, message: 'Habit not found.' });
    }
    habit.name = newName;
    await habit.save();
    // Sendet eine erfolgreiche Antwort zurück
    res.json({ success: true, message: 'Habit name updated successfully.' });
  } catch (error) {
    console.error(error);
    res.status(500).json({ success: false, message: 'Error updating habit name.' });
  }
});


app.post('/habit/delete', checkAuthenticated, async (req, res) => {
  const { habitId } = req.body;
  try {
    await Habit.findByIdAndDelete(habitId);
    res.redirect('/');
  } catch (error) {
    console.error(error);
    res.status(500).send('Error deleting habit.');
  }
});




app.listen(3000, () => {
  console.log('Server is running on http://localhost:3000');
});