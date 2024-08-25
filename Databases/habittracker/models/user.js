const mongoose = require('mongoose');

const userSchema = new mongoose.Schema({
  name: { type: String, required: true },
  email: { type: String, required: true, unique: true },
  password: { type: String, required: true },
  budget: [{ type: Number, default: 0 }] // Hier wird ein Array von Zahlen gespeichert
});

module.exports = mongoose.model('User', userSchema);
