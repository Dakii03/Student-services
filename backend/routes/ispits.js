const express = require("express");
const router = express.Router();
const db = require("../db");


// ==========================
// GET /api/Ispits
// svi ispiti
// ==========================
router.get("/", (req, res) => {
  const sql = `
    SELECT *
    FROM ispit
  `;

  db.all(sql, [], (err, rows) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.json(rows);
  });
});


// ==========================
// GET /api/Ispits/:id
// jedan ispit po ID_ISPITA
// ==========================
router.get("/:id", (req, res) => {
  const sql = `
    SELECT *
    FROM ispit
    WHERE ID_ISPITA = ?
  `;

  db.get(sql, [req.params.id], (err, row) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    if (!row) {
      return res.status(404).json({ message: "Ispit nije pronađen" });
    }
    res.json(row);
  });
});


// ==========================
// POST /api/Ispits
// dodavanje ispita
// ==========================
router.post("/", (req, res) => {
  const { ID_ROKA, ID_PREDMETA, DATUM } = req.body;

  if (!ID_ROKA || !ID_PREDMETA || !DATUM) {
    return res.status(400).json({
      message: "ID_ROKA, ID_PREDMETA i DATUM su obavezni"
    });
  }

  const sql = `
    INSERT INTO ispit (ID_ROKA, ID_PREDMETA, DATUM)
    VALUES (?, ?, ?)
  `;

  db.run(sql, [ID_ROKA, ID_PREDMETA, DATUM], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    res.status(201).json({
      message: "Ispit uspešno dodat",
      ID_ISPITA: this.lastID
    });
  });
});


// ==========================
// PUT /api/Ispits/:id
// izmena ispita
// ==========================
router.put("/:id", (req, res) => {
  const { ID_ROKA, ID_PREDMETA, DATUM } = req.body;

  const sql = `
    UPDATE ispit
    SET ID_ROKA = ?,
        ID_PREDMETA = ?,
        DATUM = ?
    WHERE ID_ISPITA = ?
  `;

  db.run(
    sql,
    [ID_ROKA, ID_PREDMETA, DATUM, req.params.id],
    function (err) {
      if (err) {
        return res.status(500).json({ error: err.message });
      }

      if (this.changes === 0) {
        return res.status(404).json({ message: "Ispit nije pronađen" });
      }

      res.json({ message: "Ispit uspešno izmenjen" });
    }
  );
});


// ==========================
// DELETE /api/Ispits/:id
// brisanje ispita
// ==========================
router.delete("/:id", (req, res) => {
  const sql = `
    DELETE FROM ispit
    WHERE ID_ISPITA = ?
  `;

  db.run(sql, [req.params.id], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    if (this.changes === 0) {
      return res.status(404).json({ message: "Ispit nije pronađen" });
    }

    res.json({ message: "Ispit uspešno obrisan" });
  });
});

module.exports = router;
