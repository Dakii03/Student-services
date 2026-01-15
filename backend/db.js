const sqlite3 = require("sqlite3").verbose();
const path = require("path");

const dbPath = path.join(__dirname, "STUDENTSKA.db");

const db = new sqlite3.Database(dbPath, (err) => {
  if (err) {
    console.error("Greška pri povezivanju sa bazom:", err.message);
  } else {
    console.log("Povezan sa SQLite bazom STUDENTSKA.db");
  }
});

module.exports = db;
