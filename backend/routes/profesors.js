const express = require("express");
const router = express.Router();
const db = require("../db");


// ==========================
// GET /api/Profesors
// svi profesori
// ==========================
router.get("/", (req, res) => {
  const sql = `
    SELECT *
    FROM profesor
  `;

  db.all(sql, [], (err, rows) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.json(rows);
  });
});


// ==========================
// GET /api/Profesors/:id
// jedan profesor po ID_PROFESORA
// ==========================
router.get("/:id", (req, res) => {
  const sql = `
    SELECT *
    FROM profesor
    WHERE ID_PROFESORA = ?
  `;

  db.get(sql, [req.params.id], (err, row) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    if (!row) {
      return res.status(404).json({ message: "Profesor nije pronađen" });
    }
    res.json(row);
  });
});

// ==========================
// POST /api/Profesors
// dodavanje profesora
// ==========================
router.post("/", (req, res) => {
  const { IME, PREZIME, ZVANJE, DATUM_ZAP } = req.body;

  if (!IME || !PREZIME || !ZVANJE || !DATUM_ZAP) {
    return res.status(400).json({
      message: "IME, PREZIME, ZVANJE i DATUM_ZAP su obavezni"
    });
  }

  const sql = `
    INSERT INTO profesor (IME, PREZIME, ZVANJE, DATUM_ZAP)
    VALUES (?, ?, ?, ?)
  `;

  db.run(sql, [IME, PREZIME, ZVANJE, DATUM_ZAP], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    res.status(201).json({
      message: "Profesor uspešno dodat",
      ID_PROFESORA: this.lastID
    });
  });
});


// ==========================
// PUT /api/Profesors/:id
// izmena profesora
// ==========================
router.put("/:id", (req, res) => {
  const { IME, PREZIME, ZVANJE, DATUM_ZAP } = req.body;
  const sql = `
    UPDATE profesor
    SET IME = ?,
        PREZIME = ?,
        ZVANJE = ?,
        DATUM_ZAP = ?
    WHERE ID_PROFESORA = ?
  `;

  db.run(
    sql,
    [IME, PREZIME, ZVANJE, DATUM_ZAP, req.params.id],
    function (err) {
      if (err) {
        return res.status(500).json({ error: err.message });
      }

      if (this.changes === 0) {
        return res.status(404).json({ message: "Profesor nije pronađen" });
      }

      res.json({ message: "Profesor uspešno izmenjen" });
    }
  );
});


// ==========================
// DELETE /api/Profesors/:id
// brisanje profesora
// ==========================
router.delete("/:id", (req, res) => {
  const sql = `
    DELETE FROM profesor
    WHERE ID_PROFESORA = ?
  `;

  db.run(sql, [req.params.id], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    if (this.changes === 0) {
      return res.status(404).json({ message: "Profesor nije pronađen" });
    }

    res.json({ message: "Profesor uspešno obrisan" });
  });
});

module.exports = router;
