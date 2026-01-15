const express = require("express");
const router = express.Router();
const db = require("../db");

// ==========================
// GET /api/IspitniRoks
// svi ispitni rokovi
// ==========================
router.get("/", (req, res) => {
  const sql = `
    SELECT *
    FROM ispitni_rok
  `;

  db.all(sql, [], (err, rows) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.json(rows);
  });
});

// ==========================
// GET /api/IspitniRoks/:id
// jedan ispit rok po ID_ROKA
// ==========================
router.get("/:id", (req, res) => {
  const sql = `
    SELECT *
    FROM ispitni_rok
    WHERE ID_ROKA = ?
  `;

  db.get(sql, [req.params.id], (err, row) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    if (!row) {
      return res.status(404).json({ message: "Ispitni rok nije pronađen" });
    }
    res.json(row);
  });
});


// ==========================
// POST /api/IspitniRoks
// dodavanje ispitnog roka
// ==========================
router.post("/", (req, res) => {
  const { NAZIV, SKOLSKA_GOD, STATUS_ROKA } = req.body;

  if (!NAZIV || !SKOLSKA_GOD || !STATUS_ROKA) {
    return res.status(400).json({
      message: "NAZIV, SKOLSKA_GOD i STATUS_ROKA su obavezni"
    });
  }

  const sql = `
    INSERT INTO ispitni_rok (NAZIV, SKOLSKA_GOD, STATUS_ROKA)
    VALUES (?, ?, ?)
  `;

  db.run(sql, [NAZIV, SKOLSKA_GOD, STATUS_ROKA], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    res.status(201).json({
      message: "Ispitni rok uspešno dodat",
      ID_ROKA: this.lastID
    });
  });
});


// ==========================
// PUT /api/IspitniRoks/:id
// izmena ispitnog roka
// ==========================
router.put("/:id", (req, res) => {
  const { NAZIV, SKOLSKA_GOD, STATUS_ROKA } = req.body;
  const sql = `
    UPDATE ispitni_rok
    SET NAZIV = ?,
        SKOLSKA_GOD = ?,
        STATUS_ROKA = ?
    WHERE ID_ROKA = ?
  `;

  db.run(
    sql,
    [NAZIV, SKOLSKA_GOD, STATUS_ROKA, req.params.id],
    function (err) {
      if (err) {
        return res.status(500).json({ error: err.message });
      }

      if (this.changes === 0) {
        return res.status(404).json({ message: "Ispitni rok nije pronađen" });
      }

      res.json({ message: "Ispitni rok uspešno izmenjen" });
    }
  );
});


// ==========================
// DELETE /api/IspitniRoks/:id
// brisanje ispitnog roka
// ==========================
router.delete("/:id", (req, res) => {
  const sql = `
    DELETE FROM ispitni_rok
    WHERE ID_ROKA = ?
  `;

  db.run(sql, [req.params.id], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    if (this.changes === 0) {
      return res.status(404).json({ message: "Ispitni rok nije pronađen" });
    }

    res.json({ message: "Ispitni rok uspešno obrisan" });
  });
});

module.exports = router;