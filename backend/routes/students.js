const express = require("express");
const router = express.Router();
const db = require("../db");


// ==========================
// GET /api/Students
// svi studenti
// ==========================
router.get("/", (req, res) => {
  const sql = `
    SELECT *
    FROM student
  `;

  db.all(sql, [], (err, rows) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.json(rows);
  });
});


// ==========================
// GET /api/Students/:id
// jedan student po ID_STUDENTA
// ==========================
router.get("/:id", (req, res) => {
  const sql = `
    SELECT *
    FROM student
    WHERE ID_STUDENTA = ?
  `;

  db.get(sql, [req.params.id], (err, row) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    if (!row) {
      return res.status(404).json({ message: "Student nije pronađen" });
    }
    res.json(row);
  });
});

// ==========================
// POST /api/Students
// dodavanje studenta
// ==========================
router.post("/", (req, res) => {
  const { IME, PREZIME, SMER, BROJ, GODINA_UPISA } = req.body;

  if (!IME || !PREZIME || !SMER || !BROJ || !GODINA_UPISA) {
    return res.status(400).json({
      message: "IME, PREZIME, SMER, BROJ i GODINA_UPISA su obavezni"
    });
  }

  const sql = `
    INSERT INTO student (IME, PREZIME, SMER, BROJ, GODINA_UPISA)
    VALUES (?, ?, ?, ?, ?)
  `;

  db.run(sql, [IME, PREZIME, SMER, BROJ, GODINA_UPISA], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    res.status(201).json({
      message: "Student uspešno dodat",
      ID_STUDENTA: this.lastID
    });
  });
});


// ==========================
// PUT /api/Students/:id
// izmena studenta
// ==========================
router.put("/:id", (req, res) => {
  const { IME, PREZIME } = req.body;

  const sql = `
    UPDATE student
    SET IME = ?,
        PREZIME = ?
    WHERE ID_STUDENTA = ?
  `;

  db.run(
    sql,
    [IME, PREZIME, req.params.id],
    function (err) {
      if (err) {
        console.error("SQL GREŠKA:", err);
        return res.status(500).json({ error: err.message });
      }

      if (this.changes === 0) {
        return res.status(404).json({ message: "Student nije pronadjen" });
      }

      res.json({ message: "Student uspešno izmenjen" });
    }
  );
});



// ==========================
// DELETE /api/Students/:id
// brisanje studenta
// ==========================
router.delete("/:id", (req, res) => {
  const sql = `
    DELETE FROM student
    WHERE ID_STUDENTA = ?
  `;

  db.run(sql, [req.params.id], function (err) {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    if (this.changes === 0) {
      return res.status(404).json({ message: "Student nije pronadjen" });
    }

    res.json({ message: "Student uspesno obrisan" });
  });
});

module.exports = router;
