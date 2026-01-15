const express = require("express");
const router = express.Router();
const db = require("../db");


// ==========================
// GET /api/Predmets
// svi predmeti
// ==========================
router.get("/", (req, res) => {
  const sql = `
    SELECT *
    FROM predmet
  `;

  db.all(sql, [], (err, rows) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.json(rows);
  });
});


// ==========================
// GET /api/Predmets/:id
// jedan predmet po ID_PREDMETA
// ==========================
router.get("/:id", (req, res) => {
  const sql = `
    SELECT *
    FROM predmet
    WHERE ID_PREDMETA = ?
  `;

  db.get(sql, [req.params.id], (err, row) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    if (!row) {
      return res.status(404).json({ message: "Predmet nije pronađen" });
    }
    res.json(row);
  });
});

// ==========================
// POST /api/Predmets
// dodavanje predmeta
// ==========================
router.post("/", (req, res) => {
  const { ID_PROFESORA, NAZIV, ESPB, STATUS } = req.body;

  if (!ID_PROFESORA || !NAZIV || !ESPB || !STATUS) {
    return res.status(400).json({
      message: "ID_PROFESORA, NAZIV, ESPB i STATUS su obavezni"
    });
  }

  const sql = `
    INSERT INTO predmet (ID_PROFESORA, NAZIV, ESPB, STATUS)
    VALUES (?, ?, ?, ?)
  `;

  db.run(sql, [ID_PROFESORA, NAZIV, ESPB, STATUS], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    res.status(201).json({
      message: "Predmet uspešno dodat",
      ID_PREDMETA: this.lastID
    });
  });
});


// ==========================
// PUT /api/Predmets/:id
// izmena predmeta
// ==========================
router.put("/:id", (req, res) => {
  const { ID_PROFESORA, NAZIV, ESPB, STATUS } = req.body;
  const sql = `
    UPDATE predmet
    SET ID_PROFESORA = ?,
        NAZIV = ?,
        ESPB = ?,
        STATUS = ?
    WHERE ID_PREDMETA = ?
  `;

  db.run(
    sql,
    [ID_PROFESORA, NAZIV, ESPB, STATUS, req.params.id],
    function (err) {
      if (err) {
        return res.status(500).json({ error: err.message });
      }

      if (this.changes === 0) {
        return res.status(404).json({ message: "Predmet nije pronađen" });
      }

      res.json({ message: "Predmet uspešno izmenjen" });
    }
  );
});


// ==========================
// DELETE /api/Predmets/:id
// brisanje predmeta
// ==========================
router.delete("/:id", (req, res) => {
  const sql = `
    DELETE FROM predmet
    WHERE ID_PREDMETA = ?
  `;

  db.run(sql, [req.params.id], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    if (this.changes === 0) {
      return res.status(404).json({ message: "Predmet nije pronađen" });
    }

    res.json({ message: "Predmet uspešno obrisan" });
  });
});

module.exports = router;
