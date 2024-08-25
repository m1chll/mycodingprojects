const LocalStrategy = require('passport-local').Strategy;
const bcrypt = require('bcrypt');
const User = require('./models/user');

async function getUserByEmail(email) {
  try {
    const user = await User.findOne({ email: email });
    return user; // Wenn kein Benutzer mit der E-Mail gefunden wird, gibt findOne null zurück
  } catch (error) {
    console.error('Error finding user by email:', error);
    throw error;
  }
}

// getUserById: Diese Funktion soll einen Benutzer anhand seiner ID finden und zurückgeben
async function getUserById(id) {
  try {
    const user = await User.findById(id);
    return user; // Wenn kein Benutzer mit der ID gefunden wird, gibt findById null zurück
  } catch (error) {
    console.error('Error finding user by ID:', error);
    throw error;
  }
}

function initialize(passport) {
  const authenticateUser = async (email, password, done) => {
    try {
      const user = await getUserByEmail(email);
      if (!user) {
        return done(null, false, { message: 'No user with that email' });
      }
      if (await bcrypt.compare(password, user.password)) {
        return done(null, user);
      } else {
        return done(null, false, { message: 'Password incorrect' });
      }
    } catch (error) {
      return done(error);
    }
  };

  passport.use(new LocalStrategy({ usernameField: 'email' }, authenticateUser));
  passport.serializeUser((user, done) => done(null, user.id));
  passport.deserializeUser(async (id, done) => {
    try {
      const user = await getUserById(id);
      done(null, user);
    } catch (error) {
      done(error);
    }
  });
}

module.exports = initialize;

/*
module.exports = {
  initialize,
  getUserByEmail
};
*/


