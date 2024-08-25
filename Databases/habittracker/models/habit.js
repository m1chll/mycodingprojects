// models/habit.js
const mongoose = require('mongoose');

const habitSchema = new mongoose.Schema({
  name: {
    type: String,
    required: true
  },
  user: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'User',
    required: true
  },
  checks: [{
    type: Date
  }]
});

module.exports = mongoose.model('Habit', habitSchema);
