# Student Exam Management System

A web application for managing students, subjects, exam periods, exam records, and statistics.  
The system allows viewing student data, tracking exam attempts, calculating statistics, and analyzing student performance.

This project was developed as a university assignment using **Vue 3**, **Express.js**, and **SQLite**.

---

## Features

- Student list with selection and editing
- Subject and exam period selection
- Exam records (zapisnik) per subject and exam period
- Exam statistics (grade distribution)
- Student index formatting (study program, number, enrollment year)
- Calculation of passed exams and average grade
- REST API backend
- Clean and reactive frontend using Vue 3 Composition API

---

## Technologies Used

### Frontend
- Vue 3 (Composition API)
- Vite
- Axios
- JavaScript
- HTML / CSS

### Backend
- Node.js
- Express.js
- SQLite3

---

## Installation and Setup

### Backend

cd backend
npm install
node api.js

The backend runs on:

http://localhost:5000

### Frontend

cd frontend
npm install
npm run dev

The frontend runs on:

http://localhost:5173

### API Usage

The frontend communicates with the backend using REST endpoints such as:

GET /api/Students

PUT /api/Students/:id

GET /api/Predmets

GET /api/IspitniRoks

GET /api/Ispits

GET /api/Zapisniks

### Notes

Exam periods may share the same name but differ by academic year.

When no exams exist for a selected subject and exam period, a descriptive message is shown.

Statistics are calculated only for relevant exam records.

The application is designed for educational purposes.

### Author

Damjan Jovanović

License

This project is intended for educational use only.
