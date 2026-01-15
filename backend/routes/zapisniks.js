const express = require("express");
const router = express.Router();
const db = require("../db");


// ==========================
// GET /api/Zapisniks
// ==========================
router.get("/", (req, res) => {
  const sql = `SELECT * FROM zapisnik`;

  db.all(sql, [], (err, rows) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(rows);
  });
});


// ==========================
// GET /api/Zapisniks/{id}
// id = ID_ISPITA
// ==========================
router.get("/:id", (req, res) => {
  const sql = `
    SELECT *
    FROM zapisnik
    WHERE ID_ISPITA = ?
  `;

  db.all(sql, [req.params.id], (err, rows) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(rows);
  });
});


// ==========================
// POST /api/Zapisniks
// ==========================
router.post("/", (req, res) => {
  const {
    idStudenta,
    idIspita,
    ocena,
    bodovi
  } = req.body;

  if (
    idStudenta == null ||
    idIspita == null ||
    ocena == null ||
    bodovi == null
  ) {
    return res.status(400).json({
      message: "Sva polja su obavezna"
    });
  }

  const sql = `
    INSERT INTO zapisnik
      (ID_STUDENTA, ID_ISPITA, OCENA, BODOVI)
    VALUES (?, ?, ?, ?)
  `;

  db.run(
    sql,
    [idStudenta, idIspita, ocena, bodovi],
    function (err) {
      if (err) return res.status(500).json({ error: err.message });

      res.status(201).json({
        message: "Zapisnik uspešno dodat"
      });
    }
  );
});


// ==========================
// PUT /api/Zapisniks/{id}
// id = ID_ISPITA
// ==========================
router.put("/:id", (req, res) => {
  const { idStudenta, ocena, bodovi } = req.body;

  const sql = `
    UPDATE zapisnik
    SET OCENA = ?,
        BODOVI = ?
    WHERE ID_ISPITA = ?
      AND ID_STUDENTA = ?
  `;

  db.run(
    sql,
    [ocena, bodovi, req.params.id, idStudenta],
    function (err) {
      if (err) return res.status(500).json({ error: err.message });
      if (this.changes === 0)
        return res.status(404).json({ message: "Zapisnik nije pronađen" });

      res.json({ message: "Zapisnik uspešno izmenjen" });
    }
  );
});


// ==========================
// DELETE /api/Zapisniks/{id}
// id = ID_ISPITA
// ==========================
router.delete("/:id", (req, res) => {
  const { idStudenta } = req.body;

  const sql = `
    DELETE FROM zapisnik
    WHERE ID_ISPITA = ?
      AND ID_STUDENTA = ?
  `;

  db.run(
    sql,
    [req.params.id, idStudenta],
    function (err) {
      if (err) return res.status(500).json({ error: err.message });
      if (this.changes === 0)
        return res.status(404).json({ message: "Zapisnik nije pronađen" });

      res.json({ message: "Zapisnik uspešno obrisan" });
    }
  );
});

module.exports = router;
