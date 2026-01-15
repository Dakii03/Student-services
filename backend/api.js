const express = require("express");
const app = express();
const cors = require("cors");

app.use(cors());  
app.use(express.json());

app.use("/api/IspitniRoks", require("./routes/ispitniRoks"));
app.use("/api/Ispits", require("./routes/ispits"));
app.use("/api/Predmets", require("./routes/predmets"));
app.use("/api/Profesors", require("./routes/profesors"));
app.use("/api/StudentPredmets", require("./routes/studentPredmets"));
app.use("/api/Students", require("./routes/students"));
app.use("/api/Zapisniks", require("./routes/zapisniks"));

app.get("/WeatherForecast", (req, res) => {
  res.send("WeatherForecast GET");
});

const PORT = 5000;
app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});
