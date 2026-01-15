const express = require("express");
const router = express.Router();
const db = require("../db");

// ==========================
// GET /api/StudentPredmets
// ==========================
router.get("/", (req, res) => {
  const sql = `SELECT ID_STUDENTA, ID_PREDMETA, SKOLSKA_GODINA FROM student_predmet`;

  db.all(sql, [], (err, rows) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(rows);
  });
});

// ==========================
// GET /api/StudentPredmets/:id
// id = ID_STUDENTA
// ==========================
router.get("/:id", (req, res) => {
  const sql = `
    SELECT ID_STUDENTA, ID_PREDMETA, SKOLSKA_GODINA
    FROM student_predmet
    WHERE ID_STUDENTA = ?
  `;

  db.all(sql, [req.params.id], (err, rows) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(rows);
  });
});

// ==========================
// POST /api/StudentPredmets
// ==========================
router.post("/", (req, res) => {
  const { idStudenta, idPredmeta, skolskaGodina } = req.body;

  if (idStudenta == null || idPredmeta == null || !skolskaGodina) {
    return res.status(400).json({ message: "Sva polja su obavezna" });
  }

  const sql = `
    INSERT INTO student_predmet
      (ID_STUDENTA, ID_PREDMETA, SKOLSKA_GODINA)
    VALUES (?, ?, ?)
  `;

  db.run(sql, [idStudenta, idPredmeta, skolskaGodina], function (err) {
    if (err) return res.status(500).json({ error: err.message });

    res.status(201).json({ message: "StudentPredmet uspešno dodat" });
  });
});

// ==========================
// PUT /api/StudentPredmets/:id
// id = ID_STUDENTA
// ==========================
router.put("/:id", (req, res) => {
  const { idPredmeta, skolskaGodina } = req.body;

  if (idPredmeta == null || !skolskaGodina) {
    return res.status(400).json({ message: "idPredmeta i skolskaGodina su obavezni" });
  }

  const sql = `
    UPDATE student_predmet
    SET SKOLSKA_GODINA = ?
    WHERE ID_STUDENTA = ? AND ID_PREDMETA = ?
  `;

  db.run(sql, [skolskaGodina, req.params.id, idPredmeta], function (err) {
    if (err) return res.status(500).json({ error: err.message });
    if (this.changes === 0)
      return res.status(404).json({ message: "Zapis o student_predmet nije pronađen" });

    res.json({ message: "StudentPredmet uspešno izmenjen" });
  });
});

// ==========================
// DELETE /api/StudentPredmets/:idStudent/:idPredmet
// ==========================
router.delete("/:idStudent/:idPredmet", (req, res) => {
  const { idStudent, idPredmet } = req.params;

  const sql = `
    DELETE FROM student_predmet
    WHERE ID_STUDENTA = ? AND ID_PREDMETA = ?
  `;

  db.run(sql, [idStudent, idPredmet], function (err) {
    if (err) return res.status(500).json({ error: err.message });
    if (this.changes === 0)
      return res.status(404).json({ message: "StudentPredmet nije pronađen" });

    res.json({ message: "StudentPredmet uspešno obrisan" });
  });
});

module.exports = router;
